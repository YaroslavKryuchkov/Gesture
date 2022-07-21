using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float angleSpeed;

    float tanAngle;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    void FixedUpdate()
    {
        Vector3 moveVector = speed * (transform.InverseTransformVector(transform.forward) * Input.GetAxis("Vertical") 
                                    + transform.InverseTransformVector(transform.right) * Input.GetAxis("Horizontal")) * Time.deltaTime;
        this.transform.Translate(moveVector);

        this.transform.Rotate(transform.up, Input.GetAxis("Mouse X") * angleSpeed, Space.World);
        //this.transform.Rotate(this.transform.right, Input.GetAxis("Mouse Y") * angleSpeed);
    }

    public void StopMove()
    {
        this.enabled = false;
    }

    public void StartMove()
    {
        this.enabled = true;
    }
}
