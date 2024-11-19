using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] 
        private Rigidbody m_RigidBody;
        [SerializeField]
        private float m_speed;
        [SerializeField]
        private float m_rotSpeed;

        //optimization
        Vector3 globalMovement;
        float inputX;
        float inputY;

        void Update()
        {
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");
        }

        void FixedUpdate()
        {
            PlayerMove();
            PlayerLook();
        }

        private void PlayerLook()
        {
            float step = m_rotSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, InputManager.instance.mouseDirection(transform.position), step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }
        private void PlayerMove()
        {
            globalMovement = (inputY * Vector3.forward + inputX * Vector3.right).normalized * m_speed;
            m_RigidBody.velocity = new Vector3(globalMovement.x, m_RigidBody.velocity.y, globalMovement.z);
        }

        private void OnDrawGizmos()
        {
            if(InputManager.instance == null)
            {
                return;
            }
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + InputManager.instance.mouseDirection(transform.position) * 3);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * 3);
        }
    }
}

