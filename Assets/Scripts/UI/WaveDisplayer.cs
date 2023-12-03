using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveDisplayer : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateText(int current, int total)
    {
        if (total > 0)
        {
            text.text = current + "/" + total;
        }
        else
        {
            text.text = current.ToString();
        }
    }
}
