using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ShadowlightMod.Projectiles.Ranged;
using Terraria.DataStructures;
using Terraria.Audio;

namespace ShadowlightMod.Items.Weapons.Ranged
{
    public class BioForceGun : ModItem
    {
        public const int HoldoutDistance = 20;

        public override void SetDefaults()
        {
            Item.width = 128;
            Item.height = 62;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.damage = 1000;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.shoot = ProjectileID.FireworkFountainRainbow; // self-voiding projectile discovered by AkiraPlayz
            Item.DamageType = DamageClass.Ranged;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ItemRarityID.Cyan;
            Item.shootSpeed = 16f;
            Item.channel = true;

        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // Since this item will attempt to shoot an ammo item, we need to set it back to the actual held projectile here.
            type = ModContent.ProjectileType<BioForceGunHoldout>();

            // The velocity value provided is not correct, so we need to calculate a new velocity since velocity for held projectiles is actually the holdout offset.
            velocity = Vector2.Normalize(velocity) * HoldoutDistance;

            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer);
            return false;

        }
        // Because this weapon fires a holdout projectile, it needs to block usage if its projectile already exists.
        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[ModContent.ProjectileType<BioForceGunHoldout>()] <= 1;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2f, 8f);
        }
    }
}