using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] public PlayerAim PlayerAim = new PlayerAim();
        [SerializeField] public PlayerMove PlayerMove = new PlayerMove();
        [SerializeField] public PlayerAnim PlayerAnim = new PlayerAnim();

        // Start is called before the first frame update
        private void Start()
        {
            PlayerAim.Owner = this;
            PlayerMove.Owner = this;
            PlayerAnim.Owner = this;
        }

        // Update is called once per frame
        private void Update()
        {
            PlayerAim.Run();
            PlayerMove.Run();
            PlayerAnim.Run();
        }
    }
}