using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resolutionChanger : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SetScreenSmall();
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            SetScreenDefault();
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            SetScreenBig();
        }
    }

    public void SetScreenSmall()
    {
        Screen.SetResolution(512, 288, false);
    }

    public void SetScreenDefault()
    {
        Screen.SetResolution(1024, 576, false);
    }

    public void SetScreenBig()
    {
        Screen.SetResolution(1536, 864, false);
    }
}
