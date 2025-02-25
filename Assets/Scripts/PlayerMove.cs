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


        private void Awake()
        {
            _moveAction = new();
            _moveAction.Enable();
            _moveAction.Player.Move.started += OnMove;
            _moveAction.Player.Move.performed += OnMove;
            _moveAction.Player.Move.canceled += OnMove;
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (WebManager.Instance.IsPlayer(data.ID) && !start)
            {
                start = true;
                _moveAction = new();
                _moveAction.Enable();
                _moveAction.Player.Move.started += OnMove;
                _moveAction.Player.Move.performed += OnMove;
                _moveAction.Player.Move.canceled += OnMove;
                _rb = GetComponent<Rigidbody2D>();
            }
            if (WebManager.Instance.IsPlayer(data.ID))
                SendTransForm();
            
        }


        public void OnMove(InputAction.CallbackContext context)
        {
            _rb.linearVelocity = context.ReadValue<Vector2>() * _speed;
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