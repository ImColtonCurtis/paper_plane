using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public Vector2 offset;    

    private void Awake()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {        
        if (!GameManager.levelFailed && !GameManager.levelPassed)
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x + offset.x, target.position.y + offset.y, -10), 5.2f * Time.fixedDeltaTime);
    }
}