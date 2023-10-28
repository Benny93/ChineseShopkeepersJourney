using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectThrower))]
public class ObjectThrowerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ObjectThrower thrower = (ObjectThrower)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Throw Object"))
        {
            thrower.ThrowObject();
        }
    }
}
