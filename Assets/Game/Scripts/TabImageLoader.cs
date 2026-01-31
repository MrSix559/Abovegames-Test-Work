using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public enum LoadType {All, Odd, Even}

public class TabImageLoader : MonoBehaviour
{
    [SerializeField, BoxGroup("Scroll")] private ScrollRect _scrollRect;
    [SerializeField, BoxGroup("Scroll")] private RectTransform _viewport;

    [SerializeField, BoxGroup("Loading")] private LoadType _loadType;
    [SerializeField, BoxGroup("Loading")] private float _preloadOffset = 300f;
    [SerializeField, BoxGroup("Loading")] private ImageSlot[] _slots;

    private readonly Dictionary<int, Sprite> _spriteCache = new();
    private readonly HashSet<int> _loadingInProgress = new();

    private void Awake()
    {
        InitSlots();
        _scrollRect.onValueChanged.AddListener(_ => CheckVisibleSlots());
    }
    private void OnEnable() => CheckVisibleSlots();
    private void InitSlots()
    {
        int imageIndex = GetStartIndex();

        for (int i = 0; i < _slots.Length; i++)
        {
            _slots[i].Init(imageIndex);
            imageIndex += GetIndexStep();
        }
    }
    private int GetStartIndex()
    {
        return _loadType switch
        {
            LoadType.Even => 2,
            LoadType.Odd => 1,
            _ => 1
        };
    }
    private int GetIndexStep()
    {
        return _loadType == LoadType.All ? 1 : 2;
    }
    private bool IsPremium(int imageIndex) => imageIndex > 0 && imageIndex % 4 == 0;
    private void CheckVisibleSlots()
    {
        foreach (var slot in _slots)
        {
            if (slot.IsLoaded)
                continue;

            if (!IsVisible(slot.RectTransform()))
                continue;

            LoadSlotAsync(slot).Forget();
        }
    }
    private bool IsVisible(RectTransform slot)
    {
        Vector3[] slotCorners = new Vector3[4];
        Vector3[] viewCorners = new Vector3[4];

        slot.GetWorldCorners(slotCorners);
        _viewport.GetWorldCorners(viewCorners);

        float slotTop = slotCorners[1].y;
        float slotBottom = slotCorners[0].y;

        float viewTop = viewCorners[1].y + _preloadOffset;
        float viewBottom = viewCorners[0].y - _preloadOffset;

        return slotBottom < viewTop && slotTop > viewBottom;
    }
    private async UniTaskVoid LoadSlotAsync(ImageSlot slot)
    {
        int index = slot.ImageIndex;

        if (_loadingInProgress.Contains(index))
            return;

        _loadingInProgress.Add(index);

        if (_spriteCache.TryGetValue(index, out var cachedSprite))
        {
            slot.SetSprite(cachedSprite);
            _loadingInProgress.Remove(index);
            return;
        }

        Sprite sprite = await ImageLoader.LoadSpriteAsync(index);

        if (sprite != null)
        {
            _spriteCache[index] = sprite;
            slot.SetSprite(sprite);
            if (IsPremium(index))
                slot.ShowPremium();
        }

        _loadingInProgress.Remove(index);
    }
}
