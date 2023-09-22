using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTitleScreen : MonoBehaviour
{
    public void ToTitle()
    {
        SceneManager.LoadScene(0);
    }

}
