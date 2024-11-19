using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : Entity, IDamagable
{
    [SerializeField]
    protected float Health = 100;

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public void DamageAction(float damage)
    {
        TakeDamage(damage);
    }

    public Transform ObjectTransform()
    {
        return this.transform;
    }
}
