using UnityEngine;
using UnityEditor;

namespace com.vollmergames
{
    [CustomEditor(typeof(Customer))]
    public class CustomerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Customer customer = (Customer)target;

            if (GUILayout.Button("Complete Order"))
            {
                customer.CompleteOrder();
            }
        }
    }
}
