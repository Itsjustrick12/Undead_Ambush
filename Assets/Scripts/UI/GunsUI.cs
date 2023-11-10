using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunsUI : MonoBehaviour
{
    private Gun currentGun;

    public TextMeshProUGUI gunName;
    public Image gunImage;

    public Image[] bullets;
    public Sprite bullet;
    public Sprite emptyBullet;

    bool isReloading = false;

    

    // Update is called once per frame
    void Update()
    {
        if (currentGun != null && !isReloading)
        {
            

            if (currentGun.currentBullets > currentGun.mBullets)
            {
                currentGun.currentBullets = currentGun.mBullets;
            }

            for (int i = 0; i < bullets.Length; i++)
            {
                if (i < currentGun.currentBullets)
                {
                    bullets[i].sprite = bullet;
                }
                else
                {
                    bullets[i].sprite = emptyBullet;
                }

                if (i < currentGun.mBullets)
                {
                    bullets[i].enabled = true;
                }
                else
                {
                    bullets[i].enabled = false;
                }
            }
        }
    }

    IEnumerator reloadBullets(float waitTime)
    {
        for (int i = 0; i < currentGun.currentBullets; i++)
        {
            if (i < currentGun.currentBullets)
            {
                bullets[i].sprite = bullet;
            }
            yield return new WaitForSeconds(waitTime);
        }
        isReloading = false;
    }

    public void reloadAnim(float reloadTime)
    {
        isReloading = true;
        StartCoroutine("reloadBullets", reloadTime / currentGun.mBullets);

    }

    public void ChangeGunUI(Gun newGun)
    {
        currentGun = newGun;
        gunImage.sprite = newGun.UISprite;
        gunName.text = newGun.name;
        
    }
}
