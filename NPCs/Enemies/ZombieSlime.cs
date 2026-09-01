using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace ShadowlightMod.NPCs.Enemies
{

    public class ZombieSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;
        }
        public override void SetDefaults()
        {
            NPC.aiStyle = NPCAIStyleID.Slime;
            AIType = NPCID.CorruptSlime;
            NPC.damage = 80;
            NPC.width = 48;
            NPC.height = 36;
            NPC.defense = 16;
            NPC.lifeMax = 220;
            NPC.alpha = 0;
            NPC.knockBackResist = 0.8f;
            NPC.value = Item.buyPrice(0, 0, 4, 0);
            AnimationType = NPCID.CorruptSlime;
            NPC.lavaImmune = false;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.PlayerSafe)
                return 0f;
            return SpawnCondition.OverworldNight.Chance * 0.5f;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.ZombieArm, 100, 0, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 3, 6));
        }
    }
}