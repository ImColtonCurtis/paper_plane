using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool levelStarted, levelFailed, levelPassed;

    [SerializeField]
    Material brickMat, antiBrickMat;

    [SerializeField]
    Color[] colorArray = new Color[19];
    // 327, 52, 56
    // 57, 77 , 55

    // each object (including each rotation) as seperate object prefabs
    [SerializeField]
    GameObject[] obstacleSlates = new GameObject[2];

    [SerializeField]
    float[] obstacleDistances = new float[2];

    [SerializeField]
    Transform levelObjectsFolder, planeTransform;

    bool moveRight = false;

    bool removedFinishedLine;

    [SerializeField]
    GameObject finishLineObj, staionaryBorders, movingBorders;

    Vector3 spawnPos;
    int obstacleInt;
    float previousSpawnedLocation;

    bool changedColor;

    [SerializeField]
    TextMeshPro coinText;

    [SerializeField]
    Camera myCam;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        if (Screen.height / Screen.width > 1.75f)
            myCam.orthographicSize = 5.74f;
        else
            myCam.orthographicSize = 5.47f;

        if (PlayerPrefs.GetInt("ChangeColor", 0) == 1)
        {
            PlayerPrefs.SetInt("ChangeColor", 0);

            Color brickColor = colorArray[Random.Range(0, 19)];
            brickMat.SetColor("_Color", brickColor);

            float antiBrickH, S, V;
            Color.RGBToHSV(brickColor, out antiBrickH, out S, out V);

            if (antiBrickH <= 0.5f)
                antiBrickH += 0.5f;
            else
                antiBrickH -= 0.5f;

            Color antiBrickColor = Color.HSVToRGB(antiBrickH, 0.77f, 0.55f);
            antiBrickMat.SetColor("_Color", antiBrickColor);
        }

        SpawnLevel();

        levelStarted = false;
        levelFailed = false;
        levelPassed = false;
    }
        
    private void Start()
    {
        removedFinishedLine = false;
        changedColor = false;
    }

    private void Update()
    {
        coinText.text = PlayerPrefs.GetInt("CoinCount", 0).ToString();

        if (!changedColor && PlayerController.score % 10 == 0 && PlayerController.score > 9)
        {
            Color brickColor = colorArray[Random.Range(0, 19)];
            brickMat.SetColor("_Color", brickColor);

            float antiBrickH, S, V;
            Color.RGBToHSV(brickColor, out antiBrickH, out S, out V);

            if (antiBrickH <= 0.5f)
                antiBrickH += 0.5f;
            else
                antiBrickH -= 0.5f;

            Color antiBrickColor = Color.HSVToRGB(antiBrickH, 0.77f, 0.55f);
            antiBrickMat.SetColor("_Color", antiBrickColor);

            changedColor = true;
        }
        else if (changedColor && (PlayerController.score - 1) % 10 == 0)
            changedColor = false;

        // spawn level
        if (PlayerPrefs.GetInt("Mode", 0) == 1 && levelStarted && !levelFailed)
        {
            // spawn new obstacle
            if (planeTransform.position.y < previousSpawnedLocation - 11.28f)
            {
                obstacleInt = Random.Range(0, obstacleSlates.Length);
                spawnPos += new Vector3(0, obstacleDistances[obstacleInt], 0);
                Instantiate(obstacleSlates[obstacleInt], spawnPos, Quaternion.identity, levelObjectsFolder);

                previousSpawnedLocation = planeTransform.position.y;
            }
        }

        if (!removedFinishedLine && levelStarted && PlayerPrefs.GetInt("SprintCreateNewLevel", 1) == 0)
        {

            // remove finish line
            if (PlayerPrefs.GetInt("Mode", 0) == 1) // infinite mode
            {
                staionaryBorders.SetActive(false);
                movingBorders.SetActive(true);

                finishLineObj.SetActive(false);

                // spawn 2 more objects
                for (int j = 0; j < 2; j++)
                {
                    obstacleInt = Random.Range(0, obstacleSlates.Length);
                    spawnPos += new Vector3(0, obstacleDistances[obstacleInt], 0);
                    Instantiate(obstacleSlates[obstacleInt], spawnPos, Quaternion.identity, levelObjectsFolder);
                }
            }
            else // non infinite mode
            {
                // spawn obstacles 2 & 3
                if (PlayerPrefs.GetInt("CreateNewLevel", 1) == 1)
                {
                    // choose obstacle
                    for (int i = 0; i < 2; i++)
                    {                        
                        // choose obstacle
                        obstacleInt = Random.Range(0, obstacleSlates.Length);
                        if (i == 0)
                            PlayerPrefs.SetInt("StoredObstacle2", obstacleInt);
                        else if (i == 1)
                            PlayerPrefs.SetInt("StoredObstacle3", obstacleInt);
                        spawnPos += new Vector3(0, obstacleDistances[obstacleInt], 0);
                        Instantiate(obstacleSlates[obstacleInt], spawnPos, Quaternion.identity, levelObjectsFolder);
                    }

                    // Reset Create New Level Int
                    PlayerPrefs.SetInt("CreateNewLevel", 0);
                }
                else
                {
                    // choose obstacle
                    obstacleInt = PlayerPrefs.GetInt("StoredObstacle2", Random.Range(0, obstacleSlates.Length));
                    spawnPos += new Vector3(0, obstacleDistances[obstacleInt], 0);
                    Instantiate(obstacleSlates[obstacleInt], spawnPos, Quaternion.identity, levelObjectsFolder);

                    obstacleInt = PlayerPrefs.GetInt("StoredObstacle3", Random.Range(0, obstacleSlates.Length));
                    spawnPos += new Vector3(0, obstacleDistances[obstacleInt], 0);
                    Instantiate(obstacleSlates[obstacleInt], spawnPos, Quaternion.identity, levelObjectsFolder);
                }
            }
            previousSpawnedLocation = planeTransform.position.y;
            removedFinishedLine = true;
        }
    }

    void SpawnLevel()
    {
        spawnPos = new Vector3(0, 0, 0);
        
        if (PlayerPrefs.GetInt("CreateNewLevel", 1) == 1)
        {
            // choose obstacle
            obstacleInt = Random.Range(0, obstacleSlates.Length);
            PlayerPrefs.SetInt("StoredObstacle1", obstacleInt);
            Instantiate(obstacleSlates[obstacleInt], spawnPos, Quaternion.identity, levelObjectsFolder);
            PlayerPrefs.SetInt("SprintCreateNewLevel", 0);
        }
        else
        {
            // choose obstacle
            obstacleInt = PlayerPrefs.GetInt("StoredObstacle1", Random.Range(0, obstacleSlates.Length));
            Instantiate(obstacleSlates[obstacleInt], spawnPos, Quaternion.identity, levelObjectsFolder);
        }        
    }
}
