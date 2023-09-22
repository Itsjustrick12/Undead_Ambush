using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI reasonText;

    public void Infected() {
        reasonText.text = "You Were Infected!";
    }
    public void Zombie() {
        reasonText.text = "A Zombie Got Away!";
    }
}
