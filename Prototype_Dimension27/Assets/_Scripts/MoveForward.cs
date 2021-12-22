using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float velocity = 4, limitX = 25, limitZ = 12;
    // Start is called before the first frame update
    private void Update()
    {
        Vector3 moveTorward = new Vector3(0,0,1.5f) * velocity * Time.deltaTime;
        transform.Translate(moveTorward);

        DestroyFromPosition();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemie"))
        {
            Destroy(gameObject);
        }
    }
    void DestroyFromPosition()
    {
        
        if(Math.Abs(transform.position.x) > limitX || Math.Abs(transform.position.z) > limitZ)
        {
            Destroy(gameObject);
        }
    }
}
