using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using ShadowlightMod.Projectiles.Magic;

namespace ShadowlightMod.Items.Weapons.Magic
{
    public class TrolleyProblem : ModItem
    {

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.WaterBolt);
            Item.damage = 62;
            Item.useTime = 50;
            Item.width = 32;
            Item.height = 38;
            Item.useAnimation = 50;
            Item.mana = 22;
            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<BirchChip>();
            Item.knockBack = 2f;
            Item.value = Item.buyPrice(silver: 1);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item8;
            Item.autoReuse = true;
        }
    }
}