
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using ShadowlightMod.Projectiles.OwnedByBosses;
using Terraria.DataStructures;

namespace ShadowlightMod.NPCs.Bosses.DeletedBlight
{
    [AutoloadBossHead]
    public class DeletedBlight : ModNPC
    {

        public ref float CurrentAttack => ref NPC.ai[0]; // What attack is being used

        public ref float AttackTimer => ref NPC.ai[1]; // How long any attack has been going on for

        public ref float AttackTimer2 => ref NPC.ai[2]; // Ditto, but for a second timer for attacks that need it

        public ref float AttackNumber => ref NPC.ai[3]; // How many times an attack has been used, for attacks that need it

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 1;

            NPCID.Sets.SpecificDebuffImmunity[NPC.type][BuffID.Confused] = true;
        }

        public static readonly SoundStyle GFBDeath = new SoundStyle("DeletedBlight/Sounds/NPCKilled/StoryofUndertale");
        public static readonly SoundStyle GFBDeath2 = new SoundStyle("DeletedBlight/Sounds/NPCKilled/DeltaruneExplosion");
        public bool CanAttack = true; // Turns off at points to prevent cheapshots
        public override bool CanHitPlayer(Player target, ref int cooldownSlot) => CanAttack;
        public override void SetDefaults()
        {
            NPC.width = 200;
            NPC.height = 200;
            NPC.damage = 50;
            NPC.defense = 20;
            NPC.lifeMax = 500000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(0, 10, 0, 0);
            NPC.boss = true;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.lavaImmune = true;
            NPC.aiStyle = -1; // Custom AI
            NPC.noTileCollide = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.WorldData);
            }
            Main.NewText("________ has awoken!", Color.MediumPurple); // ooooooo the boss has no name that makes it creepy ooooooo
        }
        public override void AI() // Making a few functions that have different attacks in them
        {
            NPC.TargetClosest();
            Player targetPlayer = Main.player[NPC.target]; // useful variable

            switch ((int)CurrentAttack)
            {
                case 0:
                    DemonicAssault();
                    break;
                case 1:
                    LeapOfFaith();
                    break;
                case 2:
                    TurkeyTurnabout();
                    break;
            }

            if (CurrentAttack >= 3) // Since I only have 3 attacks for now, reset the counter to loop
            {
                CurrentAttack = 0;
            }
            if (targetPlayer.dead)
            {
                NPC.velocity.Y -= 0.1f; // Fly up if the player is dead
                NPC.EncourageDespawn(10); // Method to despawn when offscreen after x ticks
                return;
            }
            
        }

        private void DemonicAssault()
        {
            // 5-dash attack, where the boss creates a star of 8 demon scythes in between each dash.
            AttackTimer++;
            Player targetPlayer = Main.player[NPC.target]; // useful variable
            Vector2 directionToTarget = targetPlayer.Center - NPC.Center;
            directionToTarget.Normalize();
            bool dashBrace = AttackTimer >= 60;

            if (!dashBrace)
            {
                AttackTimer++;
                CanAttack = false; // Prevent the boss from hitting cheapshots
                Vector2 desiredPosition = targetPlayer.Center; // Position above the player
                NPC.Center = Vector2.Lerp(NPC.Center, desiredPosition, 0.03f); // Move towards the desired position
                NPC.rotation = directionToTarget.ToRotation() - MathHelper.PiOver2; // Rotate the boss to face the player
            }

            if (dashBrace)
            {
                for (int loops = 0; loops < 2; loops++)
                {
                    for (int i = 0; i < 50; i++)
                    {
                        if (AttackTimer2 <= 10) // Dust stops during charge
                        {
                            NPC.velocity *= 0.9f;
                            Vector2 speed = Main.rand.NextVector2CircularEdge(1f, 1f);
                            Dust d = Dust.NewDustPerfect(NPC.Center, DustID.PurpleCrystalShard, speed * 10 * (loops + 1), Scale: 1.5f);
                            d.noGravity = true;
                        }
                    }
                }
                AttackTimer2++;
            }

            if (AttackTimer2 == 30) // After 30 ticks of bracing, dash towards the player
            {
                NPC.velocity = directionToTarget * 20f; // Adjust the speed of the dash
                NPC.rotation = directionToTarget.ToRotation() - MathHelper.PiOver2; // Rotate the boss to face the player
                SoundEngine.PlaySound(SoundID.Roar, NPC.position); // Play a roar sound when dashing
                for (int i = 0; i < 8; i++)
                {
                    Vector2 shootDirection = new Vector2(10f, 0f).RotatedBy(MathHelper.ToRadians(i * 45));
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, shootDirection, ProjectileID.DemonSickle, 100, 1f);
                }
                SoundEngine.PlaySound(SoundID.Item12, NPC.position); // Play a shooting sound
            }
            if (AttackTimer2 >= 60) // Reset timers
            {
                AttackTimer = 0;
                AttackTimer2 = 0;
                AttackNumber += 1; // New dash counter
            }
            if (AttackNumber >= 5)
            {
                NextAttack();
            }
        }

        private void LeapOfFaith()
        {
            // This attack has two phases: 
            // Phase 1 is the boss winding up and getting above the player
            // Phase 2 is the boss slamming down at the player and shooting projectiles to the sides
            // I'll use NPC.ai[1]/AttackTimer as a timer to determine when to switch between phases, and a bool to swap between the phases

            bool slammingDown = AttackTimer >= 60; // the swap takes place every 60 ticks

            if (!slammingDown)
            {
                // Phase 1: Winding up and getting above the player
                AttackTimer++;
                Player targetPlayer = Main.player[NPC.target]; // useful variable
                Vector2 directionToTarget = targetPlayer.Center - NPC.Center;
                directionToTarget.Normalize();
                CanAttack = false; // Prevent the boss from hitting cheapshots
                Vector2 desiredPosition = targetPlayer.Center + new Vector2(0f, -630f); // Position above the player
                NPC.Center = Vector2.Lerp(NPC.Center, desiredPosition, 0.07f); // Move towards the desired position
                NPC.rotation = directionToTarget.ToRotation() - MathHelper.PiOver2; // Rotate the boss to face the player
            }
            else
            {
                // Phase 2: Slamming down at the player and shooting projectiles to the sides
                AttackTimer++;
                Player targetPlayer = Main.player[NPC.target]; // useful variable
                Vector2 directionToTarget = targetPlayer.Center - NPC.Center;
                directionToTarget.Normalize();
                CanAttack = true; // Allow the boss to hit the player
                NPC.velocity = new Vector2(0f, 20f); // Move towards the player at high speed
                SoundEngine.PlaySound(SoundID.Roar, NPC.position); // Play a roar sound when slamming down
                NPC.rotation = 0; // Rotate the boss to face straight down

                if ((AttackTimer % 6 == 0) && (AttackTimer <= 110)) // Every 1/10 second, shoot projectiles to the sides
                {
                    Vector2 shootDirection = Vector2.UnitX * 10f; // Adjust the speed of the projectile
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, shootDirection, ModContent.ProjectileType<BlightedFeather>(), 100, 1f); // Shoot the BlightedFeather projectile
                    Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, shootDirection * -1, ModContent.ProjectileType<BlightedFeather>(), 100, 1f); // Shoot the BlightedFeather projectile                    
                    SoundEngine.PlaySound(SoundID.Item12, NPC.position); // Play a shooting sound
                }

                if (AttackTimer >= 140) // Reset the timer after a bit
                {
                    AttackTimer = 0;
                    AttackNumber += 1;
                }

                if (AttackNumber >= 5) // After 5 slams, move to the next attack
                {
                    NextAttack();
                }
            }
        }
        private void TurkeyTurnabout()
        {
            AttackTimer++;
            Player targetPlayer = Main.player[NPC.target]; // useful variable
            Vector2 directionToTarget = targetPlayer.Center - NPC.Center;
            directionToTarget.Normalize();
            Vector2 desiredPosition = targetPlayer.Center + new Vector2(500f, 0f).RotatedBy(MathHelper.ToRadians(AttackTimer * 2.4f));
            NPC.Center = Vector2.Lerp(NPC.Center, desiredPosition, 0.5f);
            NPC.rotation = directionToTarget.ToRotation() - MathHelper.PiOver2; // Rotate the boss to face the player

            if (AttackTimer % 30 == 0) // Every 30 ticks (0.5 seconds), shoot a projectile at the player
            {
                Vector2 shootDirection = directionToTarget * 10f; // Adjust the speed of the projectile
                Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center, shootDirection, ProjectileID.EyeLaser, 140, 1f);
                SoundEngine.PlaySound(SoundID.Item12, NPC.position); // Play a shooting sound
            }

            if (AttackTimer >= 450) // Reset the timer after 7 seconds
            {
                NextAttack();
            }
        }


        private void NextAttack()
        {
            CurrentAttack += 1;
            AttackTimer = 0;
            AttackTimer2 = 0;
            AttackNumber = 0;
        }

        public override void OnKill()
        {
            Main.NewText("The Deleted Blight has been defeated!", Color.MediumPurple);
        }
    }
}