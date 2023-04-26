using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeExplosionColor : MonoBehaviour
{
    [SerializeField]
    ParticleSystem myParticleSystem;

    private void OnEnable()
    {
        switch (PlayerPrefs.GetInt("PlaneNumber", 0))
        {
            case 0:
                myParticleSystem.startColor = Color.white;
                break;
            case 1:
                myParticleSystem.startColor = Color.red;
                break;
            case 2:
                myParticleSystem.startColor = Color.blue;
                break;
            case 3:
                myParticleSystem.startColor = Color.yellow;
                break;
            case 4:
                myParticleSystem.startColor = new Color(0.6039f, 0.2799f, 0.1333f, 1); // orange
                break;
            case 5:
                myParticleSystem.startColor = new Color(0.1333f, 0.6039f, 0.1984f, 1); // green                
                break;
            case 6:
                myParticleSystem.startColor = new Color(0.4488f, 0.1333f, 0.6039f, 1); // purple
                break;
            case 7:
                myParticleSystem.startColor = new Color(0, 0, 0, 1); // black
                break;
            default:
                myParticleSystem.startColor = Color.white;
                break;
        }
    }
}
