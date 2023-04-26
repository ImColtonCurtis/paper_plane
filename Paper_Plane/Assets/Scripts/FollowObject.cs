using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform objectToFollow;
    public bool horizonal, both;

    // Update is called once per frame
    void Update()
    {
        if (both)
            transform.localPosition = new Vector3(objectToFollow.position.x, objectToFollow.position.y, transform.localPosition.z);
        else if (horizonal)
            transform.localPosition = new Vector3(objectToFollow.position.x, transform.localPosition.y, transform.localPosition.z);
        else
            transform.localPosition = new Vector3(transform.localPosition.x, objectToFollow.position.y, transform.localPosition.z);
    }
}