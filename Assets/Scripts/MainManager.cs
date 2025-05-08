using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    private GameManager gameManager;
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text highScoreWithNameText;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;
    
    void Start()
    {        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.LoadData();
        highScoreWithNameText.text = $"High Score: {gameManager.jsonPlayerName} : {gameManager.jsonPlayerScore}";
        //gameManager.jsonPlayerName != string.Empty ? $"High Score: {gameManager.jsonPlayerName} : {gameManager.jsonPlayerScore}" : $"High Score: {gameManager.currentPlayerName} : {gameManager.currentPlayerScore}";
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        //gameManager.currentPlayerScore =  m_Points > gameManager.currentPlayerScore ? m_Points : gameManager.currentPlayerScore;

        if(m_Points > gameManager.jsonPlayerScore)
        {
            gameManager.currentPlayerScore = m_Points;
            highScoreWithNameText.text =  $"High Score: {gameManager.currentPlayerName} : {m_Points}";

            print("New High Score!");
            gameManager.SaveScore();
        }
        else
        {
            print("No new High Score!");

        }
    }
}
