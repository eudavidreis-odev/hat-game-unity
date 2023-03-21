using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    private GameController gameController;
    public GameObject panelMainMenu;
    public GameObject panelGame;
    public GameObject panelPause;
    public GameObject panelGameOver;

    public TMP_Text txtHigscore;
    public TMP_Text gameScore;
    public TMP_Text txtTime;



    // Start is called before the first frame update
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        SetRecord();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitGame(){
        AndroidJavaObject activity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        activity.Call<bool>("moveTaskToBack",true);
    }

    public void StartGame(){
        panelMainMenu.gameObject.SetActive(false);
        panelGame.gameObject.SetActive(true);
        gameController.StartGame();
        PauseTime(false);

    }

    public void PauseGame(){
        PauseTime(true);
        panelGame.gameObject.SetActive(false);
        panelPause.gameObject.SetActive(true);
        gameController.PauseGame();
    }
    
    public void ResumeGame(){
        panelPause.gameObject.SetActive(false);
        panelGame.gameObject.SetActive(true);
        gameController.ResumeGame();
        PauseTime(false);

    }

    public void GameOver(){
        PauseTime(true);
        panelGame.gameObject.SetActive(false);
        panelGameOver.gameObject.SetActive(true);
    }

    public void BackMainMenu(){
        gameController.DestroyAllBalls();
        PauseTime(false);
        panelPause.gameObject.SetActive(false);
        panelGameOver.gameObject.SetActive(false);
        panelGame.gameObject.SetActive(false);
        panelMainMenu.gameObject.SetActive(true);
        SetRecord();
        gameController.BackToMainMenu();
    }

    public void GameRestart(){
        PauseTime(false);
        panelGame.gameObject.SetActive(false);
        panelGameOver.gameObject.SetActive(false);
        panelGame.gameObject.SetActive(true);
        gameController.DestroyAllBalls();
        gameController.RestartGame();

    }

    public void SetRecord(){
        txtHigscore.text ="RECORDE: " + gameController.GetScore().ToString();
    }

    public void IncreaseScore(){
        gameScore.text = gameController.score.ToString();
    }

    public void PauseTime(bool active){
        if(active)Time.timeScale = 0f;
        else Time.timeScale = 1f;        
    }

}
