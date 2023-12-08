using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopGunUI : MonoBehaviour
{
    private PlayerMoney playerMon;
    private int purchasePrice;
    private int upgradePrice;
    [SerializeField] private Image GunImage;
    [SerializeField] private TextMeshProUGUI purchasePriceText;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private TextMeshProUGUI upgradePriceText;
    public Gun gunProduct;
    public bool purchased = false;
    public bool upgraded = false;
    StoreCoinUI storeCoin;

    private void Start()
    {
        //Initialize all variables and make the element display the selected gun's data
        playerMon = FindObjectOfType<PlayerMoney>();
        GunImage.sprite = gunProduct.UISprite;
        purchasePrice = gunProduct.cost;
        purchasePriceText.text = "$" + purchasePrice.ToString();
        storeCoin = FindObjectOfType<StoreCoinUI>();

        upgradePrice = gunProduct.upgradeCost;
        upgradePriceText.text = "$" + upgradePrice.ToString();
        upgraded = false;

        if (purchased)
        {
            HidePurchaseOptions();
        }
    }

    public void PurchaseGun()
    {
       //If the player doesn't have the gun yet, buy it
        if (purchased == false && CanPurchase())
        {
            //Reload the gun and add it to the player's weapon set
            gunProduct.Reload();
            FindObjectOfType<Shooting>().AddGun(gunProduct);
            playerMon.RemoveCoins(purchasePrice);
            storeCoin.UpdateCoins(playerMon.coins);
            purchased = true;
            HidePurchaseOptions();
        }

        upgraded = true;
        if (purchased && upgraded)
        {
            HideCard();
        }
    }

    private bool CanPurchase()
    {
        if (playerMon.coins >= purchasePrice)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool CanUpgrade()
    {
        if (playerMon.coins >= upgradePrice && purchased)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpgradeGun()
    {
        //Upgrade the gun.
    }

    private void HidePurchaseOptions()
    {
        purchasePriceText.gameObject.SetActive(false);
        buyButton.SetActive(false);
    }

    private void HideCard()
    {
        FindObjectOfType<shopEmptyEnabler>().CheckEmptyShop();
        Destroy(this.gameObject);
    }
}
