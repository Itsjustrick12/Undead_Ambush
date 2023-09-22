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
        FindObjectOfType<GameManager>().ToggleFullscreen();
    }

    public void UpdateFullscreenCheck()
    {
        CheckButton cB = GetComponent<CheckButton>();
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
