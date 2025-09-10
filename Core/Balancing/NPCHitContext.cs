using System;

using Terraria;
using Terraria.ModLoader;

namespace Luminance.Core.Balancing
{
    [Obsolete("Balancing API will be removed in a future version")]
    public record NPCHitContext(int Pierce, int Damage, int? ProjectileIndex, int? ProjectileType, DamageClass ClassType)
    {
        public static NPCHitContext FromProjectile(Projectile proj) => new(proj.penetrate, proj.damage, proj.whoAmI, proj.type, proj.DamageType);
    }
}
