using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShadowlightMod.Items.Armor.Vanity
{
    [AutoloadEquip(EquipType.Head)]
    public class FloroSprout : ModItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 16;
            Item.value = Item.sellPrice(0, 0, 10);
            Item.rare = ItemRarityID.Green;
            Item.vanity = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.JungleSpores, 5)
                .AddIngredient(ItemID.GrassSeeds, 1)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}