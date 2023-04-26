using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearMissLogic : MonoBehaviour
{
    [SerializeField]
    GameObject nearMissObj;

    [SerializeField]
    Transform triggerTransform;

    bool readyToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        readyToSpawn = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pipe")
        {
            if (readyToSpawn)
            {                
                StartCoroutine(WaitForNewSpawn());
            }
        }
    }

    IEnumerator WaitForNewSpawn()
    {
        readyToSpawn = false;
        yield return new WaitForSecondsRealtime(0.2f);
        if (!GameManager.levelFailed)
        {
            Instantiate(nearMissObj, new Vector3(transform.position.x, transform.position.y+0.1675f, transform.position.z-2.1f), Quaternion.identity, triggerTransform);
            yield return new WaitForSecondsRealtime(1.2f);
        }
        readyToSpawn = true;
    }

}
