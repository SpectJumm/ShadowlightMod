using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShadowlightMod.Items.Weapons.Magic
{
    public class Cactome : ModItem
    {
        // Sure hope Lucille Karma doesn't mind the minor name plagiarism or else
        public override void SetDefaults()
        {
            Item.damage = 20;
            Item.DamageType = DamageClass.Magic;
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.mana = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 5f;
            Item.value = Item.sellPrice(0, 0, 8, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.RollingCactusSpike;
            Item.shootSpeed = 7f;
        }
    }
}