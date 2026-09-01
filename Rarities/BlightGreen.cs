using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using ReLogic.Graphics;
using Terraria;
using Terraria.UI.Chat;
using Daybreak.Common.Features.Rarities;
using Daybreak.Common.Rendering;

namespace ShadowlightMod.Rarities

// Disclaimer: I never could've done ANY of this without help from the Nightshade Discord. Support them and their projects.

{
    public class BlightGreen : ModRarity, IRarityTextRenderer
    {
        public override Color RarityColor => new Color(152, 231, 83, 255); // Blighted Green Color

        void IRarityTextRenderer.RenderText(SpriteBatch sb, DynamicSpriteFont font, string text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, RarityDrawContext drawContext, float maxWidth, float spread)
        {
            Matrix originalMatrix = drawContext.Ui ? Main.UIScaleMatrix : Main.GameViewMatrix.TransformationMatrix;
            sb.End(out var snapshot);
            var customEffect = ModContent.Request<Effect>("ShadowlightMod/Assets/AutoloadedEffects/Shaders/OverlayModifiers/ChromaticAberration");
            sb.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullNone, customEffect.Value, originalMatrix);
            ChatManager.DrawColorCodedStringWithShadow(sb, font, text, position, color, rotation, origin, scale, maxWidth, spread);
            sb.End();
            sb.Begin(in snapshot);
            ChatManager.DrawColorCodedStringWithShadow(sb, font, text, position, RarityColor, rotation, origin, scale, maxWidth, spread);
        }
        public override int GetPrefixedRarity(int offset, float valueMult) => offset switch // This is Rarity 12, above Purple
        {
            -2 => ItemRarityID.Red,
            -1 => ItemRarityID.Purple,
            _ => Type,
        };
    }
}