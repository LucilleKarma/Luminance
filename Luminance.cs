﻿global using static System.MathF;
global using static Luminance.Assets.MiscTexturesRegistry;
global using static Luminance.Common.Utilities.Utilities;
global using static Microsoft.Xna.Framework.MathHelper;
using Luminance.Core.Graphics;
using Luminance.Core.Hooking;
using Terraria.ModLoader;

namespace Luminance
{
    /// <summary>
    /// The central mod type for the Luminance library.
    /// </summary>
    public sealed class Luminance : Mod
    {
        /// <summary>
        /// Handles all necessary manual unloading effects for the library.
        /// </summary>
        public override void Unload() => ManagedILEdit.UnloadEdits();

        /// <summary>
        /// Handles all necessary loading effects for the library, after all mods have loaded and all dependencies have been established.
        /// </summary>
        public override void PostSetupContent()
        {
            // Go through every mod and check for effects to autoload.
            foreach (Mod mod in ModLoader.Mods)
            {
                HookHelper.LoadHookInterfaces(mod);
                ShaderManager.LoadShaders(mod);
                AtlasManager.InitializeModAtlases(mod);
                ParticleManager.InitializeManualRenderers(mod);
                ShaderRecompilationMonitor.LoadForMod(mod);
            }

            // Mark loading operations as finished.
            ShaderManager.HasFinishedLoading = true;
        }
    }
}
