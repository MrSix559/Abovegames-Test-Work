using UnityEngine;
using PrimeTween;

public class UIEffector : Tools.Singleton<UIEffector>
{
    public RectTransform PremiumPanel; // КАСТЫЛЬ :D
    #region  OpenPanel
    public void OpenPanelWithAnim(RectTransform panelOpen, float duration, Ease ease)
    {
        Tween.UIAnchoredPositionY(panelOpen, 0, duration: duration, ease);
    }
    public void OpenPanelWithAnim(RectTransform panelOpen)
    {
        Tween.UIAnchoredPositionY(panelOpen, 0, duration: 0.5f, Ease.InOutQuad);
    }
    #endregion
    #region ClosePanel
    public void ClosePanelWithAnim(RectTransform panelClose, float duration, Ease ease)
    {
        Tween.UIAnchoredPositionY(panelClose, -3000f, duration: duration, ease: ease);
    }
    public void ClosePanelWithAnim(RectTransform panelClose)
    {
        Tween.UIAnchoredPositionY(panelClose, -3000f, duration: 0.5f, Ease.InOutQuad);
    }
    #endregion
    public void ClickButton(RectTransform button)
    {
        Tween.Scale(button, Vector3.one * 1.1f, 0.08f, Ease.OutQuad);
    }

    public void UnClickButton(RectTransform button)
    {
        Tween.Scale(button, Vector3.one, 0.08f, Ease.OutQuad);
    }
}
