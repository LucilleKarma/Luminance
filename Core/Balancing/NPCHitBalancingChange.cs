using System;

namespace Luminance.Core.Balancing
{
    [Obsolete("Balancing API will be removed in a future version")]
    public record NPCHitBalancingChange(int NPCType, params INPCHitBalancingRule[] BalancingRules);
}
