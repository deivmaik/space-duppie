using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu, 
    inGame,
    gameOver
}

public class gameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.menu;

    public static gameManager sharedInstance;

    private playerController controller;

    void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<playerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit") && currentGameState != GameState.inGame)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        SetGameState(GameState.inGame);
    }

    public void gameOver()
    {
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    private void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.menu){
            //TODO add menu's logic

        } else if(newGameState == GameState.inGame){
            controller.StartGame();
        } else if(newGameState == GameState.gameOver){
            //TODO prepare game to end
        }

        this.currentGameState = newGameState;
    }
}
