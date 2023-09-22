using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    private Shooting playerShooting;
    public List<Gun> GunsList;

    public int minesPrice = 25;
    public int grenadesPrice = 25;

    

    private void Start()
    {
        playerShooting = FindObjectOfType<Shooting>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
