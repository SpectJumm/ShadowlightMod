using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.GameContent.RGB;

namespace ShadowlightMod.NPCs.Bosses.AyakaDaiakuma
{
    [AutoloadBossHead]
    public class AyakaDaiakumaHead : ModNPC
    {
        private bool tailSpawned = false;
        private int minLength = 100; // Minimum length of the worm
        private int maxLength = 100; // Maximum length of the worm

        public override void SetDefaults()
        {
            NPC.width = 27;
            NPC.height = 36;
            NPC.scale = 2f;
            NPC.damage = 120;
            NPC.defense = 10;
            NPC.lifeMax = 400000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = Item.buyPrice(6, 90, 0, 0);
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.boss = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.friendly = false;
            Music = MusicID.Plantera; // placeholder until I compose something better

            if (Main.getGoodWorld)
            {
                NPC.damage = 99999; // Make her head instakill in GFB
            }
            else if (Main.zenithWorld)
            {
                NPC.damage = 99999; // Make her head instakill in Zenith
                NPC.scale *= 2.5f; // Girl put on some weight lmfao
            }
            else
            {
                NPC.damage = 120;
                NPC.scale = 2f;
            }
        }

        public override void AI()
        {
            if (!tailSpawned && NPC.ai[0] == 0f) // Spawn a bunch of worm body segments, then a tail (I hope)
            {
                int previousSegment = NPC.whoAmI;
                int segment;
                for (int segmentSpawn = 0; segmentSpawn < maxLength; segmentSpawn++) // Loop between summoning body segments and alt body segments
                {

                    if (segmentSpawn >= 0 % 2 && segmentSpawn < minLength)
                    {
                        segment = ModContent.NPCType<AyakaDaiakumaBody>();
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, segment, NPC.whoAmI);
                        int body = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, segment, NPC.whoAmI);
                        Main.npc[body].ai[1] = previousSegment;
                    }
                    else
                    {
                        segment = ModContent.NPCType<AyakaDaiakumaBodyAlt>();
                        NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, segment, NPC.whoAmI);
                        int bodyAlt = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, segment, NPC.whoAmI);
                        Main.npc[bodyAlt].ai[1] = previousSegment;
                    }
                } // End the loop and add a tail segment
                segment = ModContent.NPCType<AyakaDaiakumaTail>();
                int tail = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.position.X, (int)NPC.position.Y, segment, NPC.whoAmI);
                Main.npc[tail].realLife = NPC.whoAmI;
                Main.npc[tail].ai[1] = previousSegment;
                tailSpawned = true;
            }

            // Additional AI code for the head can be added here
            // placeholder AI to make her move
            int MoveSpeed = 8;
            float acceleration = 0.6f;
            Vector2 targetPosition = Main.player[NPC.target].Center;
            Vector2 direction = targetPosition - NPC.Center;
            float distance = direction.Length();
            direction.Normalize();
            if (distance > 300f)
            {
                NPC.velocity += direction * acceleration;
                if (NPC.velocity.Length() > MoveSpeed)
                {
                    NPC.velocity = Vector2.Normalize(NPC.velocity) * MoveSpeed;
                }
            }
            else
            {
                NPC.velocity *= 1.05f; // Speed up when close to the player
            }
        }
    }
}