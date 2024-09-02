using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Controls
{
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField] Camera m_Camera;
        [SerializeField] Rigidbody m_RigidBody;
        [SerializeField] Animator m_Animator;
        [SerializeField] float _speed;
        [SerializeField] float _rotSpeed;

        //save memory
        //Vector3 hit_pos;
        Vector3 globalMovement;
        Vector2 localMovement;

        Vector3 onScreenPos;
        Vector3 mouseDirectionV3;
        Vector2 mouseDirectionV2;

        Vector2 moveVector2D;
        Vector2 forwardVector2D;
        Vector2 rightVector2D;
        float InputX;
        float InputY;

        void Update()
        {
            InputX = Input.GetAxisRaw("Horizontal");
            InputY = Input.GetAxisRaw("Vertical");
            AnimationUpdate();
        }

        void FixedUpdate()
        {
            PlayerLook();
            PlayerMove();
        }

        private void PlayerLook()
        {
            onScreenPos = m_Camera.WorldToScreenPoint(transform.position);
            mouseDirectionV2 = (new Vector2(Input.mousePosition.x, Input.mousePosition.y) - new Vector2(onScreenPos.x, onScreenPos.y)).normalized;
            mouseDirectionV3 = new Vector3(mouseDirectionV2.x, 0, mouseDirectionV2.y);

            // Rotate smoothly towards the target direction
            float step = _rotSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, mouseDirectionV3, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            /*
            float deltaAngle = Vector3.SignedAngle(transform.forward, transform.position + mouseDirectionV3, Vector3.up);
            if(Mathf.Abs(deltaAngle) <= 5)
            {
                //snapping
                transform.LookAt((transform.position + mouseDirectionV3));
            }
            else
            {
                // Rotate smoothly towards the target direction
                float step = _rotSpeed * Time.deltaTime;
                Vector3 newDirection = Vector3.RotateTowards(transform.forward, mouseDirectionV3, step, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
            */
        }
        private void AnimationUpdate()
        {
            moveVector2D = new Vector2(InputX, InputY).normalized;

            forwardVector2D = new Vector2(transform.forward.x, transform.forward.z).normalized;
            rightVector2D = new Vector2(forwardVector2D.y, -forwardVector2D.x);

            localMovement = new Vector2(Vector2.Dot(moveVector2D, rightVector2D), Vector2.Dot(moveVector2D, forwardVector2D));

            m_Animator.SetFloat("xDir", localMovement.x);
            m_Animator.SetFloat("yDir", localMovement.y);
        }

        private void PlayerMove()
        {
            globalMovement = (InputY * Vector3.forward + InputX * m_Camera.transform.right).normalized * _speed;
            m_RigidBody.velocity = new Vector3(globalMovement.x, m_RigidBody.velocity.y, globalMovement.z);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + mouseDirectionV3 * 3);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 3);
        }
    }
}

