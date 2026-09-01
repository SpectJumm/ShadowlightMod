using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShadowlightMod.NPCs.Bosses.AyakaDaiakuma
{
    public class AyakaDaiakumaBody : ModNPC
    {
        public override void SetDefaults()
        {
            NPC.width = 27;
            NPC.height = 36;
            NPC.scale = 2f;
            NPC.damage = 100;
            NPC.defense = 50; // Extra defense for body segments
            NPC.lifeMax = 200000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.aiStyle = -1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.friendly = false;
            NPC.BossBar = Main.BigBossProgressBar.NeverValid; // No boss bar for body segments
            NPC.dontCountMe = true;

            if (Main.zenithWorld)
                NPC.scale *= 2.5f; // Again, Ayaka putting on weight in GFB
        }
        public override void AI()
        {
            // Body segment AI to follow the previous segment
            NPC npcPrevious = Main.npc[(int)NPC.ai[1]];
            Vector2 direction = npcPrevious.Center - NPC.Center;
            float distance = direction.Length();
            direction.Normalize();

            float desiredDistance = (NPC.width + npcPrevious.width) / 2f * NPC.scale; // Desired distance between segments
            if (distance > desiredDistance)
            {
                NPC.velocity += direction * 0.5f; // Move towards the previous segment
            }
            else
            {
                NPC.velocity *= 0.9f; // Slow down if too close
            }

            // Additional AI code for body segments can be added here
        }
    }
}