using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] PlayerInventory inventory;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && Input.GetKey(KeyCode.Mouse1))
        {
            GameObject target = InputManager.instance.RaycastMouseObject();
            if(target != null && inventory.Weapon1 != null)
            {
                target.TryGetComponent<IDamagable>(out IDamagable damagable);
                inventory.Weapon1.ActionAttack(damagable);
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1))
        {
            GameObject target = InputManager.instance.RaycastMouseObject();
            if (target != null && target.TryGetComponent<IInteractible>(out IInteractible Interactible))
            {
                Interactible.OnInteract(this.gameObject);
            }
            return;
        }
    }
}
