using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenSettings : MonoBehaviour
{
    PlayerData playerData;
    AudioManager audioM;
    // Start is called before the first frame update
    void Start()
    {
        playerData = SaveManager.Load();
        audioM = FindObjectOfType<AudioManager>();
        if (playerData.musicOn)
        {
            audioM.unMuteMusic();
        }
        else
        {
            audioM.muteMusic();
        }
    }
}
