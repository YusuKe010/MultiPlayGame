using System;
using UnityEngine;
using WebSocketSharp;

namespace BaseSystem
{
    public class TransformCorrespondence : MonoBehaviour
    {
        int ID = 0;
        CorrespondenceManager _correspondenceManager;
        private void Awake()
        {
            _correspondenceManager = FindAnyObjectByType<CorrespondenceManager>();
            _correspondenceManager.OnMessage += ReceiveMessage;
        }

        public void SendTransform(WebSocket webSocket)
        {
            string json = JsonUtility.ToJson(transform);
            webSocket.Send(json);
        }

        private void ReceiveMessage(object json)
        {
            this.transform.position = JsonUtility.FromJson<Transform>((string)json).position;
        }
    }
}