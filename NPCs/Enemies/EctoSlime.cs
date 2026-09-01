using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace ShadowlightMod.NPCs.Enemies
{

    public class EctoSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = 2;
        }
        public override void SetDefaults()
        {
            NPC.aiStyle = NPCAIStyleID.Slime;
            AIType = NPCID.DungeonSlime;
            NPC.damage = 120;
            NPC.width = 48;
            NPC.height = 36;
            NPC.defense = 20;
            NPC.lifeMax = 300;
            NPC.alpha = 50;
            NPC.knockBackResist = 0.8f;
            NPC.value = Item.buyPrice(0, 0, 6, 0);
            AnimationType = NPCID.CorruptSlime;
            NPC.lavaImmune = false;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath39;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.PlayerSafe || !NPC.downedPlantBoss)
                return 0f;
            return SpawnCondition.Dungeon.Chance * 0.1f;
        }
        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Ectoplasm, 4, 0, 1));
            npcLoot.Add(ItemDropRule.Common(ItemID.Gel, 1, 4, 6));
        }
    }
}