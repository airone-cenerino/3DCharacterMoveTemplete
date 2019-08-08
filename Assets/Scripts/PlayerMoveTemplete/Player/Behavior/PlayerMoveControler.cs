using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


/*
 * プレイヤーの動きに関する部分。
 * 
 */

namespace PlayerMoveTemplete.Player.Behavior
{
    [RequireComponent(typeof(Rigidbody))]

    public class PlayerMoveControler : SerializedMonoBehaviour
    {
        [SerializeField] private float walkSpeed = 5f;
        [SerializeField] private float dashSpeed = 10f;
        [SerializeField] private float jumpPower = 5f;
        [SerializeField] private float gravityPower = 20f;
        [SerializeField] private float playerRotationSpeed = 5f;
        [SerializeField] private GameObject playerModel = null;
        [SerializeField] private GroundChecker groundChecker = null;


        private new Rigidbody rigidbody = null;
        public Vector2 MoveDirection { private set; get; } = Vector2.zero;  // 移動方向
        private Vector3 playerModelLastPositionXZ = Vector3.zero;           // 1フレーム前のプレイヤーモデルの位置(XZ成分)
        public bool IsDash { private set; get; } = false;                   // ダッシュ中か
        public bool willJump { private set; get; } = false;                 // 次のフレームでジャンプする
        public bool IsJumping { private set; get; } = false;                // 今,宙にいるかどうか
        private float yVelocity = 0.0f;                                     // プレイヤーのy軸の速さ
        Quaternion playerDirection = Quaternion.identity;                   // プレイヤーの向き


        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            playerModelLastPositionXZ = new Vector3(playerModel.transform.position.x, 0, playerModel.transform.position.z);
            playerDirection = transform.rotation;
        }

        private void FixedUpdate()
        {
            MoveCharacter();
            RotateCharacter();
        }


        // 進行方向を受け取る
        public void Move(Vector2 direction)
        {
            MoveDirection = direction;
        }

        // 走るかどうかを受け取る
        public void Dash(bool flg)
        {
            IsDash = flg;
        }

        // 呼び出されるとジャンプする
        public void Jump()
        {
            willJump = true;
        }

        // 強制座標移動
        public void ForcedMove(Vector3 destination)
        {
            transform.position = destination;
        }


        // プレイヤーを動かす
        private void MoveCharacter()
        {
            Vector3 moveVelocity;

            Vector3 cameraForward = Vector3.Scale(UnityEngine.Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;  // カメラの向き取得
            Vector3 moveForward = cameraForward * MoveDirection.y + UnityEngine.Camera.main.transform.right * MoveDirection.x;  // 動きたい方向を取得
            moveVelocity = IsDash ? moveForward * dashSpeed : moveForward * walkSpeed;                                          // 移動スピードをかける


            if (!groundChecker.IsGround)    // 接地していないとき
            {
                yVelocity -= gravityPower * Time.deltaTime;
                IsJumping = true;
            }
            else
            {
                if (willJump)
                {
                    yVelocity += jumpPower;
                    willJump = false;
                }
                else if (IsJumping)
                {
                    yVelocity = 0.0f;
                    IsJumping = false;
                }
            }

            rigidbody.velocity = moveVelocity + new Vector3(0, yVelocity, 0);
        }

        // 移動方向にキャラモデルを向かせる
        private void RotateCharacter()
        {
            // 前フレームから一定以上動きがあったらプレイヤーの向きを変更
            if ((new Vector3(playerModel.transform.position.x, 0, playerModel.transform.position.z) - playerModelLastPositionXZ).magnitude >= 0.02f)
            {
                playerDirection = Quaternion.LookRotation(new Vector3(playerModel.transform.position.x, 0, playerModel.transform.position.z) - playerModelLastPositionXZ);
            }

            playerModel.transform.rotation = Quaternion.Lerp(playerModel.transform.rotation, playerDirection, playerRotationSpeed * Time.deltaTime);    // 滑らかに補間
            playerModelLastPositionXZ = new Vector3(playerModel.transform.position.x, 0, playerModel.transform.position.z);
        }
    }
}