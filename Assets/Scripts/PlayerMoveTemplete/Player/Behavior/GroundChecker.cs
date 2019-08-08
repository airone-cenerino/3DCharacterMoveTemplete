using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * プレイヤーが接地しているか調べる
 */

namespace PlayerMoveTemplete.Player.Behavior
{
    public class GroundChecker : MonoBehaviour
    {
        public bool IsGround { private set; get; } = true;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Ground")
                IsGround = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Ground")
                IsGround = false;
        }
    }
}