using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTRLSBTN : MonoBehaviour
{
    [SerializeField] GameObject ControlsUI;
    private bool visible = false;
    
    public void ToggleView()
    {
        if (visible)
        {
            visible = false;
            ControlsUI.SetActive(false);
        }
        else
        {
            visible = true;
            ControlsUI.SetActive(true);
        }
    }
}
