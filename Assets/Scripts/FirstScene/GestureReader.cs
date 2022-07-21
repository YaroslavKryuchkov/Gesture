using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GestureReader : MonoBehaviour
{
    LineRenderer lr;

    Vector3 lastMousePos;
    Vector3 currMousePos;

    [SerializeField] GameObject gestureCamera;
    Camera gestureCameraComponent;

    float max;
    string winner;

    public static event Action OnJump;
    public static event Action OnRight;
    public static event Action OnLeft;

    [SerializeField] string[] gestureNames;
    [Range(0.0f, 0.95f)]
    [SerializeField] float percent;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        gestureCameraComponent = gestureCamera.GetComponent<Camera>();
    }

    Vector3 GetMouseCoordinates()
    {
        return gestureCameraComponent.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = GetMouseCoordinates();

            lr.positionCount = 1;
            lr.SetPosition(lr.positionCount - 1, lastMousePos);
        }

        if (Input.GetMouseButton(0))
        {
            currMousePos = GetMouseCoordinates();
            if (Vector3.Distance(currMousePos, lastMousePos) > 0.05f)
            {
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, currMousePos);

                lastMousePos = currMousePos;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            max = 0.0f;
            winner = "";
            Vector3[] points = new Vector3[lr.positionCount];
            lr.GetPositions(points);
            points = PennyPitcher.ResampleAnyway(points);
            GestureData gesture = new GestureData(points);
            foreach (var name in gestureNames)
            {
                float percentRelative = PennyPitcher.Compare(gesture, GestureSaveSystem.LoadRaw(name));
                if (percentRelative > max && percentRelative > 31.0f * percent)
                {
                    max = percentRelative;
                    winner = name;
                }
            }
            Deside(winner);
            lr.positionCount = 0;
        }
    }

    void Deside(string winner)
    {
        switch(winner)
        {
        case "Jump":
                OnJump?.Invoke();
                break;
        case "Right":
                OnRight?.Invoke(); 
                break;
        case "Left":
                OnLeft?.Invoke(); 
                break;
        }
    }
}
