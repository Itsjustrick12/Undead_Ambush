using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    public bool visible = false;

    public void ToggleSettings()
    {
        if (visible == false)
        {
            gameObject.SetActive(true);
            visible = true;
        }
        else
        {
            gameObject.SetActive(false);
            visible = false;
        }
    }
}
