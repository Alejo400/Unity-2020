using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f, timer, displayImage = 2f;

    public GameObject Player;
    bool isPlayerHere, isPlayerCaught;

    public CanvasGroup exitImageBackground, caughtImageBackground;

    // Update is called once per frame
    void Update()
    {
        if (isPlayerHere)
        {
            EndGame(exitImageBackground, false);
        }else if (isPlayerCaught)
        {
            EndGame(caughtImageBackground, true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            isPlayerHere = true;
        }
    }

    /// <summary>
    /// Mostrar la imagen de victoria con efecto de desvanecimiento
    /// </summary>
    /// <param name="image">Imagen a mostrar, dependiendo de si hemos ganado o nos han atrapado</param>
    void EndGame(CanvasGroup image, bool doReset)
    {
        timer += Time.deltaTime;
        image.alpha = timer / fadeDuration;

        if (timer > fadeDuration + displayImage)
        {
            if (doReset)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                Application.Quit();
            }
        }
    }
    /// <summary>
    /// Indicamos si el player ha sido o no atrapado
    /// </summary>
    public void catchPlayer() { 
        isPlayerCaught = true; 
    }
}
