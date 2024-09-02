using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controls
{
    public class CameraControl : MonoBehaviour
    {
        [SerializeField] Transform m_playerPos;
        void Update()
        {
            FollowPlayer();
        }

        void FollowPlayer()
        {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, m_playerPos.position + m_playerPos.forward * 5, Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}

