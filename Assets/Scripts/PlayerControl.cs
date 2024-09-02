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
        Vector2 mouseDirectionV2;
        Vector3 mouseDirectionV3;
        Vector2 localMovement;
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
            //will need to revise, currently expensive.
            /*
            Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycasthit))
            {
                hit_pos = raycasthit.point;
                hit_pos = new Vector3(hit_pos.x, transform.position.y, hit_pos.z);
                transform.LookAt(hit_pos);
            }
            */

            mouseDirectionV2 = (new Vector2(Input.mousePosition.x, Input.mousePosition.y) - new Vector2(Screen.width / 2f, Screen.height / 2f)).normalized;
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
            Vector2 moveVector2D = new Vector2(InputX, InputY).normalized;
            Vector2 forwardVector2D = new Vector2(transform.forward.x, transform.forward.z).normalized;
            Debug.Log("movement = " + moveVector2D + ", forward =" + forwardVector2D);
            localMovement = new Vector2(moveVector2D.x * forwardVector2D.x, forwardVector2D.x * moveVector2D.x + forwardVector2D.y * moveVector2D.y);
            m_Animator.SetFloat("xDir", localMovement.x);
            m_Animator.SetFloat("yDir", localMovement.y);
        }
        private void PlayerMove()
        {
            Vector3 movementVector = (InputY * Vector3.forward + InputX * m_Camera.transform.right).normalized * _speed;
            m_RigidBody.velocity = new Vector3(movementVector.x, m_RigidBody.velocity.y, movementVector.z);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + mouseDirectionV3 * 3);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 3);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + new Vector3(localMovement.x * 3, 0, localMovement.y * 3));
        }
    }
}

