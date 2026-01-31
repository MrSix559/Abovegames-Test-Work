using System.Collections.Generic;
using UnityEngine;

namespace Tools
{
    public class PauseManager : MonoBehaviour
    {
        private readonly List<IPausable> _pausables = new List<IPausable>();
        public void Register(IPausable pausable)
        {
            if (!_pausables.Contains(pausable))
                _pausables.Add(pausable);
        }

        public void UnRegister(IPausable pausable)
        {
            _pausables.Remove(pausable);
        }

        public void SetPaused(bool paused)
        {
            if (_pausables.Count < 1) return;

            foreach (IPausable p in _pausables)
            {
                if (paused) p.OnPause();
                else p.OnResume();
            }
        }
    }
}