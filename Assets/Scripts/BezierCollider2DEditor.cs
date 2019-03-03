#if UNITY_EDITOR 
using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(BezierCollider2D))]
public class BezierCollider2DEditor : Editor
{
    BezierCollider2D bezierCollider;
    EdgeCollider2D edgeCollider;
    LineRenderer line;

    int lastPointsQuantity = 0;

    Vector2 lastFirstPoint = Vector2.zero;
    Vector2 lastHandlerFirstPoint = Vector2.zero;
    Vector2 lastSecondPoint = Vector2.zero;
    Vector2 lastHandlerSecondPoint = Vector2.zero;

    public override void OnInspectorGUI()
    {
        bezierCollider = (BezierCollider2D)target;

        edgeCollider = bezierCollider.GetComponent<EdgeCollider2D>();
        line = bezierCollider.GetComponent<LineRenderer>();

        if (edgeCollider != null)
        {
            bezierCollider.pointsQuantity = EditorGUILayout.IntField("curve points", bezierCollider.pointsQuantity, GUILayout.MinWidth(100));
            bezierCollider.firstPoint = EditorGUILayout.Vector2Field("first point", bezierCollider.firstPoint, GUILayout.MinWidth(100));
            bezierCollider.handlerFirstPoint = EditorGUILayout.Vector2Field("handler first Point", bezierCollider.handlerFirstPoint, GUILayout.MinWidth(100));
            bezierCollider.secondPoint = EditorGUILayout.Vector2Field("second point", bezierCollider.secondPoint, GUILayout.MinWidth(100));
            bezierCollider.handlerSecondPoint = EditorGUILayout.Vector2Field("handler secondPoint", bezierCollider.handlerSecondPoint, GUILayout.MinWidth(100));

            EditorUtility.SetDirty(bezierCollider);

            if (bezierCollider.pointsQuantity > 0 && !bezierCollider.firstPoint.Equals(bezierCollider.secondPoint) &&
                (
                    lastPointsQuantity != bezierCollider.pointsQuantity ||
                    lastFirstPoint != bezierCollider.firstPoint ||
                    lastHandlerFirstPoint != bezierCollider.handlerFirstPoint ||
                    lastSecondPoint != bezierCollider.secondPoint ||
                    lastHandlerSecondPoint != bezierCollider.handlerSecondPoint
                ))
            {
                edgeCollider.points = bezierCollider.calculate2DPoints();
                edgeCollider.edgeRadius = Constants.LINE_WIDTH / 2;

                line.positionCount = edgeCollider.pointCount;
                line.SetPositions(bezierCollider.calculate3DPoints());
                line.material = new Material(Shader.Find("Unlit/Color")) { color = Constants.LINE_COLOR };
                line.startWidth = line.endWidth = Constants.LINE_WIDTH;
            }
        }
    }
}
#endif