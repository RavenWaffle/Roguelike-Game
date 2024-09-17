using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class InputManager : MonoBehaviour
{
    public static InputManager instance{ get; private set; }

    //optimization
    Camera m_Camera;
    Vector3 onScreenPos;
    Vector2 mouseDirectionV2;
    Vector3 p_mouseDirectionV3;


    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            m_Camera = FindObjectOfType<Camera>();
        }
    }

    public Vector3 mouseDirection(Vector3 pos)
    {
        onScreenPos = getScreenPos(pos);
        mouseDirectionV2 = (new Vector2(Input.mousePosition.x, Input.mousePosition.y) - new Vector2(onScreenPos.x, onScreenPos.y)).normalized;
        p_mouseDirectionV3 = new Vector3(mouseDirectionV2.x, 0, mouseDirectionV2.y);
        return p_mouseDirectionV3;
    }

    public Vector3 getScreenPos(Vector3 pos)
    {
        return m_Camera.WorldToScreenPoint(pos);
    }

    public GameObject RaycastMouseObject()
    {
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycasthit))
        {
            return raycasthit.collider.gameObject;
        }
        else
        {
            Debug.Log("failed RaycastObject");
        }
        return null;
    }

    public Vector3 RaycastMousePoint()
    {
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycasthit))
        {
            return raycasthit.point;
        }
        else
        {
            Debug.Log("failed RaycastMousePoint");
        }
        return Vector3.zero;
    }
}
