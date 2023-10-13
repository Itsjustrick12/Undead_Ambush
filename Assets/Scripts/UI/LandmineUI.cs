using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LandmineUI : MonoBehaviour
{
    public GameObject[] MinesImages;
    public void UpdateMines(int amt)
    {

        for (int i = 0; i < 3; i++)
        {
            if (amt-1 >= i)
            {
                MinesImages[i].SetActive(true);
            }
            else
            {
                MinesImages[i].SetActive(false);
            }
        }
    }
}
