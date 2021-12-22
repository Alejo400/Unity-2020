using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StateGame
{
    startGame,
    inGame,
    endGame
}

public class GameManager : MonoBehaviour
{
    public StateGame currentStateGame = StateGame.startGame;
    public static GameManager gameManager;
    ItemsManager itemsManager;
    public AudioSource intro;
    public AudioSource end;
    // Start is called before the first frame update
    private void Awake()
    {
        if (gameManager == null) gameManager = this;
    }
    void Start()
    {
        GameMenu();
        itemsManager = GameObject.Find("ItemsManager").GetComponent<ItemsManager>();
    }
    /// <summary>
    /// Enumerador StateGame cambia al menu del juego
    /// </summary>
    public void GameMenu() { setCurrentGameState(StateGame.startGame); }
    /// <summary>
    /// Enumerador StateGame cambia a jugador en el juego
    /// </summary>
    public void InGame() { setCurrentGameState(StateGame.inGame); }
    /// <summary>
    /// Enumerador StateGame cambia a GameOver o final del juego
    /// </summary>
    public void GameOver() { setCurrentGameState(StateGame.endGame); }
    /// <summary>
    /// Muestra y oculta menus, crea los bloques de niveles y reproduce sonidos
    /// </summary>
    /// <param name="SG">Identifica si el juego esta en estado Menu, Playing o GameOver</param>
    public void setCurrentGameState(StateGame SG)
    {
        if (SG == StateGame.startGame)
        {
            intro.Play();
            MenuManager.menuManager.ShowStartGame();
            MenuManager.menuManager.HideInGame();
            MenuManager.menuManager.HideEndGame();
        }
        else if (SG == StateGame.inGame)
        {
            intro.Stop();
            itemsManager.initGame = true;

            MenuManager.menuManager.HideStartGame();
            MenuManager.menuManager.HideEndGame();
            MenuManager.menuManager.ShowInGame();
        }
        else if (SG == StateGame.endGame)
        {
            end.Play();
            itemsManager.initGame = false;

            MenuManager.menuManager.HideStartGame();
            MenuManager.menuManager.HideInGame();
            MenuManager.menuManager.ShowEndGame();
        }

        this.currentStateGame = SG;
    }
}
