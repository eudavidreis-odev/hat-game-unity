using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public int score;
    [HideInInspector] public int highScore;

    
    [SerializeField] private float initialTime;
    [HideInInspector] public float currentTime;

    public bool gameStarted,gamePaused,gameOver;

    public Transform player;

    private Vector2 playerPosition;

    UIController uIController;

    [SerializeField] private SpawnController spawnController;

    // Start is called before the first frame update
    void Start()
    {
        uIController = FindObjectOfType<UIController>();
        spawnController = FindObjectOfType<SpawnController>();

        gameStarted = false;
        gamePaused = false;
        highScore = GetScore();
        SetTime(initialTime);
        playerPosition = player.position;

    }

    // Update is called once per frame

    public void InvokeCountDownTime(){
        InvokeRepeating("CountDownTime",1f,1f);
    }
    public void CountDownTime(){
        if(currentTime>0f && gameStarted && !gamePaused){
            currentTime -= 1f;
            SetTime(currentTime);
        }else if(currentTime == 0 && gameStarted){
            GameOver();
            CancelInvoke("CountDownTime");
            return;
        }else{
            return;
        }
    }
    public void StartGame(){
        score = 0;
        currentTime = initialTime;
        gameStarted = true;
        player.position = playerPosition;
        InvokeCountDownTime();
    }
    public void BackToMainMenu(){
        score = 0;
        currentTime = 0f;
        gameStarted = false;
        gameOver = false;
        gamePaused = false;
        player.position = playerPosition;
        highScore = GetScore();
        CancelInvoke("CountDownTime");
    }
    public void PauseGame(){
        gamePaused = true;
        gameStarted = false;
        CancelInvoke("CountDownTime");
    }
    public void ResumeGame(){
        gamePaused = false;
        gameStarted = true;
        InvokeCountDownTime();
    }
    public void GameOver(){
            uIController.GameOver();

            gameStarted = false;
            gamePaused = false;
            gameOver = true;
            SaveScore();

            currentTime = 0f;
            CancelInvoke("CountDownTime");
    }
    public void RestartGame(){
        gameOver = false;
        gamePaused = false;
        gameStarted = true;
        currentTime = initialTime;
        score = 0;
        SetTime(currentTime);
        InvokeCountDownTime();

    }
    public void SaveScore(){
        if(score > highScore){
            highScore = score;    
            PlayerPrefs.SetInt("highscore",highScore);
        }
    }
    public int GetScore(){
        int highScore = PlayerPrefs.GetInt("highscore",0);
        return highScore;
    }
    public void SetTime(float time){
        uIController.txtTime.text = time.ToString();
    }

    public void DestroyAllBalls(){
        foreach (Transform child in spawnController.allBallsParent)
        {
            Destroy(child.gameObject);
        }
    }

}
