using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using ReLogic.Content;
using Terraria;
using ShadowlightMod.Rarities;
using ShadowlightMod.Sounds;
using ShadowlightMod.Items.Dyes;
using ShadowlightMod.Assets;

namespace ShadowlightMod
{
    // Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
    public class ShadowlightMod : Mod
    {
        public override void Load()
        {
            #region Shaders
            if (!Main.dedServ) // if this isn't a dedicated server (which shaders can't run on)
            {
                // This is where we load our shader, and add it to the filter.

                Asset<Effect> dyeShader = Assets.Request<Effect>("Assets/AutoloadedEffects/Shaders/Dyes/FirstShader");

                // To add a dye, add this line for every dye you want to add, changing the name and the shader used.
                // Reference the corresponding name of your pass in the technique for the last parameter. In this case, it's ArmorNoise.
                GameShaders.Armor.BindShader(ModContent.ItemType<FirstDye>(), new ArmorShaderData(dyeShader, "ArmorNoise"));

                // If your dye takes specific parameters such as color, you can append them after binding the shader.
                // Reference https://github.com/tModLoader/tModLoader/wiki/Expert-Shader-Guide if you get stuck.

                Asset<Effect> chromaticAberration = Assets.Request<Effect>("Assets/AutoloadedEffects/Shaders/OverlayModifiers/ChromaticAberration");

                GameShaders.Armor.BindShader(ModContent.ItemType<TestDye>(), new ArmorShaderData(chromaticAberration, "ChromaticAberration"));
                Asset<Effect> anotherDye = Assets.Request<Effect>("Assets/AutoloadedEffects/Shaders/Dyes/AnotherDye");
                GameShaders.Armor.BindShader(ModContent.ItemType<BSDye>(), new ArmorShaderData(anotherDye, "SomeBullshit"));
                // Reminder to self: you don't need to do a .UseImage() if it's just gonna be a dye because you can assign the image in the dye code.

                Asset<Effect> FirstScreenShader = Assets.Request<Effect>("Assets/AutoloadedEffects/Shaders/OverlayModifiers/FirstScreenShader");
                Filters.Scene["ShadowlightMod:FirstScreenShader"] = new Filter(new ScreenShaderData(FirstScreenShader, "FirstScreenShader"), EffectPriority.VeryHigh);
                Filters.Scene["ShadowlightMod:FirstScreenShader"].GetShader().UseColor(Color.Blue); // controls tint color
                Filters.Scene["ShadowlightMod:FirstScreenShader"].GetShader().UseOpacity(0.3f); // controls tint intensity

                Asset<Effect> BWShader = Assets.Request<Effect>("Assets/AutoloadedEffects/Shaders/OverlayModifiers/BWShader");
                Filters.Scene["ShadowlightMod:BWShader"] = new Filter(new ScreenShaderData(BWShader, "BWShader"), EffectPriority.VeryHigh);
                Filters.Scene["ShadowlightMod:BWShader"].GetShader().UseOpacity(1.0f); // controls tint intensity
                
            }
            #endregion Shaders
        }
    }
}
