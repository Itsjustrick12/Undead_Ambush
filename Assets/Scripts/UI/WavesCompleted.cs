using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WavesCompleted : MonoBehaviour
{
    public TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        UpdateText(); 
    }

    public void UpdateText()
    {
        text.text = "You survived " + (FindObjectOfType<Spawner>().currentWave-1) + " waves" + " and defeated " + FindObjectOfType<GameManager>().kills + " zombies!";
    }
}
