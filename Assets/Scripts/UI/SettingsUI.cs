using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    public bool visible = false;

    //Used for updating the settings in the player data
    [SerializeField] private CheckButton musicBTN;
    [SerializeField] private CheckButton sfxBTN;
    [SerializeField] private CheckButton screenShakeBTN;

    [SerializeField] AudioManager audioManager;
    GameManager gameManager;

    private void Start()
    {

        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();

        LoadSettings();
    }

    public void ToggleSettings()
    {
        if (visible == false)
        {
            LoadSettings();
            gameObject.SetActive(true);
            visible = true;
        }
        else
        {
            gameObject.SetActive(false);
            visible = false;
            SaveSettings();
        }
    }

    private void OnDisable()
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        gameManager.UpdateSettingsData(musicBTN.checkVisible, sfxBTN.checkVisible, screenShakeBTN.checkVisible);
    }

    public void LoadSettings()
    {
        PlayerData pd = SaveManager.Load();

        if (pd.musicOn){
            musicBTN.showCheck();
            audioManager.unMuteMusic();
        }
        else
        {
            musicBTN.hideCheck();
            audioManager.muteMusic();
        }

        if (pd.SFXOn)
        {
            sfxBTN.showCheck();
            audioManager.unMuteSFX();
        }
        else
        {
            sfxBTN.hideCheck();
            audioManager.muteSFX();
        }
        if (pd.screenShakeOn)
        {
            screenShakeBTN.showCheck();
            CinemachineShake shake = FindObjectOfType<CinemachineShake>();
            shake.turnOnShake();
        }
        else
        {
            screenShakeBTN.hideCheck();
            CinemachineShake shake = FindObjectOfType<CinemachineShake>();
            shake.turnOffShake();
        }
    }
}
