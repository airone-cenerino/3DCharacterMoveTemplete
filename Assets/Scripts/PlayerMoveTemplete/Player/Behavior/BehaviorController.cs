using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


/*
 * PlayerのBehavior管理。
 */

namespace PlayerMoveTemplete.Player.Behavior
{
    [RequireComponent(typeof(PlayerMoveControler))]

    public class BehaviorController : SerializedMonoBehaviour
    {
        private PlayerMoveControler playerMoveControler;

        private void Start()
        {
            playerMoveControler = GetComponent<PlayerMoveControler>();
        }

        public void Move(Vector2 direction)
        {
            playerMoveControler.Move(direction);
        }

        public void Dash(bool flg)
        {
            playerMoveControler.Dash(flg);
        }

        public void Jump()
        {
            playerMoveControler.Jump();
        }

        public void ForcedMove(Vector3 destination)
        {
            playerMoveControler.ForcedMove(destination);
        }
    }
}