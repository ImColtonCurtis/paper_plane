using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonLogic : MonoBehaviour
{
    bool pressedDown;

    // Start is called before the first frame update
    void Start()
    {
        pressedDown = false;
    }

    void OnTouchDown()
    {
        pressedDown = true;
    }

    void OnTouchUp()
    {
        if (pressedDown)
        {
            if (PlayerPrefs.GetInt("ShopPostion", 0) == 0)
            {
                if (!ControlsLogic.startFromProxy)
                    ControlsLogic.startFromProxy = true;
            }
            else if (!ShopButtonLogic.closedShop && PlayerPrefs.GetInt("ShopPostion", 0) == 1)
                ShopButtonLogic.closedShop = true;
        }
        pressedDown = false;
    }

    void OnTouchExit()
    {
        pressedDown = false;
    }
}
