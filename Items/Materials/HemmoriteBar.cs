using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ShadowlightMod.Tiles.Ores;

namespace ShadowlightMod.Items.Materials
{
    public class HemmoriteBar : ModItem
    {
        public override void SetStaticDefaults() {
			// Registers a vertical animation with 17 frames and each one will last 5 ticks (1/12 second)
			Main.RegisterItemAnimation(Type, new DrawAnimationVertical(5, 17));
			ItemID.Sets.AnimatesAsSoul[Type] = true; // Makes the item have an animation while in world (not held.). Use in combination with RegisterItemAnimation

			Item.ResearchUnlockCount = 25; // Configure the amount of this item that's needed to research it in Journey mode.
		}
        public override void SetDefaults() {
            Item.width = 44;
            Item.height = 32;
            Item.maxStack = 9999;
            Item.value = Item.buyPrice(silver: 2);
            Item.rare = ItemRarityID.Blue;
            
            // Link the item to your corresponding placeable tile ID
            // Item.createTile = ModContent.TileType<Tiles.ExampleBarTile>();
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.autoReuse = true;
            Item.consumable = true;
        }

        /*public override void AddRecipes() {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Hemmorite>(), 3)
                .AddTile(TileID.Furnaces)
                .Register();
        }*/
    }
}