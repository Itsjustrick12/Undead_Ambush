using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopEmptyEnabler : MonoBehaviour
{
    [SerializeField] GameObject emptyObj;

    private void Start()
    {
        //If theres nothing to buy, display the message
        if (this.transform.childCount  <= 1)
        {
            emptyObj.SetActive(true);
        }
        else
        {
            emptyObj.SetActive(false);
        }
    }
    // Update is called once per frame
    public void CheckEmptyShop()
    {
        if (this.transform.childCount - 1 == 1)
        {
            emptyObj.SetActive(true);
        }
    }
}
