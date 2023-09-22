using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrenadeUI : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject[] GrenadeImages;

    private void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateGrenades(int amt)
    {
        text.text = amt.ToString();

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
