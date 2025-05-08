using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.IO;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;

   public string currentPlayerName;
   public int currentPlayerScore;

   public string jsonPlayerName = string.Empty;
   public int jsonPlayerScore;
   public string loadedName;

   public TextMeshProUGUI textNameInputField;
   public TextMeshProUGUI textHighScore;
   public static string s_highScore;
   public static string s_highScoreName;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            LoadData();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        textHighScore.text = jsonPlayerName != string.Empty ?  $"Best Score:  {jsonPlayerName} : {jsonPlayerScore}" : "";
    }
    
    public void OnStartClick()
    {
        if(textNameInputField.text == string.Empty)
        return;

        loadedName = jsonPlayerName;
        currentPlayerName = textNameInputField.text;
        SceneManager.LoadScene(1);
    }

    public void OnQuitClick()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }

    [System.Serializable]
    public class SaveData
    {
        public int HighScore;
        public string PlayerName;
    }

    public void SaveScore()
        {
            SaveData data = new SaveData();
            data.HighScore = currentPlayerScore;
            data.PlayerName = currentPlayerName;

            string json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

            print("game saved successfully");

        }

        public void LoadData()
        {
            string path = Application.persistentDataPath + "/savefile.json";

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);

                SaveData data = JsonUtility.FromJson<SaveData>(json);

                jsonPlayerScore = data.HighScore;
                jsonPlayerName = data.PlayerName;

                print("game loaded successfully, " + jsonPlayerName + " ," + jsonPlayerScore);
            }
        }
}
