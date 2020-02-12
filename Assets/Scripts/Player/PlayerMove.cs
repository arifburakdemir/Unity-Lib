using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [Serializable]
    public class PlayerMove
    {
        private Player _owner;
        private Rigidbody _compRigidBody;


        [Header("Data")] [SerializeField] private MoveData _data;
        public MoveData Data => _data;

        [Header("General Settings")] public Transform CameraTransform = null;
        public float Speed = 2;
        public bool Enable = true;
        public bool IsDebug = false;

        public Player Owner
        {
            set
            {
                _owner = value;
                _compRigidBody = _owner.GetComponent<Rigidbody>();
            }
        }

        public MoveData Run(bool apply = true)
        {
            if (!Enable) return null;

            _data.InputVec.x = Input.GetAxisRaw("Horizontal");
            _data.InputVec.z = Input.GetAxisRaw("Vertical");

            _data.MoveVec = Quaternion.Euler(0, CameraTransform.eulerAngles.y, 0) * _data.InputVec;
            _data.MoveVec = _data.MoveVec.normalized;
            
            if (apply)
                _compRigidBody.velocity = _data.MoveVec * Speed;

            if (IsDebug)
                Debug.DrawRay(_owner.transform.position, _data.MoveVec);

            return _data;
        }
    }

    [Serializable]
    public class MoveData
    {
        public Vector3 InputVec;
        public Vector3 MoveVec;
    }
}