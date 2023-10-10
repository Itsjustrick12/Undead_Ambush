using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveManager
{
    public static string directory = "/SaveData/";
    public static string fileName = "PlayerData.txt";

    public static void Save(PlayerData pD)
    {
        string dir = Application.persistentDataPath + directory;

        //If there isn't a directory for save data yet, make one
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        //Convert the playerData object to Json
        string json = JsonUtility.ToJson(pD);

        //Save the playerData to the computer's local files
        File.WriteAllText(dir + fileName, json);
    }

    public static PlayerData Load()
    {
        string fullPath = Application.persistentDataPath + directory + fileName;

        PlayerData pD = new PlayerData();

        //Check to see there is a existing playerData to load
        if (File.Exists(fullPath))
        {
            //get the json file data if it exists
            string json = File.ReadAllText(fullPath);

            //Store the data from the file
            pD = JsonUtility.FromJson<PlayerData>(json);
        } else
        {
            Debug.Log("There is no save data to load");
        }

        return pD;
    }
}
