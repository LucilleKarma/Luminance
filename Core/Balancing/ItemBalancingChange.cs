using System;
using Terraria;

namespace Luminance.Core.Balancing
{
    [Obsolete("Balancing API will be removed in a future version")]
    public record ItemBalancingChange(int ItemType, BalancePriority Priority, Func<bool> ShouldApply, Action<Item> PerformBalancing);
}
