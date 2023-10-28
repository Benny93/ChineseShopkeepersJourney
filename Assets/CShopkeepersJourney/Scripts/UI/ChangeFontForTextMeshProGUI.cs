using UnityEngine;
using UnityEditor;
using TMPro;
using System.Collections.Generic;

public class ChangeFontForTextMeshProGUI : MonoBehaviour
{
    public TMP_FontAsset newFont;
    public List<TextMeshProUGUI> updatedComponents = new List<TextMeshProUGUI>();

    [ContextMenu("Change Font")]
    public void ChangeFont()
    {
        // Find all TextMeshProUGUI components in the hierarchy.
        TextMeshProUGUI[] textComponents = GetComponentsInChildren<TextMeshProUGUI>(true);

        updatedComponents.Clear();

        foreach (TextMeshProUGUI textComponent in textComponents)
        {
            // Check if the component already has the new font to avoid unnecessary changes.
            if (textComponent.font != newFont)
            {
                // Change the font.
                textComponent.font = newFont;
                updatedComponents.Add(textComponent);
            }
        }

        Debug.Log("Updated TextMeshProGUI components: " + updatedComponents.Count);
    }
}
