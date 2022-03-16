using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target : MonoBehaviour
{
    float positionY = -4, maxTurn = 8, maxPosition = 4, forceMin = 12, forceMax = 16;
    public int points;
    Rigidbody _rigidbody;
    GameManager _gameManager;
    public ParticleSystem explosion;
    // Start is called before the first frame update

    //ToDo freezeItem, explosiveItem, 
    /* ambos clickeables y aparecen por aleatoriedad repitiendo mas los items de frutas
     * o yield + aleatoriedad para que cada cierto tiempo (asignado) salgan.
     * freezeItem pone lentos los items por ciertos segundo
     * explosiveItem explota todos los items que esten dentro de su collider 
     */
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddForce(forceToUp(), ForceMode.Impulse);
        _rigidbody.AddTorque(forceToTurn(), ForceMode.Impulse);
        transform.position = spawnPosition();
        _gameManager = FindObjectOfType<GameManager>();

    }
    /// <summary>
    /// Impulsar hacia arriba los objetos
    /// </summary>
    /// <returns>direccion hacia arriba * numero aleatorio entre fuerza minima y maxima</returns>
    Vector3 forceToUp () => Vector3.up * Random.Range(forceMin, forceMax);
    /// <summary>
    /// Hacer girar los objetos
    /// </summary>
    /// <returns>Vector3 con las 3 posiciones aleatoriamente, usando -maxTurn y maxTurn</returns>
    Vector3 forceToTurn () => new Vector3(Random.Range(-maxTurn, maxTurn), Random.Range(-maxTurn, maxTurn),
                            Random.Range(-maxTurn, maxTurn));
    /// <summary>
    /// Posicion aleatoria en X de los objetos 
    /// </summary>
    /// <returns>numero aleatorio en X mediante el uso de maxPosition y fijo en Y con positionY</returns>
    Vector3 spawnPosition() => new Vector3(Random.Range(-maxPosition, maxPosition), positionY);

    private void OnMouseDown()
    {
        //OnMouseOver para hacer como Fruit Ninja
        if(_gameManager._gameState != GameManager.GameState.gameOver)
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            _gameManager.addScore(points);

            if (gameObject.CompareTag("Enemie"))
                _gameManager.GameOver();

            if (gameObject.CompareTag("Special1"))
               StartCoroutine(_gameManager.slowTime());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KillZone") 
            && 
            gameObject.CompareTag("Food") 
            && 
            _gameManager._gameState != GameManager.GameState.gameOver)
        {
                _gameManager.addScore(-points);
        }
            

        if (other.CompareTag("KillZone"))
            Destroy(gameObject);
    }
}
