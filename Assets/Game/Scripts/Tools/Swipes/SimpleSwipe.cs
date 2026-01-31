using UnityEngine;
using UnityEngine.Events;

namespace Tools
{
    public class SimpleSwipe : MonoBehaviour
    {
        [SerializeField] private float _swipeThreshold = 50f;
        private Vector2 _startPos;

        #region Swipe Direction Events
        [SerializeField] private UnityEvent _onSwipeLeft;
        [SerializeField] private UnityEvent _onSwipeRight;
        [SerializeField] private UnityEvent _onSwipeDown;
        [SerializeField] private UnityEvent _onSwipeUp;
        #endregion

        public void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (Input.GetMouseButtonDown(0))
                _startPos = Input.mousePosition;

            if (Input.GetMouseButtonUp(0))
            {
                Vector2 endPos = Input.mousePosition;
                CheckSwipeDirection(endPos);
            }
#else
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                    _startPos = touch.position;

                if (touch.phase == TouchPhase.Ended)
                    CheckSwipeDirection(touch.position);
            }
#endif
        }

        private void CheckSwipeDirection(Vector2 endPos)
        {
            Vector2 delta = endPos - _startPos;

            if (delta.magnitude < _swipeThreshold)
                return;

            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                if (delta.x > 0)
                    _onSwipeRight?.Invoke();
                else
                    _onSwipeLeft?.Invoke();
            }
            else
            {
                if (delta.y > 0)
                    _onSwipeUp?.Invoke();
                else
                    _onSwipeDown?.Invoke();
            }
        }
    }
}
