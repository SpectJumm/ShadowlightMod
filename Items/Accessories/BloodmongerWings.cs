using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShadowlightMod.Items.Accessories
{
	// This is a basic wings item.
	// By default wings only support 4 frames of animation, see ExampleCustomDrawWings.cs for an example of custom wing animation.
	[AutoloadEquip(EquipType.Wings)]
	public class BloodmongerWings : ModItem
	{

		public override void SetStaticDefaults() {
			// These wings use the same values as the solar wings
			// Fly time: 180 ticks = 3 seconds
			// Fly speed: 9
			// Acceleration multiplier: 2.5
			ArmorIDs.Wing.Sets.Stats[Item.wingSlot] = new WingStats(180, 9f, 2f, true, -1, 2.5f);
		}

		public override void SetDefaults() {
			Item.width = 22;
			Item.height = 20;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.accessory = true;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend) {
			ascentWhenFalling = 0.85f; // Falling glide speed
			ascentWhenRising = 0.15f; // Rising speed
			maxCanAscendMultiplier = 1f;
			maxAscentMultiplier = 3f;
			constantAscend = 0.135f;
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.wingTime == 0f && player.TryingToHoverUp && player.statLife >= (player.statLifeMax2 / 100) + 1)
            {
                player.statLife -= player.statLifeMax2 / 100;
                player.wingTime += 5;
            }
            else
            {
                return;
            }
        }
        

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.

}
}