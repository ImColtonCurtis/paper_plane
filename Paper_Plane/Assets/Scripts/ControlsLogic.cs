using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsLogic : MonoBehaviour
{
    [SerializeField]
    GameObject anchorObj, targetObj;
    public GameObject paperPlaneObj;
    public static bool touchedDown;
    Vector3 anchorPos, targetPos, moveVelocity;
    public static Vector2 moveInput;

    bool goingLeft, preformedFollowThrough, comingHome, moveOccuring;

    float anchorRadius, maxFallSpeed;
    float moveSpeed = 3.7f, slowFloat = 0.168f;

    public PlayerController controller;
    Rigidbody rb;

    public BoxCollider controlsCol;

    [SerializeField]
    SpriteRenderer tapToStartSR, tapToStartBGSR, retrySR, tapToContinueSR, modeSwitchText, roundedWhiteSquareText, blackSquareRounded;

    [SerializeField]
    SpriteRenderer squareScreen;

    [SerializeField]
    Animator myAnim;

    bool failedLevel, reloading, cancelCheckSent;

    [SerializeField]
    SpriteRenderer skipLevelSR;

    [SerializeField]
    BoxCollider skipLevelCol;

    [SerializeField]
    BoxCollider touchInputCol;

    [SerializeField]
    GameObject startupInput;

    public static bool startFromProxy, myModeSwitched;

    void Awake()
    {
        touchedDown = false;
        anchorObj.SetActive(false);
        targetObj.SetActive(false);
        anchorRadius = 0.142f; // 0.142, 0.041f

        moveOccuring = false; // so preformedFollowThrough can be set correctly
        goingLeft = false;
        preformedFollowThrough = false;
        comingHome = false;
        rb = controller.GetComponent<Rigidbody>();
        controlsCol.enabled = true;

        if (PlayerPrefs.GetInt("LostInARow", 0) >= 5 && PlayerPrefs.GetInt("Mode", 0) == 0) // show skip level button
        {
            skipLevelSR.enabled = true;
            skipLevelCol.enabled = true;
        }
        else
        {
            skipLevelSR.enabled = false;
            skipLevelCol.enabled = false;
        }

        startFromProxy = false;

        touchInputCol.enabled = false;
        startupInput.SetActive(true);
    }

    private void Start()
    {
        reloading = false;
        failedLevel = false;
        cancelCheckSent = false;
    }

    IEnumerator FadeSkipLevelIn()
    {
        float timer = 0, totalTime = 20;

        while (timer <= totalTime)
        {
            skipLevelSR.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, timer / totalTime);
            yield return new WaitForFixedUpdate();
            timer++;
        }
    }

    IEnumerator FadeSkipLevelOut()
    {
        float timer = 0, totalTime = 20;

        while (timer <= totalTime)
        {
            skipLevelSR.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), timer / totalTime);
            yield return new WaitForFixedUpdate();
            timer++;
        }
        skipLevelSR.enabled = false;
        skipLevelCol.enabled = false;
    }

    private void FixedUpdate()
    {
        if (!GameManager.levelFailed && !GameManager.levelPassed && GameManager.levelStarted)
        {
            if (!touchedDown)
            {                
                if (!preformedFollowThrough)
                {
                    if (moveInput.x > slowFloat)
                    {
                        moveInput.x -= slowFloat;
                        goingLeft = true;
                        moveOccuring = true;
                    }
                    else if (moveInput.x < -slowFloat)
                    {
                        moveInput.x += slowFloat;
                        goingLeft = false;
                        moveOccuring = true;
                    }
                    else if (moveOccuring)
                    {
                        moveInput.x = 0;
                        moveOccuring = false;
                        preformedFollowThrough = true;
                    }
                }
                if (preformedFollowThrough)
                {
                    if (goingLeft)
                    {
                        if (moveInput.x > -(slowFloat * 2.4f) && !comingHome)
                            moveInput.x -= slowFloat / 2.7f;
                        else
                            comingHome = true;
                        if (comingHome)
                        {
                            if (moveInput.x < -slowFloat)
                                moveInput.x += slowFloat / 4f;
                            else
                            {
                                moveInput.x = 0;
                                comingHome = false;
                                goingLeft = false;
                                preformedFollowThrough = false;
                            }
                        }
                    }
                    else
                    {
                        if (moveInput.x < (slowFloat * 2.4f) && !comingHome)
                            moveInput.x += slowFloat / 2.7f;
                        else
                            comingHome = true;
                        if (comingHome)
                        {
                            if (moveInput.x > slowFloat)
                                moveInput.x -= slowFloat / 4f;
                            else
                            {
                                moveInput.x = 0;
                                comingHome = false;
                                goingLeft = false;
                                preformedFollowThrough = false;
                            }
                        }
                    }
                }
                // -65.815f, , 1.104f
                paperPlaneObj.transform.eulerAngles = new Vector3(-47 - (Mathf.Abs(moveInput.x) * 18.815f), 180 - (53.65f * moveInput.x), Mathf.Abs(moveInput.x) * 1.104f);
                if (!preformedFollowThrough || moveInput.x == 0)
                {
                    moveVelocity = moveInput.normalized * moveSpeed;
                    controller.Move(moveVelocity);
                }
            }
        }
        else if (GameManager.levelFailed)
        {
            moveOccuring = false;
            comingHome = false;
            goingLeft = false;
            preformedFollowThrough = false;
        }
        else if (GameManager.levelPassed)
        {
            moveOccuring = false;
            comingHome = false;
            goingLeft = false;
            preformedFollowThrough = false;
            controlsCol.enabled = false;
        }
    }

    IEnumerator FadeOutTTS()
    {
        float timer = 0, totalTime = 20;

        while (timer <= totalTime)
        {
            modeSwitchText.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), (timer+2) / totalTime);
            roundedWhiteSquareText.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), (timer+2) / totalTime);
            blackSquareRounded.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), timer / totalTime);

            tapToStartSR.color = Color.Lerp(new Color(1, 1, 1, 0.75f), new Color(1, 1, 1, 0), timer / totalTime);
            tapToStartBGSR.color = Color.Lerp(new Color(0, 0, 0, 0.55f), new Color(0, 0, 0, 0), timer / totalTime);
            timer++;
            yield return new WaitForFixedUpdate();
        }
    }

    private void Update()
    {
        if (myModeSwitched)
        {
            if (PlayerPrefs.GetInt("LostInARow", 0) >= 5) // show skip level button
            {
                if (PlayerPrefs.GetInt("Mode", 0) == 0) // story mode
                {
                    skipLevelSR.enabled = true;
                    skipLevelCol.enabled = true;

                    StartCoroutine(FadeSkipLevelIn());
                }
                else
                    StartCoroutine(FadeSkipLevelOut());
            }
            myModeSwitched = false;
        }

        // preform gravity
        if (!GameManager.levelFailed && GameManager.levelStarted)
        {
            maxFallSpeed = -(1.8f - (Mathf.Abs(moveInput.x) * 1.39285714286f)); // (0.5,2.7)
            if (rb.velocity.y > maxFallSpeed)
            {
                rb.velocity -= new Vector3(0, 0.05f, 0);  // acceleration speed
            }
            else if (Mathf.Abs(rb.velocity.y - maxFallSpeed) < 0.02f)
            {
                rb.velocity = new Vector3(rb.velocity.x, maxFallSpeed, rb.velocity.z);
            }
            else if (rb.velocity.y < maxFallSpeed)
            {
                rb.velocity += new Vector3(0, 0.26f, 0);  // decelleration speed
            }
            else
                rb.velocity = new Vector3(rb.velocity.x, maxFallSpeed, rb.velocity.z);
        }
        if (!failedLevel && GameManager.levelFailed)
        {
            StartCoroutine(FadeInLevelLost());
            failedLevel = true;
        }
        if (!touchedDown && !cancelCheckSent && GameManager.levelStarted)
        {
            StartCoroutine(CancelCheck());
            cancelCheckSent = true;
        }

        if (startFromProxy)
        {
            if (!GameManager.levelStarted)
            {
                touchInputCol.enabled = true;
                startupInput.SetActive(false);

                myAnim.SetBool("GameStarted", true);
                StartCoroutine(FadeOutTTS());
                GameManager.levelStarted = true;

                if (PlayerPrefs.GetInt("LostInARow", 0) >= 5) // show skip level button
                    StartCoroutine(FadeSkipLevelButtonOut());
            }
            startFromProxy = false;
        }
    }

    void OnTouchDown(Vector3 point)
    {
        if (!touchedDown && !GameManager.levelFailed && !GameManager.levelPassed)
        {
            touchedDown = true;
            anchorObj.SetActive(true);
            targetObj.SetActive(true);

            anchorPos = new Vector3(point.x, point.y, anchorPos.z);
            anchorObj.transform.position = anchorPos;
            targetPos = new Vector3(point.x, point.y, targetPos.z);
            targetObj.transform.position = targetPos;
        }
        if (GameManager.levelFailed && !reloading)
        {
            reloading = true;
            StartCoroutine(ReloadLevel());
        }
        cancelCheckSent = false;
    }

    void OnTouchStay(Vector3 point)
    {
        if (GameManager.levelStarted)
        {
            if (!touchedDown && !GameManager.levelFailed && !GameManager.levelPassed)
            {
                touchedDown = true;
                anchorObj.SetActive(true);
                targetObj.SetActive(true);

                anchorPos = new Vector3(point.x, point.y, anchorPos.z);
                anchorObj.transform.position = anchorPos;
                targetPos = new Vector3(point.x, point.y, targetPos.z);
                targetObj.transform.position = targetPos;
            }
            if (touchedDown && !GameManager.levelFailed && !GameManager.levelPassed)
            {
                targetPos = new Vector3(point.x, point.y, targetPos.z);
                targetObj.transform.position = targetPos;
                moveInput = new Vector2(targetObj.transform.localPosition.x * 2.7f, targetObj.transform.localPosition.y * 2);
                if (moveInput.x >= 0.99f)
                    moveInput.x = 1;
                else if (moveInput.x <= -0.99f)
                    moveInput.x = -1;
                if (moveInput.y >= 0.99f)
                    moveInput.y = 1;
                else if (moveInput.y <= -0.99f)
                    moveInput.y = -1;

                moveVelocity = moveInput * moveSpeed;
                controller.Move(moveVelocity);

                // if finger position is greater than (anchorRadius * 2), then reset anchor to be (anchorRadius)
                if (Vector3.Distance(anchorPos, targetPos) >= (anchorRadius * 1.2f))
                {
                    anchorPos = Vector3.Lerp(anchorPos, targetPos, 0.1667f);
                    anchorObj.transform.position = anchorPos;
                }

                // Control paper plane rotation based on moveinput.x
                paperPlaneObj.transform.eulerAngles = new Vector3(-47 - (Mathf.Abs(moveInput.x) * 18.815f), 180 - (53.65f * moveInput.x), Mathf.Abs(moveInput.x) * 1.104f);
            }
        }
    }

    void OnTouchUp()
    {
        if (touchedDown)
        {
            touchedDown = false;
            anchorObj.SetActive(false);
            targetObj.SetActive(false);

            moveInput = new Vector2(moveInput.x, 0);            
        }
    }

    void OnTouchExit()
    {
        if (touchedDown)
        {
            touchedDown = false;
            anchorObj.SetActive(false);
            targetObj.SetActive(false);

            moveInput = new Vector2(moveInput.x, 0);            
        }
    }

    IEnumerator FadeInLevelLost()
    {
        float timer = 0, totalTime = 35;

        StartCoroutine(GrowRetryText());
        while (timer <= totalTime)
        {
            if (timer <= 20)
                retrySR.color = Color.Lerp(new Color(1,1,1,0), Color.white, timer/totalTime*2);
            tapToContinueSR.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, timer / totalTime);
            yield return new WaitForFixedUpdate();
            timer++;
        }
    }

    IEnumerator FadeSkipLevelButtonOut()
    {
        float timer = 0, totalTime = 15;

        skipLevelCol.enabled = false;
        while (timer <= totalTime)
        {
            skipLevelSR.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), timer / totalTime);
            yield return new WaitForFixedUpdate();
            timer++;
        }
        skipLevelSR.enabled = false;
    }

    IEnumerator GrowRetryText()
    {
        float timer = 0, totalTime = 180;

        while (timer <= totalTime)
        {
            if (timer == 130)
                StartCoroutine(FadeOutRetry());
            retrySR.transform.localScale = Vector3.Lerp(new Vector3(0.2692761f, 0.2692761f, 0.2692761f), new Vector3(0.4898203f, 0.4898203f, 0.4898203f), timer/totalTime);
            yield return new WaitForFixedUpdate();
            timer++;
        }
    }

    IEnumerator FadeOutRetry()
    {
        float timer = 0, totalTime = 50;

        while (timer <= totalTime)
        {
            retrySR.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), timer / totalTime);
            yield return new WaitForFixedUpdate();
            timer++;
        }
    }

    IEnumerator ReloadLevel()
    {
        float timer = 0, totalTime = 30;

        squareScreen.color = new Color(1, 1, 1, 0);
        tapToContinueSR.color = new Color(1, 1, 1, 0.65f);
        while (timer <= totalTime)
        {
            squareScreen.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, timer / totalTime);
            yield return new WaitForFixedUpdate();
            timer++;
        }
        yield return new WaitForSecondsRealtime(0.15f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    IEnumerator CancelCheck()
    {
        int i = 0;

        while (!touchedDown && i < 6) {
            yield return new WaitForSecondsRealtime(0.1f);
            i++;
        }
        if (!touchedDown && i >= 6)
        {
            moveInput.x = 0;
            comingHome = false;
            goingLeft = false;
            preformedFollowThrough = false;
        }
    }
}
