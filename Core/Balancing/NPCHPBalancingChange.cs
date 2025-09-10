using System;

namespace Luminance.Core.Balancing
{
    [Obsolete("Balancing API will be removed in a future version")]
    public record NPCHPBalancingChange(int NPCType, int HP, BalancePriority Priority, Func<bool> ShouldApply);
}
