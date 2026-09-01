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
using Terraria.Graphics.Effects;
using static Terraria.Main;
using static Terraria.ModLoader.ModContent;



namespace ShadowlightMod.ShadowlightPlayer
{
    public class ShadowlightPlayer : ModPlayer
    {
        public override void PostUpdate()
        {

            if (dedServ) return;

            // If the filter is running but the player lacks the buff, turn it off
            if (Filters.Scene["ShadowlightMod:FirstScreenShader"].IsActive() && !Player.HasBuff<TintBuff>())
            {
                Filters.Scene.Deactivate("ShadowlightMod:FirstScreenShader");
            }

        }
    }
}