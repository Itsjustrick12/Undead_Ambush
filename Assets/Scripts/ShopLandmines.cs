using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopLandmines : MonoBehaviour
{
    public int minesCost = 25;
    [SerializeField] Shooting playerShooting;
    [SerializeField] PlayerMoney playerMoney;
    [SerializeField] TextMeshProUGUI minesText;


    private void OnEnable()
    {
        UpdateMinesCount();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerShooting = FindObjectOfType<Shooting>();
        playerMoney = FindObjectOfType<PlayerMoney>();
        minesText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void UpdateMinesCount()
    {
        int tempAmt = playerShooting.GetMines();
        minesText.text = tempAmt.ToString() + "/" + playerShooting.maxMines;

    }

    public void BuyMine()
    {
        if (playerShooting.CanBuyMines())
        {
            if (playerMoney.coins >= minesCost)
            {
                playerShooting.addMines();
                UpdateMinesCount();
                FindObjectOfType<LandmineUI>().UpdateMines(playerShooting.GetMines());
                playerMoney.RemoveCoins(minesCost);
            }
        }
    }
}
