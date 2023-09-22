using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject bullet;

    [SerializeField] private SpriteRenderer gunSprite;

    //Stats modified by Gun Class
    private float fireRate = .1f;
    private float nextFire = 0f;
    private float gunDamage = 1;
    private int maxBullets;
    public int bulletsRemaining;
    

    //Gun Object used to update Shooting class
    [SerializeField] private Gun currentGun;

    //List of Guns in players aresenal
    public List<Gun> Guns = new List<Gun>();

    //For Reloading
    private bool canReload = true;
    private bool isReloading = false;
    private float reloadTime = 1f;

    //For Grenades
    private int grenades;
    public int maxGrenades;
    public Transform grenadePoint;
    public GameObject grenade;

    //For Landmines
    private int mines;
    public int maxMines;
    public Transform minePoint;
    public GameObject mine;

    private PlayerMovement playerMove;
    private GameManager gameManager;

    public GameObject projectileContainer;

    void Start()
    {
        projectileContainer = GameObject.Find("Projectile Container");

        //Get References to other scripts
        playerMove = GetComponent<PlayerMovement>();
        gameManager = FindObjectOfType<GameManager>();

        //Initialize Weapons
        MaxReload();
        currentGun = Guns[0];
        ChangeGun(currentGun);
        mines = maxMines;
        grenades = maxGrenades;

        //Intialize Weapon UI Counters
        FindObjectOfType<LandmineUI>().UpdateMines(mines);
        FindObjectOfType<GrenadeUI>().UpdateGrenades(grenades);

    }
    void Update()
    {
        //If the game isnt paused do everything needed for the script to work
        if (!gameManager.gamePaused)
        {

            if (CanReload()) {
                TimedReload();
            }

            //Check to see if the player is able to shoot
            if (CanShoot())
            {
                //If the gun has autofire, allow bullets to be shot without reclicking
                if (currentGun.autoFire && Input.GetButton("Fire1"))
                {
                    Shoot();
                }
                //If the gun doesn't have autoFire, require a new click for each bullet
                else if (!currentGun.autoFire && Input.GetButtonDown("Fire1"))
                {
                    Shoot();
                }
            }
            else if (Input.GetButtonDown("Fire1") && bulletsRemaining == 0 && !isReloading)
            {
                AudioManager.Instance.PlaySFX("EmptyClip");
            }

            if (!isReloading)
            {
                if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetMouseButtonDown(1)) && grenades > 0)
                {
                    ThrowGrenade();
                }
                if ((Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.LeftShift)) && mines > 0) {
                    PlaceMine();
                }
                if (Input.GetKeyDown(KeyCode.R) && canReload)
                {
                    StartCoroutine(TimedReload());
                }
            }

            //For switching guns
            if (Guns.Count > 1 && !isReloading) { 
                if (Input.GetKeyDown(KeyCode.E) || (Input.mouseScrollDelta.y > 0)) {
                    NextGun();
                }
                if (Input.GetKeyDown(KeyCode.Q) || (Input.mouseScrollDelta.y < 0))
                {
                    PreviousGun();
                }
            }
        }



    }

    private bool CanReload() {
        if (currentGun.currentBullets == maxBullets)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void Shoot()
    {
        AudioManager.Instance.PlaySFX(currentGun.name);
        nextFire = Time.time + fireRate;

        //Spawn in the new bullet
        GameObject newBulletObj = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        newBulletObj.transform.parent = projectileContainer.transform;
        
        //Modify the properties of the bullet
        Bullet newBullet = newBulletObj.GetComponent<Bullet>();
        newBullet.updateDamage(gunDamage);
        newBullet.updateSpeed(currentGun.bSpeed);

        //Change the direction of the bullet depending on which way the player faces
        if (playerMove.facingRight)
        {
            newBullet.shotRight = true;
        } else
        {
            newBullet.shotRight = false;
        }

        if (currentGun.maxPierce > 0)
        {
            newBullet.EnablePiercing(currentGun.maxPierce);
        }


        //Update the counts of the bullets
        currentGun.currentBullets--;
        bulletsRemaining--;

    }

    private void ThrowGrenade()
    {
        //Spawn a grenade and get a reference to it's script
        GameObject newNadeObj = Instantiate(grenade, grenadePoint.position, grenadePoint.rotation);
        Grenade newGrenade = newNadeObj.GetComponent<Grenade>();
        newNadeObj.transform.parent = projectileContainer.transform;

        //Make the grenade move in a corresponding direction
        if (playerMove.facingRight)
        {
            newGrenade.thrownRight = true;
        }
        else
        {
            newGrenade.thrownRight = false;
        }

        //Update the number of grenades
        grenades -= 1;
        FindObjectOfType<GrenadeUI>().UpdateGrenades(grenades);
    }

    private void PlaceMine()
    {
        GameObject newMine =Instantiate(mine, minePoint.position, minePoint.rotation);
        newMine.transform.parent = projectileContainer.transform;
        mines -= 1;
        FindObjectOfType<LandmineUI>().UpdateMines(mines);
    }

    IEnumerator TimedReload() {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        bulletsRemaining = maxBullets;
        currentGun.currentBullets = maxBullets;
        isReloading = false;
    }

    bool CanShoot() {
        if (bulletsRemaining > 0 && !isReloading) {
            if (Time.time > nextFire) {
                return true;
            }
            else {
                return false;
            }
        } else {
            return false;
                }
    }

    //For Swapping Guns

    private void PreviousGun (){
        Gun tempGun = Guns[Guns.Count - 1];
        for (int i = Guns.Count-1; i >= 0; i--){
            if (i > 0)
            {
                Guns[i] = Guns[i-1];
            }
            else {
                Guns[i] = tempGun;
            }
        }
        ChangeGun(Guns[0]);
    }

    public void NextGun() {
        Gun tempGun = Guns[0];
        for (int i = 0; i < Guns.Count; i++){
            if (i != Guns.Count - 1)
            {
                Guns[i] = Guns[i + 1];
            }
            else {
                Guns[i] = tempGun;
            }
        }
        ChangeGun(Guns[0]);
    }

    private void ChangeGun(Gun newGun) {
        currentGun = newGun;
        FindObjectOfType<GunsUI>().ChangeGunUI(newGun);
        UpdateGun();
    }

    //Change the player's gun stats to match the current gun held
    public void UpdateGun() {
        gunSprite.sprite = currentGun.sprite;
        fireRate = currentGun.fireRate;
        maxBullets = currentGun.mBullets;
        reloadTime = currentGun.rTime;
        gunDamage = currentGun.damage;
        bulletsRemaining = currentGun.currentBullets;

    }

    public void MaxReload() {
        for (int i = 0; i < Guns.Count; i++) {
            Guns[i].currentBullets = Guns[i].mBullets;
        }
    }

    public int GetMines()
    {
        return mines;
    }

    public void AddGun(Gun newGun)
    {
        Guns.Add(newGun);
    }

    public bool CanBuyGrenades()
    {
        if (grenades >= maxGrenades)
        {
            return false;

        }
        else if (grenades < maxGrenades)
        {
            return true;
        }
        return false;
    }


    public bool CanBuyMines()
    {
        if (mines >= maxMines)
        {
            return false;

        }
        else if (mines < maxMines)
        {
            return true;
        }
        return false;
    }

    public void addGrenades()
    {
        grenades++;
    }

    public void addMines()
    {
        mines++;
    }

    public int GetGrenades()
    {
        return grenades;
    }

}
