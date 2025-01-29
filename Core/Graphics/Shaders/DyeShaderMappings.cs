using System.Collections.Generic;
using Terraria.ModLoader;

namespace Luminance.Core.Graphics
{
    internal class DyeShaderMappings : ModSystem
    {
        /// <summary>
        /// Represents a mapping from item ID to shader.
        /// </summary>
        internal static readonly Dictionary<string, int> ShaderToDyeID = [];
    }
}
