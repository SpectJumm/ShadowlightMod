using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShadowlightMod.Items.Weapons.Magic;

namespace ShadowlightMod.Projectiles.Magic
{
    // The held projectile of the ExampleHeldProjectileWeapon item. This is where the custom logic of this weapon is implemented.
    // Also note that this projectile has a _Glow texture that will automatically be drawn over the regular texture.
    public class ArcticAntlerHoldout : ModProjectile
    {
        public ref float HoldTimer => ref Projectile.ai[0];
        public ref float ShootTimer => ref Projectile.ai[1];
        // These values place caps on the mana consumption rate of the Prism.
        // When first used, the Prism consumes mana once every MaxManaConsumptionDelay frames.
        // Every time mana is consumed, the pace becomes one frame faster, meaning mana consumption smoothly increases.
        // When capped out, the Prism consumes mana once every MinManaConsumptionDelay frames.
        private const float MaxManaConsumptionDelay = 15f;
        private const float MinManaConsumptionDelay = 5f;

        public override void SetDefaults()
        {
            Projectile.width = 66;
            Projectile.height = 56;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.hide = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;

            // Adjust the drawing to change how it appears when held
            DrawOffsetX = 0;
            DrawOriginOffsetY = -4;
        }
        public override bool? CanDamage() => false;

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter);

            UpdateDamageForManaSickness(player);
            // HoldTimer counts how long the weapon has been used. It helps control how fast the weapon animates and shoots arrows.
            HoldTimer += 1f;
            // Make the first round of shots ignore the animation delay by setting the "speed" to 0 until the first shot has happened
            float animationSpeed = HoldTimer < 21 ? 0f : 1f;
            int initialShootDelay = 160;  // The initial delay before the first shot
            int shootDelayAdjustmentRate = 156;  // After the initial 20 tick delay, successive shots will take 20 + 10 = 30 ticks
            ShootTimer += 1f;
            bool shouldShootArrow = false;
            if (ShootTimer >= initialShootDelay - shootDelayAdjustmentRate * animationSpeed)
            {
                ShootTimer = 0f;
                shouldShootArrow = true;
            }

            if (ShootTimer == 1f && HoldTimer != 1f)
            {
                Vector2 dustSpawnLocation = Projectile.Center + new Vector2(30, 0).RotatedBy(Projectile.rotation - (Projectile.direction == 1 ? 0 : MathHelper.Pi)) - new Vector2(8, 8);
                for (int i = 0; i < 2; i++)
                {
                    var dust = Dust.NewDustDirect(dustSpawnLocation, 16, 16, DustID.IceTorch, Projectile.velocity.X / 2f, Projectile.velocity.Y / 2f, 100);
                    dust.velocity *= 0.66f;
                    dust.noGravity = true;
                    dust.scale = 1.4f;
                }
            }

            if (shouldShootArrow && Main.myPlayer == Projectile.owner)
            {
                Item heldItem = player.HeldItem;
                if (player.channel && !player.noItems && !player.CCed && player.statMana >= 4)
                {
                    // Play firing sound

                    // Calculate the holdout offset based on the mouse position
                    {
                        float holdoutDistance = ArcticAntler.HoldoutDistance * Projectile.scale;
                        Vector2 holdoutOffset = holdoutDistance * Vector2.Normalize(Main.MouseWorld - playerCenter);
                        if (holdoutOffset.X != Projectile.velocity.X || holdoutOffset.Y != Projectile.velocity.Y)
                        {
                            Projectile.netUpdate = true;
                        }

                        // Set the projectile velocity, which is actually the holdout offset for held projectiles.
                        Projectile.velocity = holdoutOffset;
                        for (int j = 0; j < 1; j++)
                        {
                            // Calculate a spawn location, taking into account the muzzle placement and a random variation
                            bool ammoConsumed = player.PickAmmo(heldItem, out int projToShoot, out float speed, out int damage, out float knockBack, out int usedAmmoItemId); // idk what this is but removing it breaks everything
                            var source = player.GetSource_ItemUse_WithPotentialAmmo(heldItem, usedAmmoItemId);
                            Projectile.NewProjectile(source, new Vector2(Main.MouseWorld.X, playerCenter.Y + 600f), new Vector2(0f, 16f), ModContent.ProjectileType<SnowgraveIce>(), 55, knockBack, Projectile.owner);
                            player.CheckMana(4, true, false);
                        }
                    }
                }
                else
                {
                    Projectile.Kill();
                }
            }

            // Update player arm position and item use time
            Projectile.direction = Projectile.velocity.X < 0 ? -1 : 1;
            Projectile.spriteDirection = Projectile.direction;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.SetDummyItemTime(2);
            Projectile.Center = playerCenter;
            float rotationOffset = Projectile.spriteDirection == -1 ? MathHelper.Pi : 0;
            Projectile.rotation = Projectile.velocity.ToRotation() + rotationOffset;
            player.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();
            Projectile.timeLeft = 2; // Set the time left to make sure it disappears after SHE WAS USED UP
        }
        private void UpdateDamageForManaSickness(Player player)
        {
            Projectile.damage = (int)player.GetDamage(DamageClass.Magic).ApplyTo(player.HeldItem.damage);
        }
    }
}