using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    Vector3 velocity;
    public Rigidbody myRigidBody;
    public ParticleSystem explosionSystem;
    public GameObject explosion;

    [SerializeField]
    MeshCollider planeMesh;

    [SerializeField]
    GameObject planesObject;

    [SerializeField]
    SpriteRenderer squareScreen;

    [SerializeField]
    TextMeshPro levelPassedText;

    [SerializeField]
    GameObject confettiObj;

    [SerializeField]
    SpriteRenderer progressSR, progressLineSR, progressStartSR, progressEndSR;

    [SerializeField]
    SpriteRenderer progressBGSR, progressLineBGSR, progressStartBGSR, progressEndBGSR, shopButtonSR;

    [SerializeField]
    TextMeshPro currentLevelInt, nextLevelInt, highScoreInt;

    bool fadeProgressIn, fadeProgressOut, leveledOut;
    float pullBack;

    [SerializeField]
    GameObject coinParticles;

    [SerializeField]
    Transform coinFolder;

    bool levelingOut, setScoreToZero;

    public static bool fakeRestart, modeSwitched;

    public static int score;
    float incrementHeight;

    private void Awake()
    {
        myRigidBody.constraints = RigidbodyConstraints.FreezePositionZ;
        explosion.SetActive(false);
        confettiObj.SetActive(false);

        modeSwitched = false;
    }

    private void Start()
    {
        planeMesh.enabled = true;

        fakeRestart = false;

        score = 0;

        StartCoroutine(FadeInScreen());
        int currentLevelNum = PlayerPrefs.GetInt("CurrentLevel", 1);

        if (currentLevelNum >= 10000)
        {
            currentLevelInt.fontSize = 4.41f;
            nextLevelInt.fontSize = 4.41f;
        }
        else if (currentLevelNum >= 1000)
        {
            currentLevelInt.fontSize = 6;
            nextLevelInt.fontSize = 6;
        }
        else if (currentLevelNum >= 100)
        {
            currentLevelInt.fontSize = 6.65f;
            nextLevelInt.fontSize = 6.65f;
        }
        else if (currentLevelNum >= 10)
        {
            currentLevelInt.fontSize = 8.1f;
            nextLevelInt.fontSize = 8.1f;
        }
        else
        {
            currentLevelInt.fontSize = 9.12f;
            nextLevelInt.fontSize = 9.12f;
        }

        highScoreInt.text = PlayerPrefs.GetInt("HighScore", 0).ToString();

        currentLevelInt.text = currentLevelNum.ToString();
        currentLevelNum += 1;
        nextLevelInt.text = currentLevelNum.ToString();

        fadeProgressOut = false;
        fadeProgressIn = false;

        leveledOut = false;
        pullBack = 0;
        incrementHeight = 0;
        levelingOut = false;

        setScoreToZero = false;

        StartCoroutine(LevelOut(Vector3.zero));

        if (PlayerPrefs.GetInt("Mode", 0) == 1) // infinite mode
            highScoreInt.color = Color.white;
        else
            highScoreInt.color = new Color(1, 1, 1, 0);
        }

    // Update is called once per frame
    public void Move(Vector3 _velocity)
    {
        if (GameManager.levelStarted && !GameManager.levelFailed)
        {
            if (ControlsLogic.touchedDown)
                velocity = new Vector3(_velocity.x, myRigidBody.velocity.y, velocity.z);
            else
            {
                if (!leveledOut)
                {
                    if (!levelingOut)
                    {
                        StartCoroutine(LevelOut(_velocity));
                        leveledOut = true;
                    }
                }
            }
        }
    }

    IEnumerator LevelOut(Vector3 _velocity)
    {
        pullBack = Mathf.Abs(((transform.localEulerAngles.x - 296.3675f) / 16.6325f)-1) * 3.7f; // 296.3675 -> 313 <- 296.3675 | -3.7 -> 0 <- 3.7
        float endCount = (pullBack/3.7f*7)+6;
        if (_velocity.x < 0)
            pullBack *= -1;
        float startingSpeed = pullBack;        
        levelingOut = true;
        for (float i = 0; i <= endCount; i++)
        {
            if (ControlsLogic.touchedDown)
                break;

            pullBack = Mathf.Lerp(startingSpeed, 0, i / endCount);

            velocity = new Vector3(pullBack, myRigidBody.velocity.y, velocity.z);
            yield return new WaitForFixedUpdate();
        }
        levelingOut = false;
        leveledOut = false;
    }

    private void Update()
    {
        if (!GameManager.levelPassed && !GameManager.levelFailed)
            progressSR.transform.localPosition = new Vector3((transform.localPosition.y/38*15.4f)+7.7f , 0.27f, 0);

        if (!fadeProgressIn && GameManager.levelStarted)
        {
            StartCoroutine(FadeProgressIn());
            fadeProgressIn = true;
        }

        if (!fadeProgressOut && GameManager.levelFailed)
        {
            StartCoroutine(FadeProgressOut());
            fadeProgressOut = true;
        }

        if (fakeRestart)
        {
            StartCoroutine(FadeInLevelPassed());
            StartCoroutine(LevelFinished(true));
            StartCoroutine(FadeProgressOut());
            fakeRestart = false;
        }

        if (modeSwitched)
        {
            if (PlayerPrefs.GetInt("Mode", 0) == 0) // story mode
                StartCoroutine(FadeHighScoreOut());
            else
                StartCoroutine(FadeHighScoreIn());
            modeSwitched = false;
        }

        if (!setScoreToZero && GameManager.levelStarted)
        {
            highScoreInt.text = score.ToString();
            setScoreToZero = true;
        }

        // Increment score
        if (PlayerPrefs.GetInt("Mode", 0) == 1 && transform.position.y < incrementHeight-5) // infinite mode
        {
            incrementHeight = transform.position.y;
            score++;
            highScoreInt.text = score.ToString();
        }
    }

    public void FixedUpdate()
    {
        if (GameManager.levelStarted && !GameManager.levelFailed)
            myRigidBody.MovePosition(myRigidBody.position + velocity * Time.fixedDeltaTime);        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!GameManager.levelPassed)
        {
            transform.GetComponent<MeshRenderer>().enabled = false;
            if (myRigidBody.velocity.y > 0.475f)
                explosionSystem.startSpeed = 7 + ((Mathf.Abs(myRigidBody.velocity.y) - 0.475f) / 2.425f * 7); // 7 to 13
            else
                explosionSystem.startSpeed = 7;
            explosion.SetActive(true);
            myRigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX
                | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            myRigidBody.velocity = Vector3.zero;
            GameManager.levelFailed = true;

            if (PlayerPrefs.GetInt("Mode", 0) == 0)
                PlayerPrefs.SetInt("LostInARow", PlayerPrefs.GetInt("LostInARow", 0) + 1);

            if (PlayerPrefs.GetInt("Mode", 0) == 1 && score > PlayerPrefs.GetInt("HighScore", 0))
                PlayerPrefs.SetInt("HighScore", score);

            if (PlayerPrefs.GetInt("Mode", 0) == 1)
                PlayerPrefs.SetInt("ChangeColor", 1);

            planesObject.SetActive(false);

            PlayerPrefs.SetInt("GamesLost", PlayerPrefs.GetInt("GamesLost", 0) + 1);

            if (PlayerPrefs.GetInt("Mode", 0) == 1)
                PlayerPrefs.SetInt("RecentScore", score);
            else
                PlayerPrefs.SetInt("RecentScore", 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinishLine")
        {
            planeMesh.enabled = false;

            PlayerPrefs.SetInt("LostInARow", 0);            

            PlayerPrefs.SetInt("ChangeColor", 1);

            PlayerPrefs.SetInt("LevelJustPassed", 1);
            confettiObj.SetActive(true);

            PlayerPrefs.SetInt("CreateNewLevel", 1);

            levelPassedText.text = "Level " + PlayerPrefs.GetInt("CurrentLevel", 1) + "\r\nPassed!";
            PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel", 1) + 1);
            StartCoroutine(FadeInLevelPassed());

            GameManager.levelPassed = true;
            StartCoroutine(LevelFinished(true));
            StartCoroutine(FadeProgressOut());
        }
        if (other.tag == "Coin")
        {
            // increment currentCoinCount
            int currentCoinCount = PlayerPrefs.GetInt("CoinCount", 0);
            currentCoinCount++;
            PlayerPrefs.SetInt("CoinCount", currentCoinCount);
            // instantiate coin particles
            GameObject tempPart = Instantiate(coinParticles, other.transform.position, Quaternion.identity, coinFolder);
            tempPart.transform.eulerAngles = new Vector3(0, 180, 0);
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }

    IEnumerator FadeProgressIn()
    {
        float timer = 0, totalTime = 20;

        Color startingShopColor = shopButtonSR.color;

        while (timer <= totalTime)
        {
            if (PlayerPrefs.GetInt("Mode", 0) == 0) // story mode
            {
                currentLevelInt.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, timer / totalTime);
                nextLevelInt.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, timer / totalTime);

                progressBGSR.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 0.62f), timer / totalTime);
                progressLineBGSR.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 0.62f), timer / totalTime);
                progressStartBGSR.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 0.62f), timer / totalTime);
                progressEndBGSR.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 0.62f), timer / totalTime);

                progressSR.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, timer / totalTime);
                progressLineSR.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, timer / totalTime);
                progressStartSR.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, timer / totalTime);
                progressEndSR.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, timer / totalTime);
            }
            shopButtonSR.color = Color.Lerp(startingShopColor, new Color(1, 1, 1, 0), timer / totalTime);

            yield return new WaitForFixedUpdate();
            timer++;
        }
    }

    IEnumerator FadeHighScoreIn()
    {
        float timer = 0, totalTime = 20;

        while (timer <= totalTime)
        {
            highScoreInt.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, timer / totalTime);
            yield return new WaitForFixedUpdate();
            timer++;
        }
    }

    IEnumerator FadeHighScoreOut()
    {
        float timer = 0, totalTime = 20;

        while (timer <= totalTime)
        {
            highScoreInt.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), timer / totalTime);
            yield return new WaitForFixedUpdate();
            timer++;
        }
    }

    IEnumerator FadeProgressOut()
    {
        float timer = 0, totalTime = 15;

        while (timer <= totalTime)
        {
            if (PlayerPrefs.GetInt("Mode", 0) == 0) // story mode
            {
                currentLevelInt.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), timer / totalTime);
                nextLevelInt.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), timer / totalTime);

                progressBGSR.color = Color.Lerp(new Color(0, 0, 0, 0.62f), new Color(0, 0, 0, 0), timer / totalTime);
                progressLineBGSR.color = Color.Lerp(new Color(0, 0, 0, 0.62f), new Color(0, 0, 0, 0), timer / totalTime);
                progressStartBGSR.color = Color.Lerp(new Color(0, 0, 0, 0.62f), new Color(0, 0, 0, 0), timer / totalTime);
                progressEndBGSR.color = Color.Lerp(new Color(0, 0, 0, 0.62f), new Color(0, 0, 0, 0), timer / totalTime);

                progressSR.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), timer / totalTime);
                progressLineSR.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), timer / totalTime);
                progressStartSR.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), timer / totalTime);
                progressEndSR.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), timer / totalTime);
            }
            yield return new WaitForFixedUpdate();
            timer++;
        }
    }

    IEnumerator FadeInLevelPassed()
    {
        float timer = 0, totalTime = 20;

        StartCoroutine(GrowLevelPassedText());
        while (timer <= totalTime)
        {
            levelPassedText.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, timer / totalTime);
            yield return new WaitForFixedUpdate();
            timer++;
        }
    }

    IEnumerator GrowLevelPassedText()
    {
        float timer = 0, totalTime = 180;

        while (timer <= totalTime)
        {
            if (timer == 130)
                StartCoroutine(FadeOutLevelPassed());
            levelPassedText.transform.localScale = Vector3.Lerp(new Vector3(0.82757f, 0.82757f, 0.82757f), new Vector3(1.449019f, 1.449019f, 1.449019f), timer / totalTime);
            yield return new WaitForFixedUpdate();
            timer++;
        }
    }

    IEnumerator FadeOutLevelPassed()
    {
        float timer = 0, totalTime = 50;

        while (timer <= totalTime)
        {
            levelPassedText.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), timer / totalTime);
            yield return new WaitForFixedUpdate();
            timer++;
        }
    }

    IEnumerator FadeInScreen()
    {
        float timer = 0, totalTime = 22;

        squareScreen.color = new Color(1, 1, 1, 1);
        yield return new WaitForSecondsRealtime(0.2f);
        while (timer <= totalTime)
        {
            squareScreen.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), timer / totalTime);
            yield return new WaitForFixedUpdate();
            timer++;
        }
    }

    IEnumerator LevelFinished(bool shouldWait)
    {
        float timer = 0, totalTime = 50;

        squareScreen.color = new Color(1, 1, 1, 0);
        if (shouldWait)
            yield return new WaitForSecondsRealtime(1.7f);
        while (timer <= totalTime)
        {
            squareScreen.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, timer/totalTime);
            yield return new WaitForFixedUpdate();
            timer++;
        }
        yield return new WaitForSecondsRealtime(0.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
