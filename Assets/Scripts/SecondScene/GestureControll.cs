using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GestureControll : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] GameObject image;
    [SerializeField] Material gestureMaterial;

    [SerializeField] Texture[] images;

    Dictionary<string, Texture> dictionaryImages;

    void Awake()
    {
        dictionaryImages = new Dictionary<string, Texture>();
        CreateDictionary();
        Cube.OnGesturingStart += StartGesturing;
        GestureRecognise.OnGesturingEnd += EndGesturing;
    }

    public void StartGesturing(Vector3 pos, Vector3 where, string name, int ID)
    {
        transform.position = pos;
        transform.LookAt(where);
        player.GetComponent<PlayerMovement>().StopMove();
        gestureMaterial.SetTexture("_MainTex", dictionaryImages[name]);
        image.SetActive(true);
        this.GetComponent<GestureRecognise>().enabled = true;
        GestureRecognise.SetGesture(name);
        GestureRecognise.SetID(ID);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void EndGesturing(int ID)
    {
        transform.localRotation = Quaternion.identity;
        transform.localPosition = new Vector3(0.0f, 1.0f, -5.0f);
        player.GetComponent<PlayerMovement>().StartMove();
        this.GetComponent<GestureRecognise>().enabled = false;
        image.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    void CreateDictionary()
    {
        foreach(var image in images)
        {
            dictionaryImages[image.name] = image;
        }
    }
}
