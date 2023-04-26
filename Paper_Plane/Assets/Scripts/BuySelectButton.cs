using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuySelectButton : MonoBehaviour
{
    [SerializeField]
    int buttonNumber;

    bool pressedDown;

    // Start is called before the first frame update
    void Start()
    {
        pressedDown = false;
    }

    void OnTouchDown()
    {
        pressedDown = true;
        ShopButtonLogic.shopButtonDown[buttonNumber] = true;
    }

    void OnTouchUp()
    {
        if (pressedDown)
        {
            ShopButtonLogic.shopButtonUp[buttonNumber] = true;
        }
        pressedDown = false;
    }

    void OnTouchExit()
    {
        if (pressedDown)
        {
            ShopButtonLogic.shopButtonExited[buttonNumber] = true;
            pressedDown = false;
        }
    }
}