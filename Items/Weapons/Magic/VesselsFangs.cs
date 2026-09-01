using ShadowlightMod.Projectiles.Magic;
using ShadowlightMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ShadowlightMod.Items.Weapons.Magic
{
    public class VesselsFangs : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 80;
            Item.height = 76;
            // TODO: Fix the hitbox/drawing offset in the JawTop and JawBottom projectiles
            Item.useTime = 60;
            Item.useAnimation = 60;
            Item.damage = 356;
            Item.mana = 64;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Magic;
            Item.shoot = ProjectileID.FireworkFountainRainbow; // Credit to AkiraPlayz for figuring out that this projectile essentially voids itself, thus it doesn't fire a random birch chip that does a ton of damage
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.rare = ModContent.RarityType<BlightGreen>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y - 150f), new Vector2(0, -12f), ModContent.ProjectileType<JawTop>(), damage, knockback, player.whoAmI);
            Projectile.NewProjectile(source, new Vector2(Main.MouseWorld.X, Main.MouseWorld.Y + 150f), new Vector2(0, 12f), ModContent.ProjectileType<JawBottom>(), damage, knockback, player.whoAmI);
            SoundEngine.PlaySound(SoundID.NPCDeath13);
            return false;
        }

    }
}