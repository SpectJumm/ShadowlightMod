using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ShadowlightMod.Buffs;
using ShadowlightMod.Tiles.Ores;
namespace ShadowlightMod.Items
{
    public class DebugStick : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.WoodenSword;

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.WoodenSword);
            Item.useStyle = ItemUseStyleID.RaiseLamp;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.color = Color.Purple;
            Item.noMelee = true;
            Item.buffType = ModContent.BuffType<TintBuff>();
            Item.buffTime = 300;
        }
    }
}