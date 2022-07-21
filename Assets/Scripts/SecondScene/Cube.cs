using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public static event Action<Vector3, Vector3, string, int> OnGesturingStart;
    static int ID = 0;
    int thisID;

    [SerializeField] GameObject pressE;
    [SerializeField] GameObject cameraPos;

    [SerializeField] string gestureName;

    Coroutine cor;

    void Awake()
    {
        GestureRecognise.OnGesturingEnd += MoveUp;
        thisID = ID++;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            pressE.SetActive(true);
            cor = StartCoroutine(Gesturing());
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            pressE.SetActive(false);
            StopCoroutine(cor);
        }
    }

    IEnumerator Gesturing()
    {
        while (Input.GetAxis("Action") == 0)
            yield return null;
        OnGesturingStart?.Invoke(cameraPos.transform.position, transform.position, gestureName, thisID);
        pressE.SetActive(false);
        StopCoroutine(cor);
    }

    void MoveUp(int workID)
    {
        if(workID == thisID)
        {
            transform.position += new Vector3(0.0f, 2.0f, 0.0f);
            this.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
