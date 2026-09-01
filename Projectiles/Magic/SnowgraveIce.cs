using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShadowlightMod.Projectiles.Magic
{
    public class SnowgraveIce : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 4;
        }
        /*public override bool PreDraw(ref Color lightColor)
        {
            var sourceRectangle = new Rectangle(-13, 54 * Projectile.frame, 288, 56);
            Main.spriteBatch.Draw(TextureAssets.Projectile[Type].Value, Projectile.Center - Main.screenPosition, sourceRectangle, Lighting.GetColor(Projectile.Center.ToTileCoordinates()), Projectile.rotation, TextureAssets.Projectile[Type].Value.Size() / 2f, Projectile.scale, SpriteEffects.None, 0);
            return false;
        }*/ // AAAAA FUCK THIS PROJECTILE DRAWING OFFSET BS!!!

        public override void SetDefaults()
        {

            Projectile.width = 288;
            Projectile.height = 54;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.hide = false;
            Projectile.timeLeft = 18000;
            Projectile.alpha = 20;
            Projectile.light = 0.8f;
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 5;
        }
        public override void AI()
        {
            Projectile.frameCounter += 1;
            if (Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                Projectile.frame = ++Projectile.frame % Main.projFrames[Type];
            }
            Projectile.velocity.Y = -15f; // Make it negative to go up
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Frostburn, 240); // Apply Frostburn debuff for 4 seconds
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.IceRod, Scale: 1.5f);
                dust.noGravity = true;
                dust.velocity *= 2f;
            }
        }
    }
}