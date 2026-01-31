using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public static class ImageLoader
{
    private const string BaseUrl = "http://data.ikppbb.com/test-task-unity-data/pics/";

    public static async UniTask<Sprite> LoadSpriteAsync(int imageIndex)
    {
        string url = $"{BaseUrl}{imageIndex}.jpg";

        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Ошибка загрузки изображения: {request.error}\n{url}");
                return null;
            }

            Texture2D texture = DownloadHandlerTexture.GetContent(request);

            return Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );
        }
    }
}
