using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopButtonLogic : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer shopButtonSR;

    [SerializeField]
    SpriteRenderer[] shopUI;

    [SerializeField]
    GameObject shopButtons;

    bool pressedDown;

    public static bool closedShop;

    public static bool[] shopButtonDown = new bool[8];
    public static bool[] shopButtonUp = new bool[8];
    public static bool[] shopButtonExited = new bool[8];

    [SerializeField]
    GameObject[] displayPlanes = new GameObject[8];

    [SerializeField]
    GameObject[] realPlanes = new GameObject[8];

    [SerializeField]
    TextMeshPro coinText;

    Color[] shopSquares = new Color[8];

    Color[] bgStartAlpha = new Color[8];

    [SerializeField]
    Animator[] planeAnim = new Animator[8];

    // Start is called before the first frame update
    void Start()
    {
        pressedDown = false;
        closedShop = false;
        shopButtonSR.color = new Color(1, 1, 1, 1);

        for (int i = 0; i < 8; i++)
        {
            shopButtons.SetActive(false);
            shopButtonDown[i] = false;
            shopButtonUp[i] = false;
            shopButtonExited[i] = false;
            displayPlanes[i].SetActive(false);
            realPlanes[i].SetActive(false);
        }

        realPlanes[PlayerPrefs.GetInt("PlaneNumber", 0)].SetActive(true);

        // set all ui to invis
        for (int i = 0; i < shopUI.Length; i++)
            shopUI[i].color = new Color(shopUI[i].color.r, shopUI[i].color.g, shopUI[i].color.b, 0);

        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.SetInt("CoinCount", 10000);
    }

    private void Update()
    {
        if (closedShop && PlayerPrefs.GetInt("ShopPostion", 0) == 1)
        {
            PlayerPrefs.SetInt("ShopPostion", 0);
            StartCoroutine(FadeShopOut());
            closedShop = false;
        }
        else if (closedShop)
            closedShop = false;

        for (int i = 0; i < 8; i++)
        {
            if (shopButtonDown[i])
            {
                switch (i)
                {
                    case 0:
                        switch (PlayerPrefs.GetInt("Plane1Setting", 3)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already sselected
                        {
                            case 2:
                                // set bg to brighter
                                shopUI[3].color = new Color(1, 1, 1, 0.42f);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 1:
                        switch (PlayerPrefs.GetInt("Plane2Setting", 1)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 1:
                                // set bg to brighter
                                shopUI[5].color = new Color(1, 1, 1, 0.42f);
                                break;
                            case 2:
                                // set bg to brighter
                                shopUI[5].color = new Color(1, 1, 1, 0.42f);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2:
                        switch (PlayerPrefs.GetInt("Plane3Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 1:
                                // set bg to brighter
                                shopUI[10].color = new Color(1, 1, 1, 0.42f);
                                break;
                            case 2:
                                // set bg to brighter
                                shopUI[10].color = new Color(1, 1, 1, 0.42f);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 3:
                        switch (PlayerPrefs.GetInt("Plane4Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 1:
                                // set bg to brighter
                                shopUI[15].color = new Color(1, 1, 1, 0.42f);
                                break;
                            case 2:
                                // set bg to brighter
                                shopUI[15].color = new Color(1, 1, 1, 0.42f);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 4:
                        switch (PlayerPrefs.GetInt("Plane5Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 1:
                                // set bg to brighter
                                shopUI[20].color = new Color(1, 1, 1, 0.42f);
                                break;
                            case 2:
                                // set bg to brighter
                                shopUI[20].color = new Color(1, 1, 1, 0.42f);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 5:
                        switch (PlayerPrefs.GetInt("Plane6Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 1:
                                // set bg to brighter
                                shopUI[25].color = new Color(1, 1, 1, 0.42f);
                                break;
                            case 2:
                                // set bg to brighter
                                shopUI[25].color = new Color(1, 1, 1, 0.42f);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 6:
                        switch (PlayerPrefs.GetInt("Plane7Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 1:
                                // set bg to brighter
                                shopUI[30].color = new Color(1, 1, 1, 0.42f);
                                break;
                            case 2:
                                // set bg to brighter
                                shopUI[30].color = new Color(1, 1, 1, 0.42f);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 7:
                        switch (PlayerPrefs.GetInt("Plane8Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 1:
                                // set bg to brighter
                                shopUI[35].color = new Color(1, 1, 1, 0.42f);
                                break;
                            case 2:
                                // set bg to brighter
                                shopUI[35].color = new Color(1, 1, 1, 0.42f);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:

                        break;
                }
                shopButtonDown[i] = false;
            }

            if (shopButtonUp[i])
            {
                switch (i)
                {
                    case 0:
                        switch (PlayerPrefs.GetInt("Plane1Setting", 3)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 2:
                                // set bg to darker
                                shopUI[3].color = new Color(1, 1, 1, 0.1f);
                                PlayerPrefs.SetInt("PlaneNumber", 0);
                                PlayerPrefs.SetInt("Plane1Setting", 3);

                                // update other planes
                                if (PlayerPrefs.GetInt("Plane2Setting", 1) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane2Setting", 2);
                                    shopUI[5].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane3Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane3Setting", 2);
                                    shopUI[10].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane4Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane4Setting", 2);
                                    shopUI[15].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane5Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane5Setting", 2);
                                    shopUI[20].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane6Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane6Setting", 2);
                                    shopUI[25].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane7Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane7Setting", 2);
                                    shopUI[30].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane8Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane8Setting", 2);
                                    shopUI[35].color = new Color(1, 1, 1, 0.2235294f);
                                }

                                StoreSetup();

                                break;
                            default:
                                break;
                        }
                        break;
                    case 1:
                        switch (PlayerPrefs.GetInt("Plane2Setting", 1)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 1:
                                // set bg to darker
                                if (PlayerPrefs.GetInt("CoinCount", 0) >= 150) {

                                    PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount", 0)-150);
                                    shopUI[5].color = new Color(1, 1, 1, 0.1f);
                                    PlayerPrefs.SetInt("PlaneNumber", 1);
                                    PlayerPrefs.SetInt("Plane2Setting", 3);

                                    PlayerPrefs.SetInt("Plane3Setting", 1);
                                    
                                    shopUI[6].enabled = false;
                                    shopUI[7].enabled = false;

                                    // update other planes
                                    if (PlayerPrefs.GetInt("Plane1Setting", 3) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane1Setting", 2);
                                        shopUI[3].color = new Color(1, 1, 1, 0.2235294f);
                                    }

                                    // update plane 3 shop square
                                    StoreSetup();
                                    shopUI[10].color = bgStartAlpha[2];

                                    coinText.text = PlayerPrefs.GetInt("CoinCount", 0).ToString();
                                }
                                else
                                    shopUI[5].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            case 2:
                                // set bg to darker
                                shopUI[5].color = new Color(1, 1, 1, 0.1f);
                                PlayerPrefs.SetInt("PlaneNumber", 1);
                                PlayerPrefs.SetInt("Plane2Setting", 3);

                                // update other planes
                                if (PlayerPrefs.GetInt("Plane1Setting", 3) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane1Setting", 2);
                                    shopUI[3].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane3Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane3Setting", 2);
                                    shopUI[10].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane4Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane4Setting", 2);
                                    shopUI[15].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane5Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane5Setting", 2);
                                    shopUI[20].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane6Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane6Setting", 2);
                                    shopUI[25].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane7Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane7Setting", 2);
                                    shopUI[30].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane8Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane8Setting", 2);
                                    shopUI[35].color = new Color(1, 1, 1, 0.2235294f);
                                }

                                StoreSetup();

                                break;
                            default:
                                break;
                        }
                        break;
                    case 2:
                        switch (PlayerPrefs.GetInt("Plane3Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 1:
                                // set bg to darker
                                if (PlayerPrefs.GetInt("CoinCount", 0) >= 380)
                                {

                                    PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount", 0) - 380);
                                    shopUI[10].color = new Color(1, 1, 1, 0.1f);
                                    PlayerPrefs.SetInt("PlaneNumber", 2);
                                    PlayerPrefs.SetInt("Plane3Setting", 3);

                                    PlayerPrefs.SetInt("Plane4Setting", 1);

                                    shopUI[11].enabled = false;
                                    shopUI[12].enabled = false;

                                    // update other planes
                                    if (PlayerPrefs.GetInt("Plane1Setting", 3) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane1Setting", 2);
                                        shopUI[3].color = new Color(1, 1, 1, 0.2235294f);
                                    }
                                    else if (PlayerPrefs.GetInt("Plane2Setting", 1) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane2Setting", 2);
                                        shopUI[5].color = new Color(1, 1, 1, 0.2235294f);
                                    }

                                    // update plane 3 shop square
                                    StoreSetup();
                                    shopUI[15].color = bgStartAlpha[3];

                                    coinText.text = PlayerPrefs.GetInt("CoinCount", 0).ToString();
                                }
                                else
                                    shopUI[10].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            case 2:
                                // set bg to darker
                                shopUI[10].color = new Color(1, 1, 1, 0.1f);
                                PlayerPrefs.SetInt("PlaneNumber", 2);
                                PlayerPrefs.SetInt("Plane3Setting", 3);

                                // update other planes
                                if (PlayerPrefs.GetInt("Plane1Setting", 3) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane1Setting", 2);
                                    shopUI[3].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane2Setting", 1) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane2Setting", 2);
                                    shopUI[5].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane4Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane4Setting", 2);
                                    shopUI[15].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane5Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane5Setting", 2);
                                    shopUI[20].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane6Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane6Setting", 2);
                                    shopUI[25].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane7Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane7Setting", 2);
                                    shopUI[30].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane8Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane8Setting", 2);
                                    shopUI[35].color = new Color(1, 1, 1, 0.2235294f);
                                }

                                StoreSetup();

                                break;
                            default:
                                break;
                        }
                        break;
                    case 3:
                        switch (PlayerPrefs.GetInt("Plane4Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 1:
                                // set bg to darker
                                if (PlayerPrefs.GetInt("CoinCount", 0) >= 460)
                                {

                                    PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount", 0) - 460);
                                    shopUI[15].color = new Color(1, 1, 1, 0.1f);
                                    PlayerPrefs.SetInt("PlaneNumber", 3);
                                    PlayerPrefs.SetInt("Plane4Setting", 3);

                                    PlayerPrefs.SetInt("Plane5Setting", 1);

                                    shopUI[16].enabled = false;
                                    shopUI[17].enabled = false;

                                    // update other planes
                                    if (PlayerPrefs.GetInt("Plane1Setting", 3) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane1Setting", 2);
                                        shopUI[3].color = new Color(1, 1, 1, 0.2235294f);
                                    }
                                    else if (PlayerPrefs.GetInt("Plane2Setting", 1) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane2Setting", 2);
                                        shopUI[5].color = new Color(1, 1, 1, 0.2235294f);
                                    }
                                    else if (PlayerPrefs.GetInt("Plane3Setting", 0) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane3Setting", 2);
                                        shopUI[10].color = new Color(1, 1, 1, 0.2235294f);
                                    }

                                    StoreSetup();
                                    shopUI[20].color = bgStartAlpha[4];

                                    coinText.text = PlayerPrefs.GetInt("CoinCount", 0).ToString();
                                }
                                else
                                    shopUI[15].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            case 2:
                                // set bg to darker
                                shopUI[15].color = new Color(1, 1, 1, 0.1f);
                                PlayerPrefs.SetInt("PlaneNumber", 3);
                                PlayerPrefs.SetInt("Plane4Setting", 3);

                                // update other planes
                                if (PlayerPrefs.GetInt("Plane1Setting", 3) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane1Setting", 2);
                                    shopUI[3].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane2Setting", 1) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane2Setting", 2);
                                    shopUI[5].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane3Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane3Setting", 2);
                                    shopUI[10].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane5Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane5Setting", 2);
                                    shopUI[20].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane6Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane6Setting", 2);
                                    shopUI[25].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane7Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane7Setting", 2);
                                    shopUI[30].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane8Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane8Setting", 2);
                                    shopUI[35].color = new Color(1, 1, 1, 0.2235294f);
                                }

                                StoreSetup();

                                break;
                            default:
                                break;
                        }
                        break;
                    case 4:
                        switch (PlayerPrefs.GetInt("Plane5Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 1:
                                // set bg to darker
                                if (PlayerPrefs.GetInt("CoinCount", 0) >= 610)
                                {

                                    PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount", 0) - 610);
                                    shopUI[20].color = new Color(1, 1, 1, 0.1f);
                                    PlayerPrefs.SetInt("PlaneNumber", 4);
                                    PlayerPrefs.SetInt("Plane5Setting", 3);

                                    PlayerPrefs.SetInt("Plane6Setting", 1);

                                    shopUI[21].enabled = false;
                                    shopUI[22].enabled = false;

                                    // update other planes
                                    if (PlayerPrefs.GetInt("Plane1Setting", 3) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane1Setting", 2);
                                        shopUI[3].color = new Color(1, 1, 1, 0.2235294f);
                                    }
                                    else if (PlayerPrefs.GetInt("Plane2Setting", 1) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane2Setting", 2);
                                        shopUI[5].color = new Color(1, 1, 1, 0.2235294f);
                                    }
                                    else if (PlayerPrefs.GetInt("Plane3Setting", 0) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane3Setting", 2);
                                        shopUI[10].color = new Color(1, 1, 1, 0.2235294f);
                                    }
                                    else if (PlayerPrefs.GetInt("Plane4Setting", 0) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane4Setting", 2);
                                        shopUI[15].color = new Color(1, 1, 1, 0.2235294f);
                                    }

                                    StoreSetup();
                                    shopUI[25].color = bgStartAlpha[5];

                                    coinText.text = PlayerPrefs.GetInt("CoinCount", 0).ToString();
                                }
                                else
                                    shopUI[20].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            case 2:
                                // set bg to darker
                                shopUI[20].color = new Color(1, 1, 1, 0.1f);
                                PlayerPrefs.SetInt("PlaneNumber", 4);
                                PlayerPrefs.SetInt("Plane5Setting", 3);

                                // update other planes
                                if (PlayerPrefs.GetInt("Plane1Setting", 3) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane1Setting", 2);
                                    shopUI[3].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane2Setting", 1) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane2Setting", 2);
                                    shopUI[5].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane3Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane3Setting", 2);
                                    shopUI[10].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane4Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane4Setting", 2);
                                    shopUI[15].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane6Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane6Setting", 2);
                                    shopUI[25].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane7Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane7Setting", 2);
                                    shopUI[30].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane8Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane8Setting", 2);
                                    shopUI[35].color = new Color(1, 1, 1, 0.2235294f);
                                }

                                StoreSetup();

                                break;
                            default:
                                break;
                        }
                        break;
                    case 5:
                        switch (PlayerPrefs.GetInt("Plane6Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 1:
                                // set bg to darker
                                if (PlayerPrefs.GetInt("CoinCount", 0) >= 850)
                                {

                                    PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount", 0) - 850);
                                    shopUI[25].color = new Color(1, 1, 1, 0.1f);
                                    PlayerPrefs.SetInt("PlaneNumber", 5);
                                    PlayerPrefs.SetInt("Plane6Setting", 3);

                                    PlayerPrefs.SetInt("Plane7Setting", 1);

                                    shopUI[26].enabled = false;
                                    shopUI[27].enabled = false;

                                    // update other planes
                                    if (PlayerPrefs.GetInt("Plane1Setting", 3) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane1Setting", 2);
                                        shopUI[3].color = new Color(1, 1, 1, 0.2235294f);
                                    }
                                    else if (PlayerPrefs.GetInt("Plane2Setting", 1) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane2Setting", 2);
                                        shopUI[5].color = new Color(1, 1, 1, 0.2235294f);
                                    }
                                    else if (PlayerPrefs.GetInt("Plane3Setting", 0) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane3Setting", 2);
                                        shopUI[10].color = new Color(1, 1, 1, 0.2235294f);
                                    }
                                    else if (PlayerPrefs.GetInt("Plane4Setting", 0) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane4Setting", 2);
                                        shopUI[15].color = new Color(1, 1, 1, 0.2235294f);
                                    }
                                    else if (PlayerPrefs.GetInt("Plane5Setting", 0) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane5Setting", 2);
                                        shopUI[20].color = new Color(1, 1, 1, 0.2235294f);
                                    }

                                    StoreSetup();
                                    shopUI[30].color = bgStartAlpha[6];

                                    coinText.text = PlayerPrefs.GetInt("CoinCount", 0).ToString();
                                }
                                else
                                    shopUI[25].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            case 2:
                                // set bg to darker
                                shopUI[25].color = new Color(1, 1, 1, 0.1f);
                                PlayerPrefs.SetInt("PlaneNumber", 5);
                                PlayerPrefs.SetInt("Plane6Setting", 3);

                                // update other planes
                                if (PlayerPrefs.GetInt("Plane1Setting", 3) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane1Setting", 2);
                                    shopUI[3].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane2Setting", 1) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane2Setting", 2);
                                    shopUI[5].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane3Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane3Setting", 2);
                                    shopUI[10].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane4Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane4Setting", 2);
                                    shopUI[15].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane5Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane5Setting", 2);
                                    shopUI[20].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane7Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane7Setting", 2);
                                    shopUI[30].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane8Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane8Setting", 2);
                                    shopUI[35].color = new Color(1, 1, 1, 0.2235294f);
                                }

                                StoreSetup();

                                break;
                            default:
                                break;
                        }
                        break;
                    case 6:
                        switch (PlayerPrefs.GetInt("Plane7Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 1:
                                // set bg to darker
                                if (PlayerPrefs.GetInt("CoinCount", 0) >= 1500)
                                {

                                    PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount", 0) - 1500);
                                    shopUI[30].color = new Color(1, 1, 1, 0.1f);
                                    PlayerPrefs.SetInt("PlaneNumber", 6);
                                    PlayerPrefs.SetInt("Plane7Setting", 3);

                                    PlayerPrefs.SetInt("Plane8Setting", 1);

                                    shopUI[31].enabled = false;
                                    shopUI[32].enabled = false;

                                    // update other planes
                                    if (PlayerPrefs.GetInt("Plane1Setting", 3) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane1Setting", 2);
                                        shopUI[3].color = new Color(1, 1, 1, 0.2235294f);
                                    }
                                    else if (PlayerPrefs.GetInt("Plane2Setting", 1) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane2Setting", 2);
                                        shopUI[5].color = new Color(1, 1, 1, 0.2235294f);
                                    }
                                    else if (PlayerPrefs.GetInt("Plane3Setting", 0) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane3Setting", 2);
                                        shopUI[10].color = new Color(1, 1, 1, 0.2235294f);
                                    }
                                    else if (PlayerPrefs.GetInt("Plane4Setting", 0) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane4Setting", 2);
                                        shopUI[15].color = new Color(1, 1, 1, 0.2235294f);
                                    }
                                    else if (PlayerPrefs.GetInt("Plane5Setting", 0) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane5Setting", 2);
                                        shopUI[20].color = new Color(1, 1, 1, 0.2235294f);
                                    }
                                    else if (PlayerPrefs.GetInt("Plane6Setting", 0) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane6Setting", 2);
                                        shopUI[25].color = new Color(1, 1, 1, 0.2235294f);
                                    }

                                    StoreSetup();
                                    shopUI[35].color = bgStartAlpha[7];

                                    coinText.text = PlayerPrefs.GetInt("CoinCount", 0).ToString();
                                }
                                else
                                    shopUI[30].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            case 2:
                                // set bg to darker
                                shopUI[30].color = new Color(1, 1, 1, 0.1f);
                                PlayerPrefs.SetInt("PlaneNumber", 6);
                                PlayerPrefs.SetInt("Plane7Setting", 3);

                                // update other planes
                                if (PlayerPrefs.GetInt("Plane1Setting", 3) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane1Setting", 2);
                                    shopUI[3].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane2Setting", 1) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane2Setting", 2);
                                    shopUI[5].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane3Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane3Setting", 2);
                                    shopUI[10].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane4Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane4Setting", 2);
                                    shopUI[15].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane5Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane5Setting", 2);
                                    shopUI[20].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane6Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane6Setting", 2);
                                    shopUI[25].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane8Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane8Setting", 2);
                                    shopUI[35].color = new Color(1, 1, 1, 0.2235294f);
                                }

                                StoreSetup();

                                break;
                            default:
                                break;
                        }
                        break;
                    case 7:
                        switch (PlayerPrefs.GetInt("Plane8Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 1:
                                // set bg to darker
                                if (PlayerPrefs.GetInt("CoinCount", 0) >= 6050)
                                {

                                    PlayerPrefs.SetInt("CoinCount", PlayerPrefs.GetInt("CoinCount", 0) - 6050);
                                    shopUI[35].color = new Color(1, 1, 1, 0.1f);
                                    PlayerPrefs.SetInt("PlaneNumber", 7);
                                    PlayerPrefs.SetInt("Plane8Setting", 3);

                                    shopUI[36].enabled = false;
                                    shopUI[37].enabled = false;

                                    // update other planes
                                    if (PlayerPrefs.GetInt("Plane1Setting", 3) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane1Setting", 2);
                                        shopUI[3].color = new Color(1, 1, 1, 0.2235294f);
                                    }
                                    else if (PlayerPrefs.GetInt("Plane2Setting", 1) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane2Setting", 2);
                                        shopUI[5].color = new Color(1, 1, 1, 0.2235294f);
                                    }
                                    else if (PlayerPrefs.GetInt("Plane3Setting", 0) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane3Setting", 2);
                                        shopUI[10].color = new Color(1, 1, 1, 0.2235294f);
                                    }
                                    else if (PlayerPrefs.GetInt("Plane4Setting", 0) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane4Setting", 2);
                                        shopUI[15].color = new Color(1, 1, 1, 0.2235294f);
                                    }
                                    else if (PlayerPrefs.GetInt("Plane5Setting", 0) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane5Setting", 2);
                                        shopUI[20].color = new Color(1, 1, 1, 0.2235294f);
                                    }
                                    else if (PlayerPrefs.GetInt("Plane6Setting", 0) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane6Setting", 2);
                                        shopUI[25].color = new Color(1, 1, 1, 0.2235294f);
                                    }
                                    else if (PlayerPrefs.GetInt("Plane7Setting", 0) == 3)
                                    {
                                        PlayerPrefs.SetInt("Plane7Setting", 2);
                                        shopUI[30].color = new Color(1, 1, 1, 0.2235294f);
                                    }

                                    coinText.text = PlayerPrefs.GetInt("CoinCount", 0).ToString();
                                }
                                else
                                    shopUI[35].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            case 2:
                                // set bg to darker
                                shopUI[35].color = new Color(1, 1, 1, 0.1f);
                                PlayerPrefs.SetInt("PlaneNumber", 7);
                                PlayerPrefs.SetInt("Plane8Setting", 3);

                                // update other planes
                                if (PlayerPrefs.GetInt("Plane1Setting", 3) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane1Setting", 2);
                                    shopUI[3].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane2Setting", 1) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane2Setting", 2);
                                    shopUI[5].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane3Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane3Setting", 2);
                                    shopUI[10].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane4Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane4Setting", 2);
                                    shopUI[15].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane5Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane5Setting", 2);
                                    shopUI[20].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane6Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane6Setting", 2);
                                    shopUI[25].color = new Color(1, 1, 1, 0.2235294f);
                                }
                                else if (PlayerPrefs.GetInt("Plane7Setting", 0) == 3)
                                {
                                    PlayerPrefs.SetInt("Plane7Setting", 2);
                                    shopUI[30].color = new Color(1, 1, 1, 0.2235294f);
                                }

                                StoreSetup();

                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                for (int j = 0; j < 8; j++)
                    realPlanes[j].SetActive(false);
                realPlanes[PlayerPrefs.GetInt("PlaneNumber", 0)].SetActive(true);

                shopButtonUp[i] = false;

                shopUI[2].color = shopSquares[0];
                shopUI[4].color = shopSquares[1];
                shopUI[9].color = shopSquares[2];
                shopUI[14].color = shopSquares[3];
                shopUI[19].color = shopSquares[4];
                shopUI[24].color = shopSquares[5];
                shopUI[29].color = shopSquares[6];
                shopUI[34].color = shopSquares[7];
            }

            if (shopButtonExited[i])
            {
                switch (i)
                {
                    case 0:
                        switch (PlayerPrefs.GetInt("Plane1Setting", 3)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 2:
                                // set bg to darker
                                shopUI[3].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 1:
                        switch (PlayerPrefs.GetInt("Plane2Setting", 1)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 1:
                                // set bg to darker
                                shopUI[5].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            case 2:
                                // set bg to darker
                                shopUI[5].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 2:
                        switch (PlayerPrefs.GetInt("Plane3Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 1:
                                // set bg to darker
                                shopUI[10].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            case 2:
                                // set bg to darker
                                shopUI[10].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 3:
                        switch (PlayerPrefs.GetInt("Plane4Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 1:
                                // set bg to darker
                                shopUI[15].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            case 2:
                                // set bg to darker
                                shopUI[15].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 4:
                        switch (PlayerPrefs.GetInt("Plane5Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 1:
                                // set bg to darker
                                shopUI[20].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            case 2:
                                // set bg to darker
                                shopUI[20].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 5:
                        switch (PlayerPrefs.GetInt("Plane6Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 1:
                                // set bg to darker
                                shopUI[25].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            case 2:
                                // set bg to darker
                                shopUI[25].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 6:
                        switch (PlayerPrefs.GetInt("Plane7Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 1:
                                // set bg to darker
                                shopUI[30].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            case 2:
                                // set bg to darker
                                shopUI[30].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            default:
                                break;
                        }
                        break;
                    case 7:
                        switch (PlayerPrefs.GetInt("Plane8Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
                        {
                            case 1:
                                // set bg to darker
                                shopUI[35].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            case 2:
                                // set bg to darker
                                shopUI[35].color = new Color(1, 1, 1, 0.2235294f);
                                break;
                            default:
                                break;
                        }
                        break;
                    default:
                        break;
                }
                shopButtonExited[i] = false;
            }
        }
    }

    void OnTouchDown()
    {
        pressedDown = true;
        shopButtonSR.color = new Color(1, 1, 1, 0.65f);
    }

    void OnTouchUp()
    {
        if (pressedDown)
        {
            if (PlayerPrefs.GetInt("ShopPostion", 0) == 0) // shop is closed
            {
                PlayerPrefs.SetInt("ShopPostion", 1);
                StartCoroutine(FadeShopIn());
            }
            else if (PlayerPrefs.GetInt("ShopPostion", 0) == 1) // shop is open
            {
                PlayerPrefs.SetInt("ShopPostion", 0);
                StartCoroutine(FadeShopOut());
            }
        }
        pressedDown = false;
    }

    void OnTouchExit()
    {
        if (pressedDown)
        {
            shopButtonSR.color = new Color(1, 1, 1, 1);
            pressedDown = false;
        }
    }

    IEnumerator FadeShopIn()
    {
        float timer = 0, totalTime = 9;

        shopButtons.SetActive(true);

        StoreSetup(); // determine store look

        while (timer <= totalTime)
        {
            yield return new WaitForFixedUpdate();
            for (int i = 0; i < shopUI.Length; i++)
            {
                if (i != 1 && i != 3 && i != 5 && i != 10 && i != 15 && i != 20 && i != 25 && i != 30 && i != 35 &&
                    i != 2 && i != 4 && i != 9 && i != 14 && i != 19 && i != 24 && i != 29 && i != 34)
                    shopUI[i].color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), timer / totalTime);
            }
            shopUI[1].color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 0.1215686f), timer / totalTime);
            shopUI[3].color = Color.Lerp(new Color(1, 1, 1, 0), bgStartAlpha[0], timer / totalTime);
            shopUI[5].color = Color.Lerp(new Color(1, 1, 1, 0), bgStartAlpha[1], timer / totalTime);
            shopUI[10].color = Color.Lerp(new Color(1, 1, 1, 0), bgStartAlpha[2], timer / totalTime);
            shopUI[15].color = Color.Lerp(new Color(1, 1, 1, 0), bgStartAlpha[3], timer / totalTime);
            shopUI[20].color = Color.Lerp(new Color(1, 1, 1, 0), bgStartAlpha[4], timer / totalTime);
            shopUI[25].color = Color.Lerp(new Color(1, 1, 1, 0), bgStartAlpha[5], timer / totalTime);
            shopUI[30].color = Color.Lerp(new Color(1, 1, 1, 0), bgStartAlpha[6], timer / totalTime);
            shopUI[35].color = Color.Lerp(new Color(1, 1, 1, 0), bgStartAlpha[7], timer / totalTime);
            shopUI[41].color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 0.2509804f), timer / totalTime);

            shopUI[2].color = Color.Lerp(new Color(shopSquares[0].r, shopSquares[0].g, shopSquares[0].b, 0), shopSquares[0], timer / totalTime);
            shopUI[4].color = Color.Lerp(new Color(shopSquares[1].r, shopSquares[1].g, shopSquares[1].b, 0), shopSquares[1], timer / totalTime);
            shopUI[9].color = Color.Lerp(new Color(shopSquares[2].r, shopSquares[2].g, shopSquares[2].b, 0), shopSquares[2], timer / totalTime);
            shopUI[14].color = Color.Lerp(new Color(shopSquares[3].r, shopSquares[3].g, shopSquares[3].b, 0), shopSquares[3], timer / totalTime);
            shopUI[19].color = Color.Lerp(new Color(shopSquares[4].r, shopSquares[4].g, shopSquares[4].b, 0), shopSquares[4], timer / totalTime);
            shopUI[24].color = Color.Lerp(new Color(shopSquares[5].r, shopSquares[5].g, shopSquares[5].b, 0), shopSquares[5], timer / totalTime);
            shopUI[29].color = Color.Lerp(new Color(shopSquares[6].r, shopSquares[6].b, shopSquares[6].b, 0), shopSquares[6], timer / totalTime);
            shopUI[34].color = Color.Lerp(new Color(shopSquares[7].r, shopSquares[7].g, shopSquares[7].b, 0), shopSquares[7], timer / totalTime);

            timer++;
        }

        StoreSetup();
    }

    // decide what is enabled/disabled
    // decide what color eachbg is based on which plane is currently selected
    void StoreSetup()
    {
        switch (PlayerPrefs.GetInt("Plane1Setting", 3)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
        {            
            case 2:
                planeAnim[0].SetBool("Chosen", false);
                bgStartAlpha[0] = new Color (1, 1, 1, 0.2235294f);
                shopSquares[0] = new Color(1, 1, 1, 1);
                break;
            case 3:
                planeAnim[0].SetBool("Chosen", true);
                planeAnim[0].SetTrigger("Picked");
                bgStartAlpha[0] = new Color(1, 1, 1, 0.1f);
                shopSquares[0] = new Color(1, 0.9686f, 0.84f, 1);
                break;
            default:
                planeAnim[0].SetBool("Chosen", true);
                planeAnim[0].SetTrigger("Picked");
                bgStartAlpha[0] = new Color(1, 1, 1, 0.1f);
                shopSquares[0] = new Color(1, 0.9686f, 0.84f, 1);
                break;
        }
        displayPlanes[0].SetActive(true);

        switch (PlayerPrefs.GetInt("Plane2Setting", 1)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
        {
            case 1:
                planeAnim[1].SetBool("Chosen", false);
                shopUI[6].enabled = true;
                shopUI[7].enabled = true;
                bgStartAlpha[1] = new Color(1, 1, 1, 0.2235294f);
                shopSquares[1] = new Color(1, 1, 1, 1);
                break;
            case 2:
                planeAnim[1].SetBool("Chosen", false);
                shopUI[6].enabled = false;
                shopUI[7].enabled = false;
                bgStartAlpha[1] = new Color(1, 1, 1, 0.2235294f);
                shopSquares[1] = new Color(1, 1, 1, 1);
                break;
            case 3:
                planeAnim[1].SetBool("Chosen", true);
                planeAnim[1].SetTrigger("Picked");
                shopUI[6].enabled = false;
                shopUI[7].enabled = false;
                bgStartAlpha[1] = new Color(1, 1, 1, 0.1f);
                shopSquares[1] = new Color(1, 0.9686f, 0.84f, 1);
                break;
            default:
                planeAnim[1].SetBool("Chosen", false);
                shopUI[6].enabled = true;
                shopUI[7].enabled = true;
                bgStartAlpha[1] = new Color(1, 1, 1, 0.2235294f);
                shopSquares[1] = new Color(1, 1, 1, 1);
                break;
        }
        shopUI[8].enabled = false;
        displayPlanes[1].SetActive(true);

        switch (PlayerPrefs.GetInt("Plane3Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
        {
            case 0:
                shopUI[11].enabled = false;
                shopUI[12].enabled = false;
                shopUI[13].enabled = true;
                bgStartAlpha[2] = new Color(0, 0, 0, 0.2509804f);
                shopSquares[2] = new Color(1, 1, 1, 1);
                displayPlanes[2].SetActive(false);
                break;
            case 1:
                planeAnim[2].SetBool("Chosen", false);
                shopUI[11].enabled = true;
                shopUI[12].enabled = true;
                shopUI[13].enabled = false;
                bgStartAlpha[2] = new Color(1, 1, 1, 0.2235294f);
                shopSquares[2] = new Color(1, 1, 1, 1);
                displayPlanes[2].SetActive(true);
                break;
            case 2:
                planeAnim[2].SetBool("Chosen", false);
                shopUI[11].enabled = false;
                shopUI[12].enabled = false;
                shopUI[13].enabled = false;
                bgStartAlpha[2] = new Color(1, 1, 1, 0.2235294f);
                shopSquares[2] = new Color(1, 1, 1, 1);
                displayPlanes[2].SetActive(true);
                break;
            case 3:
                planeAnim[2].SetBool("Chosen", true);
                planeAnim[2].SetTrigger("Picked");
                shopUI[11].enabled = false;
                shopUI[12].enabled = false;
                shopUI[13].enabled = false;
                bgStartAlpha[2] = new Color(1, 1, 1, 0.1f);
                shopSquares[2] = new Color(1, 0.9686f, 0.84f, 1);
                displayPlanes[2].SetActive(true);
                break;
            default:
                shopUI[11].enabled = false;
                shopUI[12].enabled = false;
                shopUI[13].enabled = true;
                bgStartAlpha[2] = new Color(0, 0, 0, 0.2509804f);
                shopSquares[2] = new Color(1, 1, 1, 1);
                displayPlanes[2].SetActive(false);
                break;
        }

        switch (PlayerPrefs.GetInt("Plane4Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
        {
            case 0:
                shopUI[16].enabled = false;
                shopUI[17].enabled = false;
                shopUI[18].enabled = true;
                bgStartAlpha[3] = new Color(0, 0, 0, 0.2509804f);
                shopSquares[3] = new Color(1, 1, 1, 1);
                displayPlanes[3].SetActive(false);
                break;
            case 1:
                planeAnim[3].SetBool("Chosen", false);
                shopUI[16].enabled = true;
                shopUI[17].enabled = true;
                shopUI[18].enabled = false;
                bgStartAlpha[3] = new Color(1, 1, 1, 0.2235294f);
                shopSquares[3] = new Color(1, 1, 1, 1);
                displayPlanes[3].SetActive(true);
                break;
            case 2:
                planeAnim[3].SetBool("Chosen", false);
                shopUI[16].enabled = false;
                shopUI[17].enabled = false;
                shopUI[18].enabled = false;
                bgStartAlpha[3] = new Color(1, 1, 1, 0.2235294f);
                shopSquares[3] = new Color(1, 1, 1, 1);
                displayPlanes[3].SetActive(true);
                break;
            case 3:
                planeAnim[3].SetBool("Chosen", true);
                planeAnim[3].SetTrigger("Picked");
                shopUI[16].enabled = false;
                shopUI[17].enabled = false;
                shopUI[18].enabled = false;
                bgStartAlpha[3] = new Color(1, 1, 1, 0.1f);
                shopSquares[3] = new Color(1, 0.9686f, 0.84f, 1);
                displayPlanes[3].SetActive(true);
                break;
            default:
                shopUI[16].enabled = false;
                shopUI[17].enabled = false;
                shopUI[18].enabled = true;
                bgStartAlpha[3] = new Color(0, 0, 0, 0.2509804f);
                shopSquares[3] = new Color(1, 1, 1, 1);
                displayPlanes[3].SetActive(false);
                break;
        }

        switch (PlayerPrefs.GetInt("Plane5Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
        {
            case 0:
                shopUI[21].enabled = false;
                shopUI[22].enabled = false;
                shopUI[23].enabled = true;
                bgStartAlpha[4] = new Color(0, 0, 0, 0.2509804f);
                shopSquares[4] = new Color(1, 1, 1, 1);
                displayPlanes[4].SetActive(false);
                break;
            case 1:
                planeAnim[4].SetBool("Chosen", false);
                shopUI[21].enabled = true;
                shopUI[22].enabled = true;
                shopUI[23].enabled = false;
                bgStartAlpha[4] = new Color(1, 1, 1, 0.2235294f);
                shopSquares[4] = new Color(1, 1, 1, 1);
                displayPlanes[4].SetActive(true);
                break;
            case 2:
                planeAnim[4].SetBool("Chosen", false);
                shopUI[21].enabled = false;
                shopUI[22].enabled = false;
                shopUI[23].enabled = false;
                bgStartAlpha[4] = new Color(1, 1, 1, 0.2235294f);
                shopSquares[4] = new Color(1, 1, 1, 1);
                displayPlanes[4].SetActive(true);
                break;
            case 3:
                planeAnim[4].SetBool("Chosen", true);
                planeAnim[4].SetTrigger("Picked");
                shopUI[21].enabled = false;
                shopUI[22].enabled = false;
                shopUI[23].enabled = false;
                bgStartAlpha[4] = new Color(1, 1, 1, 0.1f);
                shopSquares[4] = new Color(1, 0.9686f, 0.84f, 1);
                displayPlanes[4].SetActive(true);
                break;
            default:
                shopUI[21].enabled = false;
                shopUI[22].enabled = false;
                shopUI[23].enabled = true;
                bgStartAlpha[4] = new Color(0, 0, 0, 0.2509804f);
                shopSquares[4] = new Color(1, 1, 1, 1);
                displayPlanes[4].SetActive(false);
                break;
        }

        switch (PlayerPrefs.GetInt("Plane6Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
        {
            case 0:
                shopUI[26].enabled = false;
                shopUI[27].enabled = false;
                shopUI[28].enabled = true;
                bgStartAlpha[5] = new Color(0, 0, 0, 0.2509804f);
                shopSquares[5] = new Color(1, 1, 1, 1);
                displayPlanes[5].SetActive(false);
                break;
            case 1:
                planeAnim[5].SetBool("Chosen", false);
                shopUI[26].enabled = true;
                shopUI[27].enabled = true;
                shopUI[28].enabled = false;
                bgStartAlpha[5] = new Color(1, 1, 1, 0.2235294f);
                shopSquares[5] = new Color(1, 1, 1, 1);
                displayPlanes[5].SetActive(true);
                break;
            case 2:
                planeAnim[5].SetBool("Chosen", false);
                shopUI[26].enabled = false;
                shopUI[27].enabled = false;
                shopUI[28].enabled = false;
                bgStartAlpha[5] = new Color(1, 1, 1, 0.2235294f);
                shopSquares[5] = new Color(1, 1, 1, 1);
                displayPlanes[5].SetActive(true);
                break;
            case 3:
                planeAnim[5].SetBool("Chosen", true);
                planeAnim[5].SetTrigger("Picked");
                shopUI[26].enabled = false;
                shopUI[27].enabled = false;
                shopUI[28].enabled = false;
                bgStartAlpha[5] = new Color(1, 1, 1, 0.1f);
                shopSquares[5] = new Color(1, 0.9686f, 0.84f, 1);
                displayPlanes[5].SetActive(true);
                break;
            default:
                shopUI[26].enabled = false;
                shopUI[27].enabled = false;
                shopUI[28].enabled = true;
                bgStartAlpha[5] = new Color(0, 0, 0, 0.2509804f);
                shopSquares[5] = new Color(1, 1, 1, 1);
                displayPlanes[5].SetActive(false);
                break;
        }

        switch (PlayerPrefs.GetInt("Plane7Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
        {
            case 0:
                shopUI[31].enabled = false;
                shopUI[32].enabled = false;
                shopUI[33].enabled = true;
                bgStartAlpha[6] = new Color(0, 0, 0, 0.2509804f);
                shopSquares[6] = new Color(1, 1, 1, 1);
                displayPlanes[6].SetActive(false);
                break;
            case 1:
                planeAnim[6].SetBool("Chosen", false);
                shopUI[31].enabled = true;
                shopUI[32].enabled = true;
                shopUI[33].enabled = false;
                bgStartAlpha[6] = new Color(1, 1, 1, 0.2235294f);
                shopSquares[6] = new Color(1, 1, 1, 1);
                displayPlanes[6].SetActive(true);
                break;
            case 2:
                planeAnim[6].SetBool("Chosen", false);
                shopUI[31].enabled = false;
                shopUI[32].enabled = false;
                shopUI[33].enabled = false;
                bgStartAlpha[6] = new Color(1, 1, 1, 0.2235294f);
                shopSquares[6] = new Color(1, 1, 1, 1);
                displayPlanes[6].SetActive(true);
                break;
            case 3:
                planeAnim[6].SetBool("Chosen", true);
                planeAnim[6].SetTrigger("Picked");
                shopUI[31].enabled = false;
                shopUI[32].enabled = false;
                shopUI[33].enabled = false;
                bgStartAlpha[6] = new Color(1, 1, 1, 0.1f);
                shopSquares[6] = new Color(1, 0.9686f, 0.84f, 1);
                displayPlanes[6].SetActive(true);
                break;
            default:
                shopUI[31].enabled = false;
                shopUI[32].enabled = false;
                shopUI[33].enabled = true;
                bgStartAlpha[6] = new Color(0, 0, 0, 0.2509804f);
                shopSquares[6] = new Color(1, 1, 1, 1);
                displayPlanes[6].SetActive(false);
                break;
        }

        switch (PlayerPrefs.GetInt("Plane8Setting", 0)) // 0 is locked, 1 is not purchased, 2 is purchased, 3 is already selected
        {
            case 0:
                shopUI[36].enabled = false;
                shopUI[37].enabled = false;
                shopUI[38].enabled = true;
                bgStartAlpha[7] = new Color(0, 0, 0, 0.2509804f);
                shopSquares[7] = new Color(1, 1, 1, 1);
                displayPlanes[7].SetActive(false);
                break;
            case 1:
                planeAnim[7].SetBool("Chosen", false);
                shopUI[36].enabled = true;
                shopUI[37].enabled = true;
                shopUI[38].enabled = false;
                bgStartAlpha[7] = new Color(1, 1, 1, 0.2235294f);
                shopSquares[7] = new Color(1, 1, 1, 1);
                displayPlanes[7].SetActive(true);
                break;
            case 2:
                planeAnim[7].SetBool("Chosen", false);
                shopUI[36].enabled = false;
                shopUI[37].enabled = false;
                shopUI[38].enabled = false;
                bgStartAlpha[7] = new Color(1, 1, 1, 0.2235294f);
                shopSquares[7] = new Color(1, 1, 1, 1);
                displayPlanes[7].SetActive(true);
                break;
            case 3:
                planeAnim[7].SetBool("Chosen", true);
                planeAnim[7].SetTrigger("Picked");
                shopUI[36].enabled = false;
                shopUI[37].enabled = false;
                shopUI[38].enabled = false;
                bgStartAlpha[7] = new Color(1, 1, 1, 0.1f);
                shopSquares[7] = new Color(1, 0.9686f, 0.84f, 1);
                displayPlanes[7].SetActive(true);
                break;
            default:
                shopUI[36].enabled = false;
                shopUI[37].enabled = false;
                shopUI[38].enabled = true;
                bgStartAlpha[7] = new Color(0, 0, 0, 0.2509804f);
                shopSquares[7] = new Color(1, 1, 0.84f, 1);
                displayPlanes[7].SetActive(false);
                break;
        }
    }

    IEnumerator FadeShopOut()
    {
        float timer = 0, totalTime = 9;

        float[] shopUIStartingAlpha = new float[shopUI.Length];

        for (int i = 0; i < shopUIStartingAlpha.Length; i++)
            shopUIStartingAlpha[i] = shopUI[i].color.a;

        shopButtons.SetActive(false);

        for (int i = 0; i < 8; i++)
            displayPlanes[i].SetActive(false);

        while (timer <= totalTime)
        {
            yield return new WaitForFixedUpdate();
            shopButtonSR.color = Color.Lerp(new Color(1, 1, 1, 0.65f), new Color(1, 1, 1, 1), timer / totalTime);

            for (int i = 0; i < shopUI.Length; i++)
            {
                if (i != 1 && i != 3 && i != 5 && i != 10 && i != 15 && i != 20 && i != 25 && i != 30 && i != 35 &&
                    i != 2 && i != 4 && i != 9 && i != 14 && i != 19 && i != 24 && i != 29 && i != 34)
                    shopUI[i].color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), timer / totalTime);
            }
            
            shopUI[1].color = Color.Lerp(new Color(1, 1, 1, 0.1215686f), new Color(1, 1, 1, 0), timer / totalTime);
            shopUI[3].color = Color.Lerp(bgStartAlpha[0], new Color(1, 1, 1, 0), timer / totalTime);
            shopUI[5].color = Color.Lerp(bgStartAlpha[1], new Color(1, 1, 1, 0), timer / totalTime);
            shopUI[10].color = Color.Lerp(bgStartAlpha[2], new Color(1, 1, 1, 0), timer / totalTime);
            shopUI[15].color = Color.Lerp(bgStartAlpha[3], new Color(1, 1, 1, 0), timer / totalTime);
            shopUI[20].color = Color.Lerp(bgStartAlpha[4], new Color(1, 1, 1, 0), timer / totalTime);
            shopUI[25].color = Color.Lerp(bgStartAlpha[5], new Color(1, 1, 1, 0), timer / totalTime);
            shopUI[30].color = Color.Lerp(bgStartAlpha[6], new Color(1, 1, 1, 0), timer / totalTime);
            shopUI[35].color = Color.Lerp(bgStartAlpha[7], new Color(1, 1, 1, 0), timer / totalTime);
            shopUI[41].color = Color.Lerp(new Color(0, 0, 0, 0.2509804f), new Color(0, 0, 0, 0), timer / totalTime);

            shopUI[2].color = Color.Lerp(shopSquares[0], new Color(shopSquares[0].r, shopSquares[0].g, shopSquares[0].b, 0), timer / totalTime);
            shopUI[4].color = Color.Lerp(shopSquares[1], new Color(shopSquares[1].r, shopSquares[1].g, shopSquares[1].b, 0), timer / totalTime);
            shopUI[9].color = Color.Lerp(shopSquares[2], new Color(shopSquares[2].r, shopSquares[2].g, shopSquares[2].b, 0), timer / totalTime);
            shopUI[14].color = Color.Lerp(shopSquares[3], new Color(shopSquares[3].r, shopSquares[3].g, shopSquares[3].b, 0), timer / totalTime);
            shopUI[19].color = Color.Lerp(shopSquares[4], new Color(shopSquares[4].r, shopSquares[4].g, shopSquares[4].b, 0), timer / totalTime);
            shopUI[24].color = Color.Lerp(shopSquares[5], new Color(shopSquares[5].r, shopSquares[5].g, shopSquares[5].b, 0), timer / totalTime);
            shopUI[29].color = Color.Lerp(shopSquares[6], new Color(shopSquares[6].r, shopSquares[6].b, shopSquares[6].b, 0), timer / totalTime);
            shopUI[34].color = Color.Lerp(shopSquares[7], new Color(shopSquares[7].r, shopSquares[7].g, shopSquares[7].b, 0), timer / totalTime);

            timer++;
        }
    }
}