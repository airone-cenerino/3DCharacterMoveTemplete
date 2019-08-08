using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * カメラの追従
 */

namespace PlayerMoveTemplete.Player.Camera
{
    public class CameraMoveBehavior : MonoBehaviour
    {
        [SerializeField] private float folloeUpSpeed = 4.0f;        // カメラの追従スピード
        [SerializeField] private Transform targetTransform = null;  // カメラが追いかける対象

        private Vector3 cameraPlayerGap;    // 対象とカメラとの相対座標

        private void Start()
        {
            cameraPlayerGap = transform.position - targetTransform.position;    // 初めに対象との相対座標を取得。
        }

        private void FixedUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, targetTransform.position + cameraPlayerGap, folloeUpSpeed * Time.deltaTime);  //滑らかに補間して対象を追いかける。
        }
    }
}
