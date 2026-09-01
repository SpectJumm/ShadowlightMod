using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShadowlightMod.Buffs
{
    public class BayonetStabbed : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.pvpBuff[Type] = false; // Allow this buff in PvP
            Main.buffNoSave[Type] = true; // Don't save this buff when exiting the world
            Main.debuff[Type] = true; // Mark this buff as a debuff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // This buff doesn't have any specific behavior on the player, but it can be used to modify damage taken in the BlightGlobalNPC class
        }
    }
}