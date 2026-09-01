using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShadowlightMod.Projectiles.Ranged;

namespace ShadowlightMod.Items.Ammo
{
    public class VileBullet : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 14;
            Item.height = 36;
            Item.damage = 10;
            Item.DamageType = DamageClass.Ranged;

            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.knockBack = 1.5f;
            Item.value = Item.sellPrice(copper: 16);
            Item.shoot = ModContent.ProjectileType<VileBulletProjectile>();
            Item.shootSpeed = 7f;
            Item.ammo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            CreateRecipe(100)
                .AddIngredient(ItemID.WormTooth, 1)
                .AddIngredient(ItemID.MusketBall, 100)
                .AddIngredient(ItemID.ShadowScale, 1)
                .AddTile(TileID.DemonAltar)
                .Register();
        }
    }
}