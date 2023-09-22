using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    public int coins = 0;
    private CoinUI coinUI;

    private void Start()
    {
        coinUI = FindObjectOfType<CoinUI>();
        coinUI.UpdateCoins(coins);
    }

    public void AddCoins (int numCoins)
    {
        coins += numCoins;
        coinUI.UpdateCoins(coins);
    }
    public void RemoveCoins (int numCoins)
    {
        coins -= numCoins;
        if (coins < 0)
        {
            coins = 0;
        }
        coinUI.UpdateCoins(coins);
    }
     public int CheckCoins()
    {
        return coins;
    }

}
