using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    [System.Serializable]
    public class Tab
    {
        public GameObject TabPanel;
        public RectTransform Line;
    }

    [SerializeField] private Tab[] _tabs;
    [SerializeField] private Image[] _tabsState;
    private int _currentTabNum = 0;

    private void Awake()
    {
        ChangeTab(1);
    }

    public void ChangeTab(int tabnum)
    {
        if (tabnum == _currentTabNum)
            return;

        if (tabnum < 0 || tabnum >= _tabs.Length)
            return;

        Tween.StopAll(_tabsState[_currentTabNum]);
        Tween.StopAll(_tabsState[tabnum]);
        Tween.StopAll(_tabs[_currentTabNum].Line);
        Tween.StopAll(_tabs[tabnum].Line);

        Tween.Alpha(_tabsState[tabnum], 1f, 0.45f);
        Tween.Alpha(_tabsState[_currentTabNum], 0f, 0.45f);

        _tabs[tabnum].TabPanel.SetActive(true);
        _tabs[_currentTabNum].TabPanel.SetActive(false);

        Tween.ScaleX(_tabs[tabnum].Line, 1f, 0.5f);
        Tween.ScaleX(_tabs[_currentTabNum].Line, 0f, 0.5f);

        _currentTabNum = tabnum;
    }
}
