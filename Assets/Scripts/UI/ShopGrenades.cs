using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopGrenades : MonoBehaviour
{
    public int grenadeCost = 25;
    [SerializeField] Shooting playerShooting;
    [SerializeField] PlayerMoney playerMoney;
    [SerializeField] TextMeshProUGUI grenadesText;
    private StoreCoinUI storeCoin;

    private void OnEnable()
    {
        UpdateGrenadeCount();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerShooting = FindObjectOfType<Shooting>();
        playerMoney = FindObjectOfType<PlayerMoney>();
        grenadesText = GetComponentInChildren<TextMeshProUGUI>();
        storeCoin = FindObjectOfType<StoreCoinUI>();
    }

    private void UpdateGrenadeCount()
    {
        int tempAmt= playerShooting.GetGrenades();
        grenadesText.text = tempAmt.ToString() + "/" + playerShooting.maxGrenades;

    }

    public void BuyGrenade()
    {
        if (playerShooting.CanBuyGrenades())
        {
            if (playerMoney.coins >= grenadeCost)
            {
                playerShooting.addGrenades();
                UpdateGrenadeCount();
                FindObjectOfType<GrenadeUI>().UpdateGrenades(playerShooting.GetGrenades());
                playerMoney.RemoveCoins(grenadeCost);
                storeCoin.UpdateCoins(playerMoney.coins);
            }
        }
    }
}
