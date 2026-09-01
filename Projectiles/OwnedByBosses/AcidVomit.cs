using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ShadowlightMod.Projectiles.OwnedByBosses;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Daybreak.Common.Rendering;

namespace ShadowlightMod.Projectiles.OwnedByBosses;

public class AcidVomit : ModProjectile
{
    public override void SetStaticDefaults()
    {
        Main.projFrames[Type] = 3;
    }

    public override void SetDefaults()
    {
        Projectile.width = 42;
        Projectile.height = 42;
        Projectile.scale = 2f;
        Projectile.penetrate = -1;
        Projectile.tileCollide = false;
        Projectile.ignoreWater = true;
        Projectile.hostile = true;
    }
    public override bool PreDraw(ref Color lightColor)
    {
        var customEffect = ModContent.Request<Texture2D>("ShadowlightMod/Assets/Textures/Noise/AcidNoise").Value;
        Main.spriteBatch.End(out var snapshot);
        Main.instance.GraphicsDevice.Textures[1] = customEffect;
        Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        return true;
    }








    public override void AI()
    {
        Projectile.velocity.Y *= 1.02f;
        if (Projectile.velocity.Y > 10f)
        {
            Projectile.velocity.Y = 10f;
        }

        Projectile.frameCounter += 1;
        if (Projectile.frameCounter >= 3)
        {
            Projectile.frameCounter = 0;
            Projectile.frame = ++Projectile.frame % Main.projFrames[Type];
        }
        Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
    }
    public override void PostDraw(Color lightColor)
    {
        Main.spriteBatch.End(out var snapshot);
        Main.instance.GraphicsDevice.Textures[1] = null;
        Main.spriteBatch.Begin(snapshot);
    }
}