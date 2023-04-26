using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningLogic : MonoBehaviour
{
    public GameObject halfBrickSheetObj;
    public GameObject[] brickSheets;
    public Transform[] triggers = new Transform[3];
    public Transform brickSheetsTransform;
    float[] bounds = new float[4];

    // Start is called before the first frame update
    void Awake()
    {
        // Bottom Bound
        bounds[0] = -13.44f;
        // Left Bound
        bounds[1] = -6.91f;
        // Right Bound
        bounds[2] = 3.455f;
        // move down by 6.72, 3.455
        bounds[3] = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Spawn Trigger Logic
        if (other.tag == "BottomTrigger")
        {
            triggers[0].localPosition = new Vector3(triggers[0].localPosition.x, triggers[0].localPosition.y - 6.72f, triggers[0].localPosition.z);
            for (int i = 0; i < brickSheets.Length; i++)
            {
                if (brickSheets[i].transform.localPosition.y == bounds[3])
                    brickSheets[i].transform.localPosition = new Vector3(brickSheets[i].transform.localPosition.x, bounds[0] - 6.72f, brickSheets[i].transform.localPosition.z);
            }
            bounds[0] -= 6.72f;
            bounds[3] -= 6.72f;
        }
        else if (other.tag == "LeftTrigger")
        {
            triggers[1].localPosition = new Vector3(triggers[1].localPosition.x - 3.455f, triggers[1].localPosition.y, triggers[1].localPosition.z);
            triggers[2].localPosition = new Vector3(triggers[2].localPosition.x - 3.455f, triggers[2].localPosition.y, triggers[2].localPosition.z);

            for (int i = 0; i < brickSheets.Length; i++)
            {
                if (brickSheets[i].transform.localPosition.x == bounds[2])
                    brickSheets[i].transform.localPosition = new Vector3(bounds[1] - 3.455f, brickSheets[i].transform.localPosition.y, brickSheets[i].transform.localPosition.z);
            }
            bounds[1] -= 3.455f;
            bounds[2] -= 3.455f;
        }
        else if (other.tag == "RightTrigger")
        {
            triggers[1].localPosition = new Vector3(triggers[1].localPosition.x + 3.455f, triggers[1].localPosition.y, triggers[1].localPosition.z);
            triggers[2].localPosition = new Vector3(triggers[2].localPosition.x + 3.455f, triggers[2].localPosition.y, triggers[2].localPosition.z);

            for (int i = 0; i < brickSheets.Length; i++)
            {
                if (brickSheets[i].transform.localPosition.x == bounds[1])
                    brickSheets[i].transform.localPosition = new Vector3(bounds[2] + 3.455f, brickSheets[i].transform.localPosition.y, brickSheets[i].transform.localPosition.z);
            }
            bounds[1] += 3.455f;
            bounds[2] += 3.455f;
        }
    }
}
