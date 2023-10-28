using UnityEditor;
using UnityEngine;

namespace com.vollmergames
{
    [CustomEditor(typeof(HandGrabbable))]
    public class HandGrabbableEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            HandGrabbable handGrabbable = (HandGrabbable)target;

            DrawDefaultInspector();

            if (GUILayout.Button("Throw Item at Default Target"))
            {
                HandTrackingAimAssist aimAssist = HandTrackingAimAssist.Instance;

                if (aimAssist != null)
                {
                    Rigidbody itemRigidbody = handGrabbable.GetComponent<Rigidbody>();
                    if (itemRigidbody != null)
                    {
                        aimAssist.ThrowItemAtDefaultTarget(itemRigidbody);
                    }
                    else
                    {
                        Debug.LogWarning("No Rigidbody found on the HandGrabbable item. Make sure the item has a Rigidbody component.");
                    }
                }
                else
                {
                    Debug.LogWarning("HandTrackingAimAssist component not found. Please make sure it's in the scene.");
                }
            }
        }
    }

}
