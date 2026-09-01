using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace ShadowlightMod.Projectiles.Ranged

{
    public class EngineersMegaphoneProjectile : ModProjectile
    {
        public static readonly SoundStyle CriticalStrike = new SoundStyle("ShadowlightMod/Sounds/Items/MegaphoneBRAINZ")
        {
            Volume = 5.5f,
            PitchVariance = 0f,
        };
        public override void SetDefaults()
        {
            Projectile.damage = 34;
            Projectile.CritChance = 6;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.scale = 2f;
            Projectile.aiStyle = ProjAIStyleID.Explosive;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.alpha = 0;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;

            AIType = ProjectileID.Grenade;
        }
        public override void AI()
        {

        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (hit.Crit)
            {
                SoundEngine.PlaySound(CriticalStrike, Projectile.position);
                target.AddBuff(BuffID.Confused, 180);
                Projectile.Resize(22, 22);
            }
        }
    }
}