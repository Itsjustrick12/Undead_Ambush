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

    

    // Update is called once per frame
    void Update()
    {
        if (currentGun != null)
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

    public void ChangeGunUI(Gun newGun)
    {
        currentGun = newGun;
        gunImage.sprite = newGun.UISprite;
        gunName.text = newGun.name;
        
    }
}
