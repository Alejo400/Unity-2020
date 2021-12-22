using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed, timeToAttack;

    float horizontal, vertical, countTimeToAttack;
    Vector3 movement, desireToward;
    Quaternion desireRotation;

    Rigidbody _rigibody;
    public GameObject bullet;
    GameManager gameManager;

    bool attack;
    // Start is called before the first frame update
    void Start()
    {
        _rigibody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveAndRotation();
    }
    private void Update()
    {
        if (attack)
        {
            countTimeToAttack += Time.deltaTime;
            if(countTimeToAttack > timeToAttack)
            {
                countTimeToAttack = 0;
                attack = false;
            }
        }
        else
        {
            ShooterAttack();
        }
    }
    /// <summary>
    /// Movimiento y rotacion del jugador
    /// </summary>
    void MoveAndRotation()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        movement.Set(horizontal, 0, vertical);
        movement.Normalize();

        desireToward = Vector3.RotateTowards(transform.forward, movement, speed * Time.deltaTime, 0f);
        desireRotation = Quaternion.LookRotation(desireToward);

        _rigibody.velocity = movement * speed;
        _rigibody.MoveRotation(desireRotation);
    }
    /// <summary>
    /// Disparar en direccion forward
    /// </summary>
    void ShooterAttack()
    {
        if (Input.GetKeyDown("f") && gameManager.isFirstDimensionActive)
        {
            attack = true;
            Instantiate(bullet,
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z),
            desireRotation);
        }
    }
}