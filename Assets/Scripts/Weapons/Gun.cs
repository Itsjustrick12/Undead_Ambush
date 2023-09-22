using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    public Sprite UISprite;
    public Sprite sprite;

    public float damage;
    [Range(0.0f,1.5f)]
    public float fireRate;
    public int mBullets;
    public int currentBullets;
    [Range(0.0F, 2.0F)]
    public float rTime;
    [Range(20.0f, 35.0f)]
    public float bSpeed = 15;

    //For piercing
    public int maxPierce = 0;
    public AudioClip gunShot;

    //For Determing whether the fire button can be held down to shoot continously
    public bool autoFire = false;

    //For buying guns
    public int cost = 100;
    public int upgradeCost = 1000;

    public void Reload()
    {
        currentBullets = mBullets;
    }

}
