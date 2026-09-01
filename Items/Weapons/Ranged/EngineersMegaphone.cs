using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShadowlightMod.Projectiles.Ranged;

namespace ShadowlightMod.Items.Weapons.Ranged

{
    public class EngineersMegaphone : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 34;
            Item.DamageType = DamageClass.Ranged;


            Item.useTime = 35;
            Item.useAnimation = 35;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shoot = ModContent.ProjectileType<EngineersMegaphoneProjectile>();
            Item.shootSpeed = 14f;
            Item.knockBack = 5f;
            Item.rare = ItemRarityID.Green;
            Item.autoReuse = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient(ItemID.Wood, 10).
                AddIngredient(ItemID.Silk, 5).
                AddIngredient(ItemID.Gel, 5).
                AddTile(TileID.WorkBenches).
                Register();
        }
    }
}