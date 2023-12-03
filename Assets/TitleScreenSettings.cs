using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenSettings : MonoBehaviour
{
    PlayerData playerData;
    AudioManager audio;
    // Start is called before the first frame update
    void Start()
    {
        playerData = SaveManager.Load();
        audio = FindObjectOfType<AudioManager>();
        if (playerData.musicOn)
        {
            audio.unMuteMusic();
        }
        else
        {
            audio.muteMusic();
        }
    }
}
