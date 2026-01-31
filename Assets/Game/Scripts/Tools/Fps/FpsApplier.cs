using Tools.EventBus;
using UnityEngine;

namespace Tools
{
    public static class FpsApplier
    {
        [RuntimeInitializeOnLoadMethod]
        public static void Initialize() => EBus.Subscribe<int>("OnFpsChange", fps => Application.targetFrameRate = fps);
    }
}