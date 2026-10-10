using System;
using Microsoft.Xna.Framework;
using ShadowlightMod.Projectiles.Melee;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShadowlightMod.Items.Weapons.Melee
{
    public class TrapperTapper : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.SolarEruption);
            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(0, 2);
            Item.damage = 52;
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.noUseGraphic = true;
            Item.shoot = ModContent.ProjectileType<TrapperTapperFlail>();
        }
    }
}