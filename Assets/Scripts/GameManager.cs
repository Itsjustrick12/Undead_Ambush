using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerMovement player;
    public int counter = 0;
    public DeathCounter dCounter;

    //Game Over UI
    public GameObject GameOverInterface;
    private GameOverUI GOUI;

    //Victory Screen
    public GameObject VictoryScreen;

    //For Pause UI
    public GameObject PausedInterface;
    public GameObject SettingsInterface;
    public bool gamePaused = false;

    //Shop UI
    public bool endlessMode = false;
    public GameObject shopUI;

    //For Fullscreen
    public bool isFullscreen = true;
    

    private void Start()
    {
        if (!Screen.fullScreen)
        {
            isFullscreen = false;
        }


        PausedInterface.SetActive(true);
        PausedInterface.SetActive(false);
        SettingsInterface.SetActive(true);
        SettingsInterface.SetActive(false);
        shopUI.SetActive(true);
        shopUI.SetActive(false);
        UnPauseTime();
        player = FindObjectOfType<PlayerMovement>();
        GOUI = GameOverInterface.GetComponent<GameOverUI>();
        VictoryScreen.SetActive(false);
        GameOverInterface.SetActive(false);
        PausedInterface.SetActive(false);
        
    }

    private void Update()
    {
        //When the space key is pressed, and the game hasn't ended, pause the game
        if (Input.GetKeyDown(KeyCode.Escape) && !GameOverInterface.activeInHierarchy && !VictoryScreen.activeInHierarchy)
        {
            if (!PausedInterface.activeInHierarchy)
            {
                PausedInterface.SetActive(true);
                shopUI.SetActive(false);
                PauseTime();
            }
            else
            {
                PausedInterface.SetActive(false);
                SettingsInterface.SetActive(false);
                UnPauseTime();
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab) && !GameOverInterface.activeInHierarchy && !VictoryScreen.activeInHierarchy && endlessMode)
        {
            if (!shopUI.activeInHierarchy)
            {
                shopUI.SetActive(true);
                PausedInterface.SetActive(false);
                PauseTime();
            }
            else
            {
                shopUI.SetActive(false);
                PausedInterface.SetActive(false);
                SettingsInterface.SetActive(false);
                UnPauseTime();
            }
        }

            if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFullscreen();
        }
    }

    public void addToCounter()
    {
        counter++;
        dCounter.UpdateText(counter);

    }

    public void GameOver(bool playerDead) {
        if (playerDead)
        {
            AudioManager.Instance.PlaySFX("ZombieBite");
            PauseTime();
            GOUI.Infected();
            GameOverInterface.SetActive(true);
        }
        else {
            PauseTime();
            GOUI.Zombie();
            GameOverInterface.SetActive(true);
        }
    }

    public void Victory()
    {
        PauseTime();
        VictoryScreen.SetActive(true);
    }   

    public void PauseTime()
    {
        Time.timeScale = 0;
        gamePaused = true;
    }

    public void UnPauseTime() {
        Time.timeScale = 1;
        gamePaused = false;
    }

    public void ToggleFullscreen()
    {
        if (isFullscreen)
        {
            Screen.fullScreen = false;
            isFullscreen = false;
        }
        else
        {
            Screen.fullScreen = true;
            isFullscreen = true;
        }
    }
}
