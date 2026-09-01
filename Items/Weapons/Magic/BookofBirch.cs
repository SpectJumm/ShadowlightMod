using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using ShadowlightMod.Projectiles.Magic;

namespace ShadowlightMod.Items.Weapons.Magic
{
    public class BookofBirch : ModItem
    {

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.WaterBolt);
            Item.damage = 9;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.mana = 3;
            Item.scale = 0.5f;
            Item.shoot = ModContent.ProjectileType<BirchChip>();
            Item.knockBack = 2f;
            Item.value = Item.buyPrice(silver: 1);
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(4));
        }
        public override void AddRecipes()
        {
            CreateRecipe().
               AddIngredient(ItemID.Wood, 20).
               AddIngredient(ItemID.Acorn, 5).
               AddTile(TileID.WorkBenches).
               Register();
        }
    }
}