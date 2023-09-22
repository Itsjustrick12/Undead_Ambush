using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateBullets : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Shooting player;
    void Start()
    {
        player = FindObjectOfType<Shooting>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Bullets Left:  " + player.bulletsRemaining;
    }
}
