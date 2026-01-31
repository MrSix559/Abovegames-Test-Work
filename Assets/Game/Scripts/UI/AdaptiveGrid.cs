using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(GridLayoutGroup))]
    public class AdaptiveGrid : MonoBehaviour
    {
        private GridLayoutGroup _grid;

        void Awake()
        {
            _grid = GetComponent<GridLayoutGroup>();
            Apply();
        }

    #if UNITY_EDITOR
        void OnValidate()
        {
            if (_grid == null)
                _grid = GetComponent<GridLayoutGroup>();
            Apply();
        }
    #endif

        void Apply()
        {
            _grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;

            int minSide = Mathf.Min(Screen.width, Screen.height);

            if (minSide >= 1500)
                _grid.constraintCount = 3;
            else
                _grid.constraintCount = 2;
        }
    }
}
