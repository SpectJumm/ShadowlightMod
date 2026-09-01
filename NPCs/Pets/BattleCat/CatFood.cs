using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShadowlightMod.NPCs.Pets.BattleCat
{
    public class CatFood : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 30;
            Item.maxStack = 1;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.UseSound = SoundID.Item2;
            Item.consumable = false;
            Item.vanity = true;
            Item.rare = ItemRarityID.Green;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                player.AddBuff(ModContent.BuffType<BattleCatBuff>(), 3600);
            }
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Bass, 5)
                .AddIngredient(ItemID.TinBar, 3)
                .AddIngredient(ItemID.LicenseCat, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
            CreateRecipe()
                .AddIngredient(ItemID.Bass, 5)
                .AddIngredient(ItemID.CopperBar, 3)
                .AddIngredient(ItemID.LicenseCat, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}