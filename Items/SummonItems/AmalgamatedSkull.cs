using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShadowlightMod.NPCs.Bosses.DeletedBlight;
using Terraria.Audio;
namespace ShadowlightMod.Items.SummonItems
{
    
	public class AmalgamatedSkull : ModItem
	{
        public static readonly SoundStyle UseSound = new SoundStyle("ShadowlightMod/Sounds/SummonItems/DeletedBlightSummon");

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 28;
			Item.maxStack = 1;
			Item.rare = ItemRarityID.Purple;
			Item.useAnimation = 30;
			Item.useTime = 30;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.consumable = false;
		}

        public override bool CanUseItem(Player player)
        {
            return player.ZoneOverworldHeight;
        }

		public override bool? UseItem(Player player)
		{
			SoundEngine.PlaySound(UseSound, player.position);
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<DeletedBlight>());
            }
            else
            {
                NetMessage.SendData(MessageID.SpawnBossUseLicenseStartEvent, -1, -1, null, player.whoAmI, ModContent.NPCType<DeletedBlight>());
            }
            return true;
		}
	}
}
