using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionController : MonoBehaviour
{
    private void Start()
    {
        UpdateFullscreenCheck();
    }

    private void Update()
    {
        UpdateFullscreenCheck();
    }

    public void ToggleFullScreen()
    {
        GameManager gm = FindObjectOfType<GameManager>();

        if (gm != null)
        {
            gm.ToggleFullscreen();
        }
    }

    public void UpdateFullscreenCheck()
    {
        CheckButton cB = GetComponent<CheckButton>();

        GameManager gm = FindObjectOfType<GameManager>();

        if (gm != null)
        {
            if (FindObjectOfType<GameManager>().isFullscreen)
            {
                cB.showCheck();
            }
            else
            {
                cB.hideCheck();
            }
        }

    }

}
