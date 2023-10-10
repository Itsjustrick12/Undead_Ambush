using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    private bool showing = true;
    // Start is called before the first frame update
    void Start()
    {
        coinText = GetComponentInChildren<TextMeshProUGUI>();
        if (FindObjectOfType<Spawner>().endless)
        {
            showing = true;
            this.gameObject.SetActive(true);
        }
        else
        {
            showing = false;
            this.gameObject.SetActive(false);
        }
    }

    public void UpdateCoins(int amt)
    {
        if (showing)
        {
            string temp = amt.ToString();
            coinText.SetText(temp);
        }
        else
        {
            return;
        }
    }
}
