using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerMoveTemplete.Player.Behavior;


/*
 * プレイヤーのアニメーションを管理する。
 */

namespace PlayerMoveTemplete.Player.Animation
{
    [RequireComponent(typeof(PlayerMoveControler))]

    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator = null;

        private PlayerMoveControler playerMoveControler;
        private PlayerAnimationStateMachineBehavior playerAnimationStateMachineBehavior;

        private void Start()
        {
            playerMoveControler = GetComponent<PlayerMoveControler>();
            playerAnimationStateMachineBehavior = animator.GetBehaviour<PlayerAnimationStateMachineBehavior>();
        }

        private void FixedUpdate()
        {
            if(playerMoveControler.IsDash && playerMoveControler.MoveDirection.magnitude == 1f)
            {
                animator.SetBool("Run", true);
            }
            else
            {
                animator.SetBool("Run", false);
            }

            if (playerMoveControler.IsJumping && !playerAnimationStateMachineBehavior.IsPlayingJumpAnimation)
            {
                animator.SetTrigger("Jump");
            }
        }
    }
}
