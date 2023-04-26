using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShouldDisapear : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.GetInt("Mode", 0) == 0) {
            int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
            int lowerLimit = 8;
            if (currentLevel % 5 != 0)
            {
                if ((currentLevel + 1) % 10 == 0)
                    lowerLimit = 3;
                else if ((currentLevel + 1) % 5 == 0)
                    lowerLimit = 8;
                else if (currentLevel % 3 == 0)
                    lowerLimit = 15;
                else if (currentLevel % 2 == 0)
                    lowerLimit = 11;
                if (Random.Range(1, 22) >= lowerLimit)
                    Destroy(gameObject);
            }
            else
            {
                if (Random.Range(1, 22) >= 18)
                    Destroy(gameObject);
            }
        }
        else
        {
            if (Random.Range(1, 22) >= 11)
                Destroy(gameObject);
        }
    }
}
