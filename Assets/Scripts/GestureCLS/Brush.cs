using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Brush : MonoBehaviour
{
    LineRenderer lr;

    Vector3 lastMousePos;
    Vector3 currMousePos;

    public GameObject nameTextObject;

    public GameObject canvas;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    Vector3 GetMouseCoordinates()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
    }

    public void CreateRequest()
    {
        canvas.SetActive(false);
        StartCoroutine(Draw());
    }

    IEnumerator Draw()
    {
        while (!Input.GetMouseButtonUp(0))
        {
            yield return null;
        }
        yield return null;
        while (true)
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
                canvas.SetActive(true);
                yield break;
            }
            yield return null;
        }
    }

    public void Save()
    {
        if (lr.positionCount == 0)
        {
            Debug.Log("You have no gesture");
            return;
        }
        Vector3[] points = new Vector3[lr.positionCount];
        lr.GetPositions(points);
        string gestureName = nameTextObject.GetComponent<Text>().text;
        if (gestureName == "")
        {
            Debug.Log("Name of gesture undifiend");
            return;
        }
        points = PennyPitcher.Resample(points);
        lr.positionCount = points.Length;
        lr.SetPositions(points);
        GestureSaveSystem.Save(points, gestureName);
        string imagePath = Application.dataPath + "/Gestures/" + gestureName + ".png";
        StartCoroutine(CaptureScreen(imagePath));
    }

    IEnumerator CaptureScreen(string imagePath)
    {
        yield return null;
        canvas.SetActive(false);

        yield return new WaitForEndOfFrame();
        ScreenCapture.CaptureScreenshot(imagePath);
        canvas.SetActive(true);
    }

    public void Load()
    {
        string gestureName = nameTextObject.GetComponent<Text>().text;
        if (gestureName == "")
        {
            Debug.Log("Name of gesture undifiend");
            return;
        }
        Vector3[] points = GestureSaveSystem.Load(gestureName);
        if (points == null) return;
        lr.positionCount = points.Length;
        lr.SetPositions(points);
    }
}
