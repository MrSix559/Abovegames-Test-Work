using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

public class PreviewImage : MonoBehaviour
{
    [SerializeField] private Image _imagePreview;
    [SerializeField] private CanvasGroup _group;
    private void OnEnable()
    {
        Tools.EventBus.EBus.Subscribe<Sprite>("OnPreviewImage", Preview);
        Tools.EventBus.EBus.Subscribe("OnClosePreview", ClosePreview);
    }
    private void OnDisable()
    {
        Tools.EventBus.EBus.UnSubscribe<Sprite>("OnPreviewImage", Preview);
        Tools.EventBus.EBus.UnSubscribe("OnClosePreview", ClosePreview);
    }

    public void Preview(Sprite sprite)
    {
        if(sprite == null) return;
        Tween.Alpha(_group, 1f, 0.5f);
        _imagePreview.sprite = sprite;
        _group.blocksRaycasts = true;
        _group.interactable = true;
    }
    public void ClosePreview()
    {
        Tween.Alpha(_group, 0f, 0.5f);
        _imagePreview.sprite = null;
        _group.blocksRaycasts = false;
        _group.interactable = false;
    }
}
