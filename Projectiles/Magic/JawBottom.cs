using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using ShadowlightMod.Projectiles.Magic;

namespace ShadowlightMod.Projectiles.Magic
{
    public class JawBottom : ModProjectile
    {
        public ref float FangsOpen => ref Projectile.ai[0];
        public bool FangsClose = false;
        public override bool? CanDamage() => FangsClose;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5; // The length of old position to be recorded
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0; // The recording mode
        }
        public override void SetDefaults()
        {
            Projectile.width = 47;
            Projectile.height = 24;
            Projectile.scale = 2f;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.light = 0.8f;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.timeLeft = 120;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 2;
        }
        public override void AI()
        {
            FangsOpen++; // A timer to determine when certain things should happen
            if (FangsOpen < 20)
            {
                FangsClose = false; // Projectile can't do damage yet
                Projectile.velocity.Y *= 0.9f; // Go in one direction and decelerate
            }
            else if (FangsOpen == 20 && !FangsClose)
            {
                FangsClose = true; // Turning on FangsClose makes the projectile able to do damage
                Projectile.velocity.Y *= -1f; // Reverse direction
            }
            else
            {
                Projectile.velocity.Y *= 1.12f; // Slam down in the other direction!
            }
            if (FangsOpen > 46)
            { // If the bottom jaw hits the top jaw, kill it immediately to prevent it from hitting multiple times

                Projectile.Kill(); // If the projectile hits the player, kill it immediately to prevent it from hitting multiple times
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            // Draws an afterimage trail. See https://github.com/tModLoader/tModLoader/wiki/Basic-Projectile#afterimage-trail for more information.

            Texture2D texture = TextureAssets.Projectile[Type].Value;

            Vector2 drawOrigin = new(texture.Width * 0.5f, Projectile.height * 0.5f);
            for (int k = Projectile.oldPos.Length - 1; k > 0; k--)
            {
                Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
                Color color = Projectile.GetAlpha(lightColor) * ((Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
                Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
            }
            return true;
            // TODO: Make the afterimages only draw during the slamming down
        }
    }
}