using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    // Start is called before the first frame update

    // Update is called once per frame
    public void UpdateText(int newDead, int newWaves, int oldDead, int oldWaves)
    {
        if (newDead >= oldDead && newWaves >= oldWaves)
        {
            text.fontSize = 24;
            text.text = "NEW HIGH SCORE!";
        }
        else
        {
            text.fontSize = 18;
            text.text = "High Score: " + oldDead.ToString() + " zombies killed" + " after " + oldWaves + " waves";
        }
    }
}
