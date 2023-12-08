using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreCoinUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;
    // Start is called before the first frame update
    private void OnEnable()
    {
        PlayerMoney money = FindObjectOfType<PlayerMoney>();
        UpdateCoins(money.coins);
    }
    public void UpdateCoins(int amt)
    {
        coinText.text = amt.ToString();
    }
}
