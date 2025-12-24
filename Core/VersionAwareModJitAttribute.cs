using System;
using System.Linq;
using System.Reflection;
using Terraria.ModLoader;

namespace Luminance.Core
{
    internal sealed class VersionAwareModJitAttribute(params string[] names) : MemberJitAttribute
    {
        public override bool ShouldJIT(MemberInfo member)
        {
            return names.All(HasMod);
        }

        private static bool HasMod(string modName)
        {
            var split = modName.Split('@', 2);
            
            if (split.Length == 1)
            {
                return ModLoader.HasMod(split[0]);
            }

            return ModLoader.TryGetMod(split[0], out var mod) && mod.Version >= Version.Parse(split[1]);
        }
    }
}
