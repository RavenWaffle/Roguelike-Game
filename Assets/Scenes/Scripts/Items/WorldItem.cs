using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour, IInteractible
{
    [SerializeField]
    private Item m_item_reference;

    void Interact(GameObject initiator)
    {
        initiator.GetComponent<PlayerInventory>().AddItem(m_item_reference);
        Destroy(this.gameObject);
    }

    public void OnInteract(GameObject initiator)
    {
         Interact(initiator);
    }

    public Transform ObjectTransform()
    {
        return this.transform;
    }
}
