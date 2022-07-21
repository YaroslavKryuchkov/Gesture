using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class GestureSaveSystem
{
    public static void Save(Vector3[] points, string name)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.dataPath + "/Gestures/" + name + ".gesture";
        FileStream fileStream = new FileStream(path, FileMode.Create);

        GestureData data = new GestureData(points);

        formatter.Serialize(fileStream, data);

        fileStream.Close();
    }

    public static Vector3[] Load(string name)
    {
        string path = Application.dataPath + "/Gestures/" + name + ".gesture";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            GestureData data = formatter.Deserialize(fileStream) as GestureData;

            fileStream.Close();

            Vector3[] points = new Vector3[data.n];
            points[0].x = data.start[0];
            points[0].y = data.start[1];
            points[0].z = 0.0f;
            for (int i = 1; i < data.n; i++)
            {
                points[i].x = points[i - 1].x + data.points[i - 1, 0];
                points[i].y = points[i - 1].y + data.points[i - 1, 1];
                points[i].z = 0.0f;
            }

            return points;
        }
        Debug.Log("Can not find gesture at " + path);
        return null;
    }

    public static GestureData LoadRaw(string name)
    {
        string path = Application.dataPath + "/Gestures/" + name + ".gesture";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);

            GestureData data = formatter.Deserialize(fileStream) as GestureData;

            fileStream.Close();

            return data;
        }
        Debug.Log("Can not find gesture at " + path);
        return null;
    }
}
