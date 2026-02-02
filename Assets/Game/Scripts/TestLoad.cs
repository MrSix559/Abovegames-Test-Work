using UnityEngine;
using UnityEngine.UI;
public class TestLoad : MonoBehaviour
{
    [SerializeField] private ImageSlot[] _targetImage;
    /*private async void Start()
    {
        for(int i = 0; i < _targetImage.Length; i++)
        {
            Sprite sprite = await ImageLoader.LoadSpriteAsync(i + 1);
            _targetImage[i].SetSprite(sprite);
        }
    }*/
}
