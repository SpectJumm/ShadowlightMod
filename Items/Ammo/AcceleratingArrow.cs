using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShadowlightMod.Projectiles.Ranged;

namespace ShadowlightMod.Items.Ammo
{
    public class AcceleratingArrow : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 14;
            Item.height = 32;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.knockBack = 2.5f;
            Item.value = Item.buyPrice(0, 0, 0, 10);
            Item.rare = ItemRarityID.White;
            Item.shoot = ModContent.ProjectileType<AcceleratingArrowProjectile>();
            Item.shootSpeed = 2f;
            Item.ammo = AmmoID.Arrow;
        }
        public override void AddRecipes()
        {
            CreateRecipe(100)
                .AddIngredient(ItemID.WoodenArrow, 100)
                .AddIngredient(ItemID.MartianConduitPlating, 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}