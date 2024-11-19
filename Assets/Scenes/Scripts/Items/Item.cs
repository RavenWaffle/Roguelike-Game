using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    protected ItemType ItemType;

    public ItemType GetItemType()
    {
        return ItemType;
    }
}
