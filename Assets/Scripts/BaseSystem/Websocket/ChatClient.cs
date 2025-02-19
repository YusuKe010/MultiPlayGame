using System;
using TMPro;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using UnityEngine.UIElements;
using WebSocketSharp;

public class ChatClient : MonoBehaviour
{
    private WebSocket ws;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_Text logText;
    ContentSizeFitter fitter;

    private void Start()
    {
        fitter = logText.GetComponent<ContentSizeFitter>();

        ws = new WebSocket("ws://localhost:8080");
        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message received from " + ((WebSocket)sender).Url + ", Data " + e.Data);
            logText.SetText(e.Data);
            fitter.SetLayoutHorizontal();
            fitter.SetLayoutVertical();
            LayoutRebuilder.MarkLayoutForRebuild(logText.rectTransform);
            LayoutRebuilder.ForceRebuildLayoutImmediate(logText.gameObject.GetComponent<RectTransform>());
        };
        ws.Connect();
    }

    private void Update()
    {
        logText.ForceMeshUpdate();
       
    }

    public void ChatSend()
    {
        if (ws == null) return;
        if (inputField.text != string.Empty)
        {
            ws.SendAsync(inputField.text, (aa) => { inputField.text = string.Empty; });
        }
    }

    private void OnDestroy()
    {
        ws.Close();
        ws = null;
    }
}