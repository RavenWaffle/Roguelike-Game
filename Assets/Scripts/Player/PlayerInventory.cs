using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private Transform m_playerRoot;
    [SerializeField]
    private Transform m_playerHandRight;
    [SerializeField]
    private Transform m_playerHandLeft;

    private IWeapon m_weapon1;
    private IWeapon m_weapon2;

    public IWeapon Weapon1 => m_weapon1;
    public IWeapon Weapon2 => m_weapon2;

    [SerializeField]
    private Armour m_headGear;
    [SerializeField] 
    private Armour m_chestGear;
    [SerializeField]
    private Armour m_legGear;
    [SerializeField]
    private List<Item> m_items;

    public void AddItem(Item item)
    {
        Debug.Log(item.name);
        if(item.GetItemType() == ItemType.Weapon)
        {
            m_weapon1 = Instantiate(item, m_playerHandRight).GetComponent<IWeapon>();
            m_weapon1.SetOwner(this.gameObject);
        }
    }

    public void RemoveItem(GameObject item)
    {

    }
}
