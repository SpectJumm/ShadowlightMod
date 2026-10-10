using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ShadowlightMod.Items.Weapons.Ranged;
using Microsoft.CodeAnalysis;
using Terraria.DataStructures;

namespace ShadowlightMod.Projectiles.Ranged
{
    public class BioForceGunHoldout : ModProjectile
    {
        public ref float ChargeTimer => ref Projectile.ai[0];
        public override bool? CanDamage() => false;
        public override void SetDefaults()
        {
            Projectile.width = 128;
            Projectile.height = 62;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.hide = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
        }


        public override void AI()
        {
            bool Charged = false;
            Player player = Main.player[Projectile.owner];
            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter);
            Item heldItem = player.HeldItem;
            float holdoutDistance = BioForceGun.HoldoutDistance * Projectile.scale;
            Vector2 holdoutOffset = holdoutDistance * Vector2.Normalize(Main.MouseWorld - playerCenter);
            Projectile.velocity = holdoutOffset;
            bool ammoConsumed = player.PickAmmo(heldItem, out int projToShoot, out float speed, out int damage, out float knockBack, out int usedAmmoItemId); // idk what this is but removing it breaks everything
            var source = player.GetSource_ItemUse_WithPotentialAmmo(heldItem, usedAmmoItemId);
            if (player.channel && !player.noItems && !player.CCed)
            {
                if (!Charged)
                {
                    ChargeTimer += 1f;
                    if (ChargeTimer >= 180f)
                    {
                        Charged = true;
                    }
                    else
                    {
                        Charged = false;
                    }
                }

                if (Charged)
                {
                    Projectile.NewProjectile(source, new Vector2(playerCenter.X, playerCenter.Y), holdoutOffset, ProjectileID.VortexBeaterRocket, 1000, 16f, Projectile.owner);
                    ChargeTimer = 0f;
                    Charged = false;
                    Projectile.Kill();
                }
            } else
            {
                Projectile.Kill();
            }
            Projectile.direction = Projectile.velocity.X < 0 ? -1 : 1;
            Projectile.spriteDirection = Projectile.direction;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.SetDummyItemTime(2);
            Projectile.Center = playerCenter;
            float rotationOffset = Projectile.spriteDirection == -1 ? MathHelper.Pi : 0;
            Projectile.rotation = Projectile.velocity.ToRotation() + rotationOffset;
            player.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();
        }

    }
}