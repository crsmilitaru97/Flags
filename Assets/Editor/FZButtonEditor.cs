using UnityEditor;
using UnityEditor.UI;

//08.12.22

[CustomEditor(typeof(FZButton))]
public class FZButtonEditor : ButtonEditor
{
    public override void OnInspectorGUI()
    {
        FZButton targetButton = (FZButton)target;
        targetButton.playClickSound = EditorGUILayout.Toggle("Play Click Sound", targetButton.playClickSound);
        targetButton.buttonText = (FZText)EditorGUILayout.ObjectField("Button Text", targetButton.buttonText, typeof(FZText), true);
        targetButton.buttonImage = (FZImage)EditorGUILayout.ObjectField("Button Image", targetButton.buttonImage, typeof(FZImage), true);
        targetButton.selectedColor = EditorGUILayout.ColorField("Selected Color", targetButton.selectedColor);
        targetButton.isSelected = EditorGUILayout.Toggle("Selected", targetButton.isSelected);

        base.OnInspectorGUI();
    }
}
