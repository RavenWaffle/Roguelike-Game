using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
    public class CameraControl : MonoBehaviour
    {
        [SerializeField] Camera m_Camera;
        [SerializeField] Transform m_playerPos;

        [SerializeField] float _cameraSpeed;
        [SerializeField] float _maxSpeedDistance;
        void Update()
        {
            FollowPlayer();
        }

        void FollowPlayer()
        {
            if(Vector3.Distance(this.transform.position, m_playerPos.transform.position) < 0.025f)
            {
                this.transform.position = m_playerPos.transform.position;
                return;
            }

            float cameraSpeed = Vector3.Distance(this.transform.position, m_playerPos.transform.position) / _maxSpeedDistance;
            cameraSpeed = Mathf.Clamp01(cameraSpeed) * _cameraSpeed;
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(m_playerPos.position.x, 0, m_playerPos.position.z), Time.deltaTime * cameraSpeed);
        }
    }
}

