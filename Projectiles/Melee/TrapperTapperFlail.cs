using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace ShadowlightMod.Projectiles.Melee
{
    public class TrapperTapperFlail : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.SolarWhipSword);
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        { // took this code from Calamity's Nebulash
            if (projHitbox.Intersects(targetHitbox))
            {
                return true;
            }
            float useless = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Main.player[Projectile.owner].MountedCenter, Projectile.Center + Projectile.velocity, 16f * Projectile.scale, ref useless))
            {
                return true;
            }
            return false;
        }

        public override void AI()
        {
            // Vanilla code for the Solar Eruption's projectile
            if (!Main.dedServ && Projectile.localAI[0] == 0f)
            {
                SoundEngine.PlaySound(in SoundID.Item116, Entity.Center);
            }
            if (Projectile.localAI[1] > 0f)
            {
                Projectile.localAI[1] -= 1f;
            }
            Projectile.alpha -= 42;
            if (Projectile.alpha < 0)
            {
                Projectile.alpha = 0;
            }
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = Projectile.velocity.ToRotation();
            }
            float direction = (Projectile.localAI[0].ToRotationVector2().X >= 0f) ? 1 : (-1);
            if (Projectile.ai[1] <= 0f)
            {
                direction *= -1f;
            }
            Vector2 spinningpoint = (direction * (Projectile.ai[0] / 30f * MathHelper.TwoPi - MathHelper.PiOver2)).ToRotationVector2();
            spinningpoint.Y *= (float)Math.Sin(Projectile.ai[1]);
            if (Projectile.ai[1] <= 0f)
            {
                spinningpoint.Y *= -1f;
            }
            spinningpoint = spinningpoint.RotatedBy(Projectile.localAI[0]);
            Projectile.ai[0] += 1f;
            if (Projectile.ai[0] < 30f)
            {
                Entity.velocity += 48f * spinningpoint;
            }
            else
            {
                Projectile.Kill();
            }
            Projectile.position = Main.player[Projectile.owner].position;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Projectile.position = Main.player[Projectile.owner].position;
            Texture2D value58 = TextureAssets.Projectile[Projectile.type].Value;
            Color alpha11 = Projectile.GetAlpha(Color.White);
            if (Projectile.velocity == Vector2.Zero)
            {
                return false;
            }
            float num126 = Projectile.velocity.Length() + 16f;
            bool flag7 = num126 < 100f;
            Vector2 vector33 = Vector2.Normalize(Projectile.velocity);
            Rectangle rectangle12 = new Rectangle(0, 2, value58.Width, 40);
            Vector2 vector34 = new Vector2(0f, Main.player[Projectile.owner].gfxOffY);
            float rotation4 = Projectile.rotation + (float)Math.PI;
            Main.EntitySpriteDraw(value58, Projectile.Center.Floor() - Main.screenPosition + vector34, rectangle12, alpha11, rotation4, rectangle12.Size() / 2f - Vector2.UnitY * 4f, Projectile.scale, SpriteEffects.None);
            num126 -= 40f * Projectile.scale;
            Vector2 vector35 = Projectile.Center.Floor() + vector33 * Projectile.scale * 24f;
            rectangle12 = new Rectangle(0, 68, value58.Width, 18);
            if (num126 > 0f)
            {
                float num127 = 0f;
                while (num127 + 1f < num126)
                {
                    if (num126 - num127 < (float)rectangle12.Height)
                    {
                        rectangle12.Height = (int)(num126 - num127);
                    }
                    Main.EntitySpriteDraw(value58, vector35 - Main.screenPosition + vector34, rectangle12, alpha11, rotation4, new Vector2(rectangle12.Width / 2, 0f), Projectile.scale, SpriteEffects.None);
                    num127 += (float)rectangle12.Height * Projectile.scale;
                    vector35 += vector33 * rectangle12.Height * Projectile.scale;
                }
            }
            Vector2 vector36 = vector35;
            vector35 = Projectile.Center.Floor() + vector33 * Projectile.scale * 24f;
            rectangle12 = new Rectangle(0, 46, value58.Width, 18);
            int num128 = 18;
            if (flag7)
            {
                num128 = 9;
            }
            float num129 = num126;
            if (num126 > 0f)
            {
                float num130 = 0f;
                float num131 = num129 / (float)num128;
                num130 += num131 * 0.25f;
                vector35 += vector33 * num131 * 0.25f;
                for (int num132 = 0; num132 < num128; num132++)
                {
                    float num133 = num131;
                    if (num132 == 0)
                    {
                        num133 *= 0.75f;
                    }
                    Main.EntitySpriteDraw(value58, vector35 - Main.screenPosition + vector34, rectangle12, alpha11, rotation4, new Vector2(rectangle12.Width / 2, 0f), Projectile.scale, SpriteEffects.None);
                    num130 += num133;
                    vector35 += vector33 * num133;
                }
            }
            Main.EntitySpriteDraw(sourceRectangle: new Rectangle(0, 90, value58.Width, 48), texture: value58, position: vector36 - Main.screenPosition + vector34, color: alpha11, rotation: rotation4, origin: value58.Frame().Top(), scale: Projectile.scale, effects: SpriteEffects.None);
            return false;
        }
    }
}