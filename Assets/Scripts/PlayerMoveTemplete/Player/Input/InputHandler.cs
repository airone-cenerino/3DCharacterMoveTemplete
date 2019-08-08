using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using PlayerMoveTemplete.Player.Behavior;

/*
 *  Input管理
 */

namespace PlayerMoveTemplete.Player.Input
{
    [RequireComponent(typeof(BehaviorController))]

    public class InputHandler : SerializedMonoBehaviour
    {
        private BehaviorController behaviorController;


        void Start()
        {
            behaviorController = GetComponent<BehaviorController>();
        }


        void Update()
        {
            Vector2 moveDirection = new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), UnityEngine.Input.GetAxisRaw("Vertical")).normalized;   // WASDの入力を正規のVector2に変換
            behaviorController.Move(moveDirection); 
            behaviorController.Dash(UnityEngine.Input.GetKey(KeyCode.LeftShift));

            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                behaviorController.Jump();
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
            {
                behaviorController.ForcedMove(new Vector3(100, 0, 100));
            }
        }
    }
}
