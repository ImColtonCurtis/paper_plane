using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParentLogic : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plane" && !GameManager.levelPassed && !GameManager.levelFailed)
        {
            if (transform.parent != null)
                Destroy(transform.parent.gameObject);
        }
    }
}
