using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipLevelLogic : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer skipLevelSR;

    bool pressedDown;

    // Start is called before the first frame update
    void Start()
    {
        pressedDown = false;
    }

    void OnTouchDown()
    {        
        pressedDown = true;
        skipLevelSR.color = new Color(1, 1, 1, 0.65f);
    }

    void OnTouchUp()
    {
        if (pressedDown)
            ShowAds.shouldShowRewardedAd = true;
        pressedDown = false;
    }

    void OnTouchExit()
    {
        skipLevelSR.color = new Color(1, 1, 1, 1);
        pressedDown = false;
    }
}
