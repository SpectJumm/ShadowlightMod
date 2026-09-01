using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using ShadowlightMod.Projectiles.Melee;

namespace ShadowlightMod.Items.Weapons.Melee
{
    public class ToothofCthulhu : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 22;
            Item.DamageType = DamageClass.Melee;
            Item.width = 15;
            Item.height = 35;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.shoot = ModContent.ProjectileType<ToothofCthulhuProjectile>();
            Item.shootSpeed = 10f;
            Item.knockBack = 4f;
            Item.value = Item.buyPrice(silver: 50);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }
    }

    public class EyeofCthulhuDrops : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.EyeofCthulhu)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ToothofCthulhu>(), 4)); // 25% chance to drop
            }
        }
    }
}