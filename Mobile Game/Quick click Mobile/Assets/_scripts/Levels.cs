using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    Button _button;
    GameManager gameManager;
    [Range(1,3)]
    public int level;
    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponent<Button>();
        gameManager = FindObjectOfType<GameManager>();
        _button.onClick.AddListener(SetLevel);
    }
    /// <summary>
    /// Cambiar el nivel de la partida. Easy, Medium, Hard
    /// </summary>
    void SetLevel()
    {
        gameManager.InitGame(level);
    }
}
