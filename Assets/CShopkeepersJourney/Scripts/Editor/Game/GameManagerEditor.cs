using UnityEngine;
using UnityEditor;

namespace com.vollmergames
{
    [CustomEditor(typeof(GameManager))]
    public class GameManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            GameManager gameManager = (GameManager)target;

            GUILayout.Space(10);

            // Start Game button
            if (GUILayout.Button("Start Game"))
            {
                gameManager.StartGame();
            }

            // End Game button
            if (GUILayout.Button("End Game"))
            {
                gameManager.EndGame();
            }

            // Abort Game button
            if (GUILayout.Button("Abort Game"))
            {
                gameManager.AbortGame();
            }

            GUILayout.Space(10);          
     
            // Display the default inspector
            DrawDefaultInspector();
        }
    }
}
