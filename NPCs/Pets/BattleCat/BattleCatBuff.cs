using Terraria;
using Terraria.ModLoader;

namespace ShadowlightMod.NPCs.Pets.BattleCat
{
    public class BattleCatBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            bool unused = false;
            player.BuffHandle_SpawnPetIfNeededAndSetTime(buffIndex, ref unused, ModContent.ProjectileType<BattleCatProj>());
        }
    }
}