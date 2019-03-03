using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDesigner : MonoBehaviour
{
    public BorderLineInfo[] borderLines;

    void Start()
    {
        if (UIManager.IsGameplayScene)
            foreach (BorderLineInfo lineInfo in borderLines)
            {
                LineRenderer line = new GameObject(Time.timeSinceLevelLoad.ToString()).AddComponent<LineRenderer>();
                line.startWidth = line.endWidth = Constants.LINE_WIDTH;
                line.material = new Material(Shader.Find("Unlit/Color")) { color = Constants.LINE_COLOR };
                EdgeCollider2D edgeCollider = line.gameObject.AddComponent<EdgeCollider2D>();
                List<Vector2> points2D = new List<Vector2>();
                List<Vector3> points3D = new List<Vector3>();
                foreach (Transform item in lineInfo.checkPoints) { points2D.Add(item.position); points3D.Add(item.position); }
                edgeCollider.points = points2D.ToArray();
                edgeCollider.edgeRadius = Constants.LINE_WIDTH / 2;
                line.positionCount = points3D.Count;
                line.SetPositions(points3D.ToArray());
            }
    }
}
[System.Serializable]
public struct BorderLineInfo
{
    public Transform[] checkPoints;
}
