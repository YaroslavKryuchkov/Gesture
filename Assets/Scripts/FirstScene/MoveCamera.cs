using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField]GameObject sphere;

    void Update()
    {
        this.transform.position = sphere.transform.position + new Vector3(0.0f, 0.5f, -10.0f);
    }
}
