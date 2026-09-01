using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShadowlightMod.Items.Weapons.Melee
{
    public class ComicallyLargeMetalSword : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 30;
            Item.DamageType = DamageClass.Melee;
            Item.width = 720;
            Item.height = 720;
            Item.scale = 20f;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2f;
            Item.value = Item.buyPrice(silver: 1);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }
    }
}