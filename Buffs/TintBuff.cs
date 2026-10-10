using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
namespace ShadowlightMod.Buffs
{
    public class TintBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (!Main.dedServ && !Filters.Scene["ShadowlightMod:FirstScreenShader"].IsActive())
            {
                Filters.Scene.Activate("ShadowlightMod:FirstScreenShader");
            }
        }
    }
}