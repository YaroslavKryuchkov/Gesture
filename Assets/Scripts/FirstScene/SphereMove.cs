using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMove : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float speed = 10;

    void Awake()
    {
        GestureReader.OnJump += MoveUp;
        GestureReader.OnRight += MoveRight;
        GestureReader.OnLeft += MoveLeft;
        rb = GetComponent<Rigidbody>();
    }

    void MoveRight()
    {
        rb.velocity += new Vector3(1.0f, 0.0f, 0.0f) * speed;
    }

    void MoveLeft()
    {
        rb.velocity += new Vector3(-1.0f, 0.0f, 0.0f) * speed;
    }

    void MoveUp()
    {
        rb.velocity += new Vector3(0.0f, 1.0f, 0.0f) * speed;
    }
}
