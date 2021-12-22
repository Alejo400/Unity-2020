using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    Rigidbody2D itemRB;
    public float speedX;
    public float velocity = 1;
    public int ptos = 100;
    // Start is called before the first frame update
    void Start()
    {
        itemRB = GetComponent<Rigidbody2D>();
        speedX = Random.Range(-2,2);
    }

    // Update is called once per frame
    void Update()
    {
        itemRB.velocity = new Vector2(speedX * velocity,-1 * velocity);
    }

    private void OnMouseDown()
    {
        if (gameObject.CompareTag("Player"))
        {
            UiManager.uiManager.addPtos(ptos);
        }else if (gameObject.CompareTag("enemy"))
        {
            UiManager.uiManager.deleteBrain();
        }
        Destroy(gameObject);
    }
}
