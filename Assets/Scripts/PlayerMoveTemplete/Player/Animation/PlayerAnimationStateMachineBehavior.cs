using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * プレイヤーのAnimatorControllerにアタッチする。
 * Jumpモーション中かどうかを確認する。
 */

namespace PlayerMoveTemplete.Player.Animation
{
    public class PlayerAnimationStateMachineBehavior : StateMachineBehaviour
    {
        public bool IsPlayingJumpAnimation { private set; get; } = false;   // ジャンプモーション中かどうか。

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.IsName("Jump"))
            {
                IsPlayingJumpAnimation = true;
            }
        }

        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (stateInfo.IsName("Jump"))
            {
                IsPlayingJumpAnimation = false;
            }
        }
    }
}