using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour
{
    public float timeToGenerateItem, generateItemCounter,
    timeToGenerateEnemies, generateEnemieCounter;

    public List<Items> items = new List<Items>();
    public Transform startPosition, endPosition;
    Vector3 initPositionItems;

    public bool initGame = false;
    public static ItemsManager itemsManager;
    private void Awake()
    {
        if (itemsManager == null) itemsManager = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        generateItemCounter = timeToGenerateItem;
        generateEnemieCounter = timeToGenerateEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        if (initGame)
        {
            generateItems(0, 5);
            generateEnemies(6, 9);
        }
    }
    /// <summary>
    /// Generar los objetos en posiciones aleatorias
    /// </summary>
    /// <param name="init">indicador minimo del item a generar con Randon.Range</param>
    /// <param name="end">indicador maximo del item a generar con Randon.Range</param>
    void generateItems(int init,int end)
    {
        int randomItems = Random.Range(init,end);
        initPositionItems = new Vector3(
                                        Random.Range(startPosition.position.x, endPosition.position.x),
                                        Random.Range(startPosition.position.y, endPosition.position.y),
                                        0
                                        );
        generateItemCounter -= Time.deltaTime;
        if (generateItemCounter <= 0)
        {
            Items generateItems = Instantiate(items[randomItems]);
            generateItems.transform.SetParent(transform, false);
            generateItems.transform.position = initPositionItems;

            generateItemCounter = timeToGenerateItem;
        }
    }
    /// <summary>
    /// Generar los enemigos en posiciones aleatorias
    /// </summary>
    /// <param name="init">indicador minimo del item a generar con Randon.Range</param>
    /// <param name="end">indicador maximo del item a generar con Randon.Range</param>
    void generateEnemies(int init, int end)
    {
        int randomItems = Random.Range(init, end);
        initPositionItems = new Vector3(
                                        Random.Range(startPosition.position.x, endPosition.position.x),
                                        Random.Range(startPosition.position.y, endPosition.position.y),
                                        0
                                        );
        generateEnemieCounter -= Time.deltaTime;
        if (generateEnemieCounter <= 0)
        {
            Items generateItems = Instantiate(items[randomItems]);
            generateItems.transform.SetParent(transform, false);
            generateItems.transform.position = initPositionItems;

            generateEnemieCounter = timeToGenerateEnemies;
        }
    }
    /// <summary>
    /// Destruir todos los objetos
    /// </summary>
    public void deleteAllItems()
    {
        Items[] items = FindObjectsOfType<Items>();
        foreach(Items deleteItems in items)
        {
            Destroy(deleteItems.gameObject);
        }
    }
}
