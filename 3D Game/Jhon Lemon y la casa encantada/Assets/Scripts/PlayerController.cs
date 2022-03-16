using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float horizontal, vertical;
    public  float turnSpeed;
    bool hasHorizontal, hasVertical, walk;
    Vector3 movement;
    Quaternion rotation = Quaternion.identity;

    Animator _animator;
    Rigidbody _rigidbody;
    string isWalking = "isWalking";
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        movement.Set(horizontal, 0, vertical);
        movement.Normalize();

        hasHorizontal = !Mathf.Approximately(horizontal,0f);
        hasVertical = !Mathf.Approximately(vertical, 0f);
        walk = hasHorizontal || hasVertical;
        _animator.SetBool(isWalking,walk);

        Vector3 desireForward = Vector3.RotateTowards(
            transform.forward, movement, turnSpeed * Time.deltaTime,0f);

        rotation = Quaternion.LookRotation(desireForward);
    }

    private void OnAnimatorMove()
    {
        //Espacio = Espacio inicial + velocidad * tiempo (S = So + V * T)
        _rigidbody.MovePosition(_rigidbody.position + movement * _animator.deltaPosition.magnitude);
        _rigidbody.MoveRotation(rotation);
    }
}
