using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    private SaveData highScoreData;
    public string highScorePlayer;
    public int highScore;
    public string playerName;
    public int playerScore=0;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public string playerSaveName;
        public int playerSaveScore;
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.playerSaveName = Instance.playerName;
        data.playerSaveScore = Instance.playerScore;

        string json = JsonUtility.ToJson(data);

        if(playerName!="")File.WriteAllText(Application.persistentDataPath+"/"+playerName+".json",json);

        //Check for new highscore
        LoadHighScore();
        if (highScoreData == null)
        {
            File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
        }
        else
        {
            if (highScoreData.playerSaveScore < Instance.playerScore)
            {
                File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);

            }
        }
    }

    public void Load()
    {
        
        string path = Application.persistentDataPath + "/" + playerName + ".json";
        Debug.Log(path);

        if (playerName == "") Debug.Log("Please enter a name!");
        else if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerSaveName;
            playerScore = data.playerSaveScore;
        }
        
        
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/highscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            highScoreData = JsonUtility.FromJson<SaveData>(json);
            highScorePlayer = highScoreData.playerSaveName;
            highScore = highScoreData.playerSaveScore;
        }
    }
}
