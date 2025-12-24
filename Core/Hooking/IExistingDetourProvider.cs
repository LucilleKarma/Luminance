using System;

namespace Luminance.Core.Hooking
{
    /// <summary>
    /// Provides a class with automanaged implementation of an existing tModLoader detour(s).
    /// </summary>
    [Obsolete("Hook wrapper APIs provide no direct benefits over HookGen hooks or Hook/ILHook usage and will be removed in a future version, use On_* APIs")]
    public interface IExistingDetourProvider
    {
        /// <summary>
        /// Subscribe to the detour here.
        /// </summary>
        void Subscribe();

        /// <summary>
        /// Unsubscribe to the detour here.
        /// </summary>
        void Unsubscribe();
    }
}
