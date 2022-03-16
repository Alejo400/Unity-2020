using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexController : MonoBehaviour
{
    bool activatedByPlayer; //Determina si el player ha tocado o no al vortex
    public Material activeColor; //Color que identifica si el player ha tocado el vortex. 
    [Range(0,5), SerializeField]
    float timeToDestroyVortex;

    float countToDestroyVortex;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            activatedByPlayer = true;

        //El enemigo puede ser destruido si el vortex cambia. El vortex tambien es destruido
        if (activatedByPlayer && other.CompareTag("Enemie"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if (activatedByPlayer)
           GetComponent<MeshRenderer>().material = activeColor;

        DestroyVortexOnTime();
    }
    /// <summary>
    /// Destruir el vortex tras cierto tiempo, especificado en el prefab
    /// </summary>
    void DestroyVortexOnTime()
    {
        countToDestroyVortex += Time.deltaTime;
        if (countToDestroyVortex >= timeToDestroyVortex)
            Destroy(gameObject);
    }
}
