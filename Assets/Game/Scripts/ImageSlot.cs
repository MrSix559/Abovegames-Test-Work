using UnityEngine;
using UnityEngine.UI;
using PrimeTween;

public class ImageSlot : MonoBehaviour
{
    [SerializeField] private Image _picture;
    [SerializeField] private RectTransform _loadingImg;
    [SerializeField] private GameObject _premiumTag;
    private Tween _loadingTween;

    public int ImageIndex { get; private set; }
    public bool IsLoaded { get; private set; }

    private void Awake()
    {
        StartLoading();
    }
    public void Init(int imageIndex)
    {
        ImageIndex = imageIndex;
        IsLoaded = false;
    }
    private void StartLoading()
    {
        _loadingImg.gameObject.SetActive(true);

        _loadingTween = Tween.Custom(
            startValue: 0f,
            endValue: -360f,
            duration: 1f,
            onValueChange: value =>
            {
                var rot = _loadingImg.localEulerAngles;
                rot.z = value;
                _loadingImg.localEulerAngles = rot;
            },
            ease: Ease.Linear,
            cycles: -1
        );
    }
    private void StopLoading()
    {
        if (_loadingTween.isAlive)
            _loadingTween.Stop();

        _loadingImg.gameObject.SetActive(false);
    }
    public void SetSprite(Sprite sprite)
    {
        if (sprite == null)
            return;

        _picture.sprite = sprite;
        IsLoaded = true;
        StopLoading();
    }
    public void ShowPremium() => _premiumTag.gameObject.SetActive(true);
    public RectTransform RectTransform()
    {
        return (RectTransform)transform;
    }

    public void Preview() => Tools.EventBus.EBus.Invoke<Sprite>("OnPreviewImage", _picture.sprite);
    public void Premium() => UIEffector.Instance.OpenPanelWithAnim(UIEffector.Instance.PremiumPanel, 0.5f, Ease.InOutQuad);
}
