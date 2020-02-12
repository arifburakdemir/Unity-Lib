using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [Serializable]
    public class PlayerAim
    {
        private Player _owner;

        [Header("General Settings")] public bool Enable = true;
        public LayerMask InputLayerMask = new LayerMask();
        public bool IsDebug = false;
        public float RotationSpeed = 20;

        [Header("Data")] [SerializeField] private AimData _data;
        public AimData Data => _data;

        public Transform Transform => _owner.transform;

        public Player Owner
        {
            set => _owner = value;
        }

        public AimData Run(bool apply = true)
        {
            // --- No hit any object
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, InputLayerMask)) return null;

            // --- Calculate Direction
            var vec = hit.point - Transform.position;
            var qua = Quaternion.LookRotation(vec);
            qua = Quaternion.Euler(0, qua.eulerAngles.y, 0);

            // --- Apply to Owner
            if (Enable && apply)
                _owner.transform.rotation =
                    Quaternion.Slerp(_owner.transform.rotation, qua, Time.deltaTime * RotationSpeed);

            // --- If Debug Enable
            if (IsDebug)
                Debug.DrawRay(Transform.position, vec);

            _data.DirectionVec = vec;
            _data.DirecitonQua = qua;

            return _data;
        }

        [ContextMenu("Generate Input Box")]
        public void GenerateComponents()
        {
            var curCol = _owner.gameObject.AddComponent<BoxCollider>();
            curCol.isTrigger = true;
            curCol.size = new Vector3(10, 0.1f, 10);
        }
    }

    [Serializable]
    public class AimData
    {
        public Vector3 DirectionVec;
        public Quaternion DirecitonQua = new Quaternion();
    }
}