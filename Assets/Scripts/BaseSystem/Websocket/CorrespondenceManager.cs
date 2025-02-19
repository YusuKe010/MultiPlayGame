using System;
using System.Collections.Generic;
using BaseSystem;
using UnityEngine;
using WebSocketSharp;

public class CorrespondenceManager : MonoBehaviour
{
    WebSocket _webSocket = new WebSocket("ws://localhost:8080");

    public event Action<object> OnMessage;
    public event Action<WebSocket> OnUpdate;
    public event Action OnClose;

    private void Start()
    {
        _webSocket.OnMessage += SendMessage;
        _webSocket.Connect();
    }

    private void Update()
    {
        OnUpdate?.Invoke(_webSocket);
    }

    private void OnDestroy()
    {
        _webSocket.Close();
        _webSocket = null;
    }

    private void SendMessage(object sender, MessageEventArgs args )
    {
        
        
    }
}
