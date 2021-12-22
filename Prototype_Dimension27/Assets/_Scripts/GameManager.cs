using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    float countToChangeDimension, timeToChangeDimension, countToGenerateVortex = 4;

    [Range(-10,10), SerializeField]
    float minPositionX, maxPositionX;

    [Range(-5, 5), SerializeField]
    float minPositionZ, maxPositionZ;

    public bool isFirstDimensionActive = true, isSecondDimensionActive = false;
    float positionX, positionZ;
    Vector3 positionFinal;

    EnemyMovement[] enemyMovement;
    public GameObject vortex;
    // Update is called once per frame
    void Update()
    {
        changeDimensionInScene();
        if (isSecondDimensionActive)
            SpawnVortex();
    }
    /// <summary>
    /// Cambiar de un modo a otro, incluyendo la interaccion de los enemigos en escena
    /// </summary>
    void changeDimensionInScene()
    {
        countToChangeDimension += Time.deltaTime; //contador para cambiar de dimension
        if (countToChangeDimension > timeToChangeDimension)
        {
            enemyMovement = FindObjectsOfType<EnemyMovement>();
            countToChangeDimension = 0f;
            isFirstDimensionActive = !isFirstDimensionActive;
            isSecondDimensionActive = !isSecondDimensionActive;

            foreach (EnemyMovement enemies in enemyMovement)
            {
                //Para todos los enemigos, determinamos cual de las dimensiones esta activa
                //El estado del agente AI dependerá de si la primera dimension esta o no activa
                enemies.GetComponent<NavMeshAgent>().enabled = isFirstDimensionActive;
                enemies.isFirstDimensionActive = isFirstDimensionActive;
                enemies.isSecondDimensionActive = isSecondDimensionActive;
                //detener la velocidad del rigibody para evitar bug
                enemies.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }

        }
    }
    /// <summary>
    /// Generar vortex aleatorios, si se ha cambiado a la segunda dimension
    /// </summary>
    void SpawnVortex()
    {
        positionX = Random.Range(minPositionX,maxPositionX);
        positionZ = Random.Range(minPositionZ,maxPositionZ);
        positionFinal = new Vector3(positionX,0,positionZ);

        if (countToGenerateVortex >= 4)
        {
            countToGenerateVortex = 0;
            Instantiate(vortex,positionFinal,vortex.transform.rotation);
        }
        countToGenerateVortex += Time.deltaTime;
    }

}
