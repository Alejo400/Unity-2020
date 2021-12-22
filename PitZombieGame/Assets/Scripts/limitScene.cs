using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Esta clase es usada para repeler los objetos, una vez han tocado el limite o pared en la escena.
/// La pared es un collider. El limite, son los transform de objetos vacios
/// </summary>
public class limitScene : MonoBehaviour
{
    Items items;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(name == "LeftLimit" || name == "RightLimit")
        {
            items = collision.GetComponent<Items>();
            items.speedX *= -1;
        }
        else if(name == "DestroyZone")
        {
            Destroy(collision.gameObject);
        }
    }
}
