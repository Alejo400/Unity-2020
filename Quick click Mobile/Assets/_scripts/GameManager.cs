using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targetPrefabs;
    public float spawnRate;
    int sizePrefabs = 100;
    public TextMeshProUGUI scoreText, gameOver;
    int score = 0;
    float timeToSleep = 5;
    public Button restartButton;
    public enum GameState{
        menuGame,
        inGame,
        gameOver
    }
    public GameState _gameState;
    public GameObject titleGame;
    private void Start()
    {
        updateScore();
    }
    // Start is called before the first frame update
    /// <summary>
    /// Iniciar el juego
    /// </summary>
    /// <param name="changeLevel">Indica el nivel o dificultad del juego</param>
    public void InitGame(int changeLevel)
    {
        scoreText.text = "PUNTOS: " + score;
        titleGame.gameObject.SetActive(false);
        _gameState = GameState.inGame;
        StartCoroutine(generatePrefabs());
        spawnRate /= changeLevel;
    }
    /// <summary>
    /// Generar objetos prefabs de forma aleatoria, tras la espera en segundos de spawnRate
    /// </summary>
    /// <returns></returns>
    IEnumerator generatePrefabs()
    {
        for (int i = 0; i < sizePrefabs; i++)
        {
            if (_gameState == GameState.gameOver) break;

            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);
            Instantiate(targetPrefabs[index]);
        }
    }
    /// <summary>
    /// Función para agregar los puntos, una vez el jugador destruye las comidas
    /// </summary>
    /// <param name="newScore">Nuevos puntos a sumar al score</param>
    public void addScore(int newScore)
    {
        score += newScore;
        if (score < 0)
        {
            score = 0;
            GameOver();
        }
        scoreText.text = "PUNTOS: " + score;
    }
    /// <summary>
    /// Devuelve el valor guardado en el PREF MAX_SCORE
    /// </summary>
    /// <returns></returns>
    private int GetMaxScore() => PlayerPrefs.GetInt("MAX_SCORE", 0);
    void updateScore()
    {
        int max_score = GetMaxScore();
        scoreText.text = "PUNTOS: " + max_score;
    }
    void setMaxScore()
    {
        int max_score = GetMaxScore();

        if (score > max_score) PlayerPrefs.SetInt("MAX_SCORE",score);
    }
    /// <summary>
    /// Detiene el tiempo a la mitad, si se ha dado clic en un objeto marcado con tag Special1
    /// </summary>
    /// <returns></returns>
    public IEnumerator slowTime()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(timeToSleep);
        Time.timeScale = 1;
    }
    /// <summary>
    /// Cambiar a estado de Game Over y mostrar la UI
    /// </summary>
    public void GameOver()
    {
        setMaxScore();
        _gameState = GameState.gameOver;
        gameOver.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
    /// <summary>
    /// Reiniciar la escena que se este usando durante el juego
    /// </summary>
    public void restartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
