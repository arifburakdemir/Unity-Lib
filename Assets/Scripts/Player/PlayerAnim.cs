using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerAnim
    {
        private Player _owner;
        private Animator _animator;

        public Player Owner
        {
            set
            {
                _owner = value;
                _animator = value.GetComponent<Animator>();
            }
        }

        public void Run()
        {
            MoveAnim();
        }

        private void MoveAnim()
        {
            Vector3 moveDir = _owner.PlayerMove.Data.InputVec;
            Quaternion aimQua = _owner.PlayerAim.Data.DirecitonQua;

            Debug.DrawRay(_owner.transform.position, moveDir, Color.cyan);

            Vector3 rotDir = aimQua * moveDir ;

            Debug.DrawRay(_owner.transform.position, rotDir, Color.magenta);
            //rotDir.x *= Math.Abs(moveDir.x) < 0.01f ? 1 : -1;
            //rotDir.z *= Math.Abs(moveDir.z) < 0.01f ? 1 : -1;

            _animator.SetFloat("VelX", rotDir.x);
            _animator.SetFloat("VelZ", rotDir.z);
        }
    }
}