using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using ShadowlightMod.NPCs.Bosses.AyakaDaiakuma;
using ShadowlightMod.Rarities;

namespace ShadowlightMod.Items.SummonItems
{
    public class RitualKnife : ModItem
    {

        public static readonly SoundStyle UseSound = new SoundStyle("ShadowlightMod/Sounds/SummonItems/AyakaSummon")
        {
            Volume = 0.7f,
            PitchVariance = 0f,
        };
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.rare = ModContent.RarityType<BlightGreen>();
            Item.consumable = false;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ZoneOverworldHeight && !NPC.AnyNPCs(ModContent.NPCType<AyakaDaiakumaHead>());
        }

        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(UseSound, player.position);
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<AyakaDaiakumaHead>());
            }
            else
            {
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, -1, -1, null, player.whoAmI, ModContent.NPCType<AyakaDaiakumaHead>());
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient(ItemID.LunarBar, 5).
                AddIngredient(ItemID.LovePotion, 1).
                AddIngredient(ItemID.LivingFireBlock, 20).
                AddTile(TileID.LunarCraftingStation).
                Register();
        }
    }
}