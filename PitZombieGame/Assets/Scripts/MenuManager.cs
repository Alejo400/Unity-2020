using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Canvas StartGame;
    public Canvas InGame;
    public Canvas EndGame;
    public static MenuManager menuManager;
    // Start is called before the first frame update
    private void Awake()
    {
        if (menuManager == null) menuManager = this;
    }
    //MenuGame
    public void ShowStartGame() { StartGame.enabled = true; }
    public void HideStartGame() { StartGame.enabled = false; }
    public void ShowInGame() { InGame.enabled = true; }
    public void HideInGame() { InGame.enabled = false; }
    public void ShowEndGame() { EndGame.enabled = true; }
    public void HideEndGame() { EndGame.enabled = false; }
}
