using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Tools
{
    public static class LayerDropdownUtility
    {
        public static IEnumerable<ValueDropdownItem<int>> GetLayerInts()
        {
            for (int i = 0; i < 32; i++)
            {
                string layerName = LayerMask.LayerToName(i);
                if (!string.IsNullOrEmpty(layerName))
                {
                    yield return new ValueDropdownItem<int>(layerName, i);
                }
            }
        }
    }
}