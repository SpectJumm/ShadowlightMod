using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using System;


namespace ShadowlightMod.NPCs.Enemies
{
    public class ArmoredSnake : ModNPC
    {

        private readonly int cooldown = 100;
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 4; // Assuming the snake has 4 frames of animation
        }

        public override void SetDefaults()
        {
            NPC.width = 44;
            NPC.height = 28;
            NPC.damage = 15;
            NPC.defense = 1;
            NPC.lifeMax = 50;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 100f;
            NPC.noGravity = false;
            NPC.knockBackResist = 1f;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldDayDesert.Chance * 0.1f; // Adjust the spawn chance as needed
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter += 1.0;
            if (NPC.frameCounter >= 10.0) // Change frame every 10 ticks
            {
                NPC.frameCounter = 0.0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y >= frameHeight * Main.npcFrameCount[NPC.type])
                {
                    NPC.frame.Y = 0;
                }
            }
        }
        public override void AI()
        {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            NPC.ai[0]++; // Increment the cooldown timer
            NPC.spriteDirection = NPC.direction; // Face the direction of movement
            NPC.velocity.Y += 0.1f; // Apply gravity

            if (NPC.justHit)
            {
                NPC.ai[0] = 0; // Reset the cooldown timer when hit
            }

            if (player.active && NPC.Distance(player.Center) < 100f)
            {
                NPC.velocity = Vector2.Zero; // Stop moving when close to the player
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.t_Cactus, 0f, 0f, 100, default(Color), 0.5f); // Add a smoke effect when preparing to attack
                if (NPC.ai[0] >= cooldown)
                {
                    SoundEngine.PlaySound(SoundID.Zombie46, NPC.position); // Play a sound when attacking
                    Vector2 shootDirection = player.Center - NPC.Center;
                    shootDirection.Normalize();
                    shootDirection *= 5f; // Set the speed of the projectile
                    var projectile = Projectile.NewProjectileDirect(NPC.GetSource_FromAI(), NPC.Center, shootDirection, ProjectileID.RollingCactusSpike, NPC.damage / 2, 0f);
                    projectile.hostile = true;
                    projectile.friendly = false;
                    NPC.ai[0] = 0; // Reset the cooldown timer
                }
            }
            else
            {
                Vector2 direction = player.Center - NPC.Center;
                direction.Normalize();
                NPC.velocity.X = direction.X * 1.5f; // Move towards the player at a speed of 1.5
            }
        }
    }
}