using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureMove : MonoBehaviour
{
    [SerializeField] GameObject camera;

    void Update()
    {
        this.transform.position = -0.1f * camera.transform.position;
    }
}
