using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Text uIScore;
    public static UiManager uiManager;
    public int score;
    public AudioSource soundAttack, misc;

    public int countBrains;
    public List<Image> brains = new List<Image>();
    public GameObject bloodOverlay;
    private void Start()
    {
        countBrains = brains.Count;
        countBrains -= 1;
    }
    private void Awake()
    {

        if (uiManager == null)
            uiManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (countBrains < 0)
        {
            GameManager.gameManager.GameOver();
            ItemsManager.itemsManager.deleteAllItems();
            enableBrains();
        }   
    }
    /// <summary>
    /// Agregar puntos al dar clic en los objetos
    /// </summary>
    /// <param name="ptos">puntos de los objetos</param>
    public void addPtos(int ptos)
    {
        misc.Play();
        score += ptos;
        uIScore.text = score.ToString();
    }
    /// <summary>
    /// Ocurre cuando el jugador pierda 1 vida o cerebro
    /// </summary>
    public void deleteBrain()
    {
        brains[countBrains].enabled = false;
        countBrains -= 1;
        soundAttack.Play();
        bloodOverlay.SetActive(true);
        Invoke("desactiveBlood",1);
    }
    /// <summary>
    /// Ocultar la sangre mostrada, al perder una vida
    /// </summary>
    public void desactiveBlood()
    {
        bloodOverlay.SetActive(false);
    }
    /// <summary>
    /// Devolver las vidas y puntaje al jugador 
    /// </summary>
    public void enableBrains()
    {
        foreach(Image brain in brains)
        {
            brain.enabled = true;
            countBrains = brains.Count;
            countBrains -= 1;
            uIScore.text = "0";
            score = 0;
        }
    }
}
