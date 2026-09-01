using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShadowlightMod.Buffs;
using ShadowlightMod.Items;
using ShadowlightMod.Projectiles;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Creative;
using Terraria.GameContent.NetModules;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Net;
using Terraria.WorldBuilding;
using static Terraria.Main;
using static Terraria.ModLoader.ModContent;

namespace ShadowlightMod.NPCs
{
    public class ShadowlightGlobalNPC : GlobalNPC
    {
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (npc.HasBuff(BuffType<BayonetStabbed>()) && item.DamageType == DamageClass.Ranged)
            {
                // Increase damage by 1.5x if the NPC is affected by BayonetStabbed and the player is using a ranged weapon
                modifiers.FinalDamage *= 1.5f;
            }
        }
    }
}