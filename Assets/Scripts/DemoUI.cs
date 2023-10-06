using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoUI : MonoBehaviour
{
    public GameObject controlsPanel;
    public bool showing = false;

    public void ToggleControls()
    {
        if (showing)
        {
            controlsPanel.SetActive(false);
            showing = false;
        }
        else
        {
            controlsPanel.SetActive(true);
            showing = true;
        }
    }

}
