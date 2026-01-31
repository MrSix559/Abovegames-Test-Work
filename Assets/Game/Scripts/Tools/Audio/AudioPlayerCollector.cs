using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Tools
{
    public class AudioPlayerCollector : MonoBehaviour
    {
        public List<AudioPlayer> AudioPlayers = new List<AudioPlayer>();

        private async void Awake()
        {
            await FindObjects();
        }

        private async UniTask FindObjects()
        {
            await UniTask.Delay(500);
            AudioPlayers.Clear();
            try { AudioPlayers.AddRange(FindObjectsOfType<AudioPlayer>(false)); }
            catch(OperationCanceledException) {}
        }
    }   
}
