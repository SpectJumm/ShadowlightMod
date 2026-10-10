using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using ShadowlightMod.Items.Materials;

namespace ShadowlightMod.Items.Armor
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Body value here will result in TML expecting a X_Body.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Body)]
	public class BloodmongerBreastplate : ModItem
	{
		public static readonly int moveSpeedBonus = 8;
		public static readonly int lifeRegenBonus = 8;

		public override void SetDefaults()
		{
			Item.width = 18; // Width of the item
			Item.height = 18; // Height of the item
			Item.value = Item.sellPrice(gold: 1); // How many coins the item is worth
			Item.rare = ModContent.RarityType<Rarities.BlightGreen>(); // The rarity of the item
			Item.defense = 24; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player)
        {
            player.buffImmune[BuffID.OnFire] = true; // Make the player immune to Fire
            player.moveSpeed += moveSpeedBonus / 100f;
			player.lifeRegen += lifeRegenBonus / 100;
		}

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<HemmoriteBar>(), 24)
                .AddTile(TileID.LunarCraftingStation)
                .Register();
        }

	}
}
