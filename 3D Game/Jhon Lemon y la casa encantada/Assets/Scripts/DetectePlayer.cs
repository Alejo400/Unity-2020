using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]

public class DetectePlayer : MonoBehaviour
{
    public Transform Player;
    bool isPlayerInRange;
    public GameEnding gameEnding;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == Player)
            isPlayerInRange = true;
    }

    private void Update()
    {
        if (isPlayerInRange)
        {
            //Un Vector se define con el Final - El origen
            //Sumamos Vector3.up debido a que nuestro player tiene su origen en los pies
            //También podríamos colocar un objeto en el centro del player y usarlo como referencia
            Vector3 direction = Player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit rayCastHit;
            if (Physics.Raycast(ray, out rayCastHit))
            {
                if (rayCastHit.collider.transform == Player)
                {
                    gameEnding.catchPlayer();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == Player)
            isPlayerInRange = false;
    }

}
