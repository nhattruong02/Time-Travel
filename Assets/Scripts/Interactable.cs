using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable
{
    public IEnumerator Interact(Transform initiator);
}
