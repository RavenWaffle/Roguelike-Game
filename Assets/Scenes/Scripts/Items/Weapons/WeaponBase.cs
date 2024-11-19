using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponBase : Item, IWeapon
{
    [SerializeField]
    protected float WeaponDamage;
    [SerializeField]
    protected float WeaponRange;
    [SerializeField]
    protected float WeaponCooldown;
    [SerializeField]
    protected bool IsReady = true;
    [SerializeField]
    protected GameObject Owner;

    protected virtual IEnumerator Attack(IDamagable target)
    {
        if(!IsReady)
        {
            yield break;
        }
        IsReady = false;
        if(target != null && (target.ObjectTransform().position - Owner.transform.position).magnitude <= WeaponRange)
        {
            target.DamageAction(WeaponDamage);
        }
        
        yield return new WaitForSeconds(WeaponCooldown);
        IsReady = true;
    }

    public void ActionAttack(IDamagable target)
    {
        StartCoroutine(Attack(target));
    }

    public void SetOwner(GameObject owner)
    {
        Owner = owner;
        Debug.Log(Owner);
    }
}
