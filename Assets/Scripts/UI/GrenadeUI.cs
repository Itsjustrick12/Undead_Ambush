using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrenadeUI : MonoBehaviour
{
    public GameObject[] GrenadeImages;

    public void UpdateGrenades(int amt)
    {

        for (int i = 0; i < 3; i++)
        {
            if (amt - 1 >= i)
            {
                GrenadeImages[i].SetActive(true);
            }
            else
            {
                GrenadeImages[i].SetActive(false);
            }
        }
    }
}
