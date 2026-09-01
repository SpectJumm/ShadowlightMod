using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace ShadowlightMod.Projectiles.Melee
{
    public class ToothofCthulhuProjectile : ModProjectile
    {

        public bool appliedbuff = false;

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Shroomerang);
            Projectile.width = 35;
            Projectile.height = 35;
            Projectile.damage = 22;
            Projectile.scale = 0.5f;
            Projectile.aiStyle = ProjAIStyleID.Boomerang;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
        }

        public override void AI()
        {
            if (Projectile.ai[0] != 0f) // Returning phase behavior
            {
                if (!appliedbuff)
                {
                    Projectile.damage = (int)(Projectile.damage * 1.5f); // Increase damage by 50% when returning
                    Projectile.knockBack *= 1.5f; // Increase knockback by 50% when returning
                    Projectile.velocity *= -3f;
                    appliedbuff = true; // Ensure the buff is only applied once
                }
            }
            // TODO: add afterimages for the returning phase (I tried but it didn't work)
        }
    }
}