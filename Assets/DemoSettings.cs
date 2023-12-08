using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSettings : MonoBehaviour
{
    public bool visible = false;

    //Used for updating the settings in the player data
    [SerializeField] private CheckButton musicBTN;
    [SerializeField] private CheckButton sfxBTN;
    [SerializeField] private CheckButton screenShakeBTN;
    [SerializeField] AudioManager audioManager;

    private void OnEnable()
    {

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
        PlayerData playerData = SaveManager.Load();
        playerData.musicOn = musicBTN.checkVisible;
        playerData.SFXOn = sfxBTN.checkVisible;
        playerData.screenShakeOn = screenShakeBTN.checkVisible;
        SaveManager.Save(playerData);
    }

    public void LoadSettings()
    {
        PlayerData pd = SaveManager.Load();

        if (pd.musicOn)
        {
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
        }
        else
        {
            screenShakeBTN.hideCheck();
        }
    }
}
