using UnityEngine;
using UnityEngine.AddressableAssets;

namespace BaseSystem
{
    public enum PLayerType
    {
        Player1,
        PLayer2
    }
    public class PlayerManager : MonoBehaviour
    {
        private GameObject playerPrefab;
        private async void Initialized()
        {
            playerPrefab = Addressables.LoadAssetAsync<GameObject>("Assets/PlayerPrefab").Result;
            
            
        }

        public void InstantiatePlayer()
        {
            GameObject player = Instantiate(playerPrefab);
        }
    }
}