using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LandmineUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject[] MinesImages;


    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateMines(int amt)
    {
        text.text = amt.ToString();

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
