using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrownCheck : MonoBehaviour
{
    [SerializeField] Sprite sprite;
    Image img;

    void Start(){
        img = GetComponent<Image>();
        PlayerData pd = SaveManager.Load();
        if (pd.beatEndless)
        {
            img.sprite = sprite;
        }
    }
}
