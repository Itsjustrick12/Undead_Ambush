using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerMovement player;
    
    //Game Stats
    public int kills = 0;
    private int waveNumber = 0;

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

    public PlayerData playerData;

    private AudioManager audioManager;

    private void Start()
    {
        playerData = SaveManager.Load();

        audioManager = FindObjectOfType<AudioManager>();

        LoadSettings();

        if (!Screen.fullScreen)
        {
            isFullscreen = false;
        }

        //Toggle all UI elements
        PausedInterface.SetActive(true);
        PausedInterface.SetActive(false);
        SettingsInterface.SetActive(true);
        SettingsInterface.SetActive(false);
        shopUI.SetActive(true);
        shopUI.SetActive(false);


        //Get reference to scripts
        player = FindObjectOfType<PlayerMovement>();
        GOUI = GameOverInterface.GetComponent<GameOverUI>();

        //Hide UI that should not be showing
        VictoryScreen.SetActive(false);
        GameOverInterface.SetActive(false);
        PausedInterface.SetActive(false);
        
        //Ensure that time is resumed when game starts
        UnPauseTime();
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
        kills++;
        dCounter.UpdateText(kills);

    }

    public void updateWave(int wave)
    {
        waveNumber = wave;
    }

    public void GameOver(bool playerDead) {
        //Update the player's save Data
        UpdateSaveData();

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
        //Update the player's save Data
        UpdateSaveData();

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

    private void UpdateSaveData()
    {
        //Only update these stats if playing endless mode
        if (endlessMode)
        {
            //If the player hit a new kill high score, update their save data
            if (kills >= playerData.highestKills)
            {
                playerData.highestKills = kills;
            }

            if (waveNumber >= playerData.highestWave)
            {
                playerData.highestWave = waveNumber;
            }
        }

        //Save the player's current stats
        SaveManager.Save(playerData);
    }

    public void UpdateSettingsData(bool music, bool sfx, bool screenShake)
    {
        playerData.musicOn = music;
        playerData.SFXOn = sfx;
        playerData.screenShakeOn = screenShake;
        SaveManager.Save(playerData);
    }

    public void LoadSettings() { 
        if (playerData.musicOn)
        {
            audioManager.unMuteMusic();
        }
        else
        {
            audioManager.muteMusic();
        }

        if (playerData.SFXOn)
        {
            audioManager.unMuteSFX();
        }
        else
        {
            audioManager.muteSFX();
        }
        if (playerData.screenShakeOn)
        {
            //Turn on ss
            CinemachineShake shake = FindObjectOfType<CinemachineShake>();
            shake.turnOnShake();
        }
        else
        {
            //Turn off ss
            CinemachineShake shake = FindObjectOfType<CinemachineShake>();
            shake.turnOffShake();
        }
    }
}
