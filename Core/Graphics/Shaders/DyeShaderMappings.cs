using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace Luminance.Core.Graphics
{
    internal class DyeShaderMappings : ModSystem
    {
        /// <summary>
        /// Represents a mapping from item ID to shader.
        /// </summary>
        internal static readonly Dictionary<string, int> ShaderToDyeID = [];

        /// <summary>
        /// Represents the set of all textures to use for the purposes of rendering a given dye mapping.
        /// </summary>
        internal static readonly Dictionary<string, List<ManagedScreenFilter.DeferredTexture>> ShaderTextureCache = [];

        public override void OnModLoad() => On_ArmorShaderData.Apply += PassParametersToShader;

        private static void PassParametersToShader(On_ArmorShaderData.orig_Apply orig, ArmorShaderData self, Entity entity, DrawData? drawData)
        {
            orig(self, entity, drawData);

            foreach (var kv in ShaderToDyeID)
            {
                int dyeID = kv.Value;
                if (GameShaders.Armor.GetShaderFromItemId(dyeID) == self && ShaderManager.TryGetShader(kv.Key, out ManagedShader shader))
                    CopyArmorShaderParameters(shader, entity, drawData);
            }
        }

        private static void CopyArmorShaderParameters(ManagedShader shader, Entity entity, DrawData? drawData)
        {
            if (drawData.HasValue)
            {
                DrawData draw = drawData.Value;
                Vector4 frame = (!draw.sourceRect.HasValue) ? new Vector4(0f, 0f, draw.texture.Width, draw.texture.Height) : new Vector4(draw.sourceRect.Value.X, draw.sourceRect.Value.Y, draw.sourceRect.Value.Width, draw.sourceRect.Value.Height);

                shader.TrySetParameter("uSourceRect", frame);
                shader.TrySetParameter("uLegacyArmorSourceRect", frame);
                shader.TrySetParameter("uWorldPosition", Main.screenPosition + draw.position);
                shader.TrySetParameter("uImageSize0", new Vector2(draw.texture.Width, draw.texture.Height));
                shader.TrySetParameter("uLegacyArmorSheetSize", new Vector2(draw.texture.Width, draw.texture.Height));
                shader.TrySetParameter("uRotation", draw.rotation * (draw.effect.HasFlag(SpriteEffects.FlipHorizontally) ? (-1f) : 1f));
                shader.TrySetParameter("uDirection", (!draw.effect.HasFlag(SpriteEffects.FlipHorizontally)) ? 1 : (-1));
            }
            else
            {
                Vector4 frame = new(0f, 0f, 4f, 4f);
                shader.TrySetParameter("uSourceRect", frame);
                shader.TrySetParameter("uLegacyArmorSourceRect", frame);
                shader.TrySetParameter("uRotation", 0f);
            }

            if (entity != null)
                shader.TrySetParameter("uDirection", (float)entity.direction);

            if (entity is Player player)
            {
                Rectangle bodyFrame = player.bodyFrame;
                shader.TrySetParameter("uLegacyArmorSourceRect", new Vector4(bodyFrame.X, bodyFrame.Y, bodyFrame.Width, bodyFrame.Height));
                shader.TrySetParameter("uLegacyArmorSheetSize", new Vector2(40f, 1120f));
            }

            if (ShaderTextureCache.TryGetValue(shader.Name, out List<ManagedScreenFilter.DeferredTexture> textures))
            {
                for (int i = 0; i < textures.Count; i++)
                    shader.SetTexture(textures[i].Texture, textures[i].Index, textures[i].SamplerState);
            }

            for (int i = 1; i < 16; i++)
            {
                if (Main.instance.GraphicsDevice.Textures[i] is Texture2D texture)
                    shader.TrySetParameter($"uImageSize{i}", texture.Size());
            }
        }
    }
}
