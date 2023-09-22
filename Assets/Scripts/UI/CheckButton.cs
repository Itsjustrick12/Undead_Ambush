using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CheckButton : MonoBehaviour
{

    public  bool checkVisible = false;
    public Image checkImage;

    // Start is called before the first frame update
    void Start()
    {
        if (checkVisible)
        {
            checkImage.color = new Color(255, 255, 255, 255);
        }
        else
        {
            checkImage.color = new Color(255, 255, 255, 0);
        }
    }

    public void showCheck()
    {
        checkVisible = true;
        checkImage.color = new Color(255, 255, 255, 255);
    }

    public void hideCheck()
    {
        checkVisible = false;
        checkImage.color = new Color(255, 255, 255, 0);
    }

    public void ToggleCheck()
    {
        if (checkVisible)
        {
            checkImage.color = new Color(255, 255, 255, 0);
            checkVisible = false;
        }
        else
        {
            checkImage.color = new Color(255, 255, 255, 255);
            checkVisible = true;
        }
    }
}
