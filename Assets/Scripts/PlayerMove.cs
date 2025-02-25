using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] float _speed;

        InputSystem_Actions _moveAction;
        Rigidbody2D _rb;
        public CommunicateData data = new();
        private bool start;
        private Vector3 _savePos;


        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_rb == null)
                _rb = GetComponent<Rigidbody2D>();

            if (WebManager.Instance.IsPlayer(data.ID))
            {
                OnMove();
            }

            if (transform.position != _savePos)
            {
                _savePos = transform.position;
                SendTransForm();
            }
        }


        public void OnMove()
        {
            Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            _rb.linearVelocity = move * _speed;
        }

        private async void SendTransForm()
        {
            data.Command = "SynchronizeTransform";
            data.JsonBody = JsonUtility.ToJson(transform.position);
            string json = JsonUtility.ToJson(data);
            await WebManager.Instance.SendCommand(json);
        }
    }
}