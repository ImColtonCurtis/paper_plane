using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimation : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] bool isPlane;
    // Start is called before the first frame update
    void Awake()
    {
        // start idle anim from random spot
        if (!isPlane)
            anim.Play("CoinAnim", 0, Random.Range(0f, 1f));
        else
            anim.Play("IdleStorePlane", 0, Random.Range(0f, 1f));
    }
}
