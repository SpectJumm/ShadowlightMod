using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ShadowlightMod.Projectiles.Ranged;

namespace ShadowlightMod.Items.Weapons.Ranged
{
    public class CoconutGun : ModItem
    {

        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 42;
            Item.height = 36;
            Item.useAnimation = 24;
            Item.useTime = 8; // one third of useAnimation
            Item.reuseDelay = 30;
            Item.consumeAmmoOnLastShotOnly = true;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item11;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<CoconutGunProjectile>();
            Item.shootSpeed = 10f;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(4));
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Coconut, 2)
                .AddRecipeGroup("Wood", 20)
                .AddIngredient(ItemID.GoldBar, 10)
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe() // idk how to make it so that it can be crafted with either gold or platinum, so i just made two recipes
                .AddIngredient(ItemID.Coconut, 2)
                .AddRecipeGroup("Wood", 20)
                .AddIngredient(ItemID.PlatinumBar, 10)
                .AddTile(TileID.Anvils)
                .Register();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2f, -2f);
        }
    }
}