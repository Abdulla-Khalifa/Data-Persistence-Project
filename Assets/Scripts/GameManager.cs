using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
   public string currentPlayerName;
   public int currentPlayerScore;
   public TextMeshProUGUI textNameInputField;
   public TextMeshProUGUI textHighScore;
   public static string s_highScore;
   public static string s_highScoreName;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        textHighScore.text = $"Best Score:  {s_highScoreName} : {s_highScore}";
    }
    
    public void OnStartClick()
    {
        if(textNameInputField.text == string.Empty)
        return;

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
}
