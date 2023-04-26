using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchModeLogic : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer skipLevelSR;

    bool pressedDown;

    [SerializeField]
    Transform switchModeBlackSquare;

    // Start is called before the first frame update
    void Start()
    {
        pressedDown = false;
        switchModeBlackSquare.localPosition = new Vector3(0, -1.5f, 0);

        if (PlayerPrefs.GetInt("Mode", 0) == 0)
        {
            switchModeBlackSquare.localPosition = new Vector3(0, -1.5f, 0);
        }
        else
        {
            switchModeBlackSquare.localPosition = new Vector3(0, 1.5f, 0);
        }
    }

    void OnTouchDown()
    {
        pressedDown = true;
        skipLevelSR.color = new Color(1, 1, 1, 0.65f);
    }

    void OnTouchUp()
    {
        if (pressedDown)
        {            
            if (PlayerPrefs.GetInt("Mode", 0) == 0)
            {
                PlayerPrefs.SetInt("Mode", 1); // Infinite Mode
                StartCoroutine(ModeBlackSquareUp());
            }
            else
            {
                PlayerPrefs.SetInt("Mode", 0); // Story Mode
                StartCoroutine(ModeBlackSquareDown());
            }
            ControlsLogic.myModeSwitched = true;
            PlayerController.modeSwitched = true;
        }        
        pressedDown = false;
    }

    void OnTouchExit()
    {
        skipLevelSR.color = new Color(1, 1, 1, 1);
        pressedDown = false;
    }

    IEnumerator ModeBlackSquareDown()
    {
        float timer = 0, totalTime = 9;

        while (timer <= totalTime)
        {
            yield return new WaitForFixedUpdate();
            switchModeBlackSquare.localPosition = Vector3.Lerp(new Vector3(0, 1.5f, 0), new Vector3(0, -1.5f, 0), timer / totalTime);
            skipLevelSR.color =Color.Lerp(new Color(1, 1, 1, 0.65f), new Color(1, 1, 1, 1), timer / totalTime);
            timer++;
        }
        switchModeBlackSquare.localPosition = new Vector3(0, -1.5f, 0);
    }

    IEnumerator ModeBlackSquareUp()
    {
        float timer = 0, totalTime = 9;

        while (timer <= totalTime)
        {
            yield return new WaitForFixedUpdate();
            switchModeBlackSquare.localPosition = Vector3.Lerp(new Vector3(0, -1.5f, 0), new Vector3(0, 1.5f, 0), timer / totalTime);
            skipLevelSR.color = Color.Lerp(new Color(1, 1, 1, 0.65f), new Color(1, 1, 1, 1), timer / totalTime);
            timer++;
        }
        switchModeBlackSquare.localPosition = new Vector3(0, 1.5f, 0);
    }
}
