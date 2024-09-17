using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void DamageAction(float damage);
    Transform ObjectTransform();
}
