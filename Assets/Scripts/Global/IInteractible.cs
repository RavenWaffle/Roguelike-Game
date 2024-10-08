using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractible
{
    void OnInteract(GameObject initiator);
    Transform ObjectTransform();
}
