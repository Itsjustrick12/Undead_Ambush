using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBoundsCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            FindObjectOfType<GameManager>().GameOver(false);
        }
    }
}
