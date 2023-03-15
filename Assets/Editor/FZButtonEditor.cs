using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

//14.03.23

[CustomEditor(typeof(FZButton))]
public class FZButtonEditor : UnityEditor.UI.ButtonEditor
{
    public override void OnInspectorGUI()
    {
        FZButton targetButton = (FZButton)target;
        targetButton.playClickSound = EditorGUILayout.Toggle("Play Click Sound", targetButton.playClickSound);
        targetButton.buttonText = (Text)EditorGUILayout.ObjectField("Button Text", targetButton.buttonText, typeof(Text), true);
        targetButton.buttonImage = (Image)EditorGUILayout.ObjectField("Button Image", targetButton.buttonImage, typeof(Image), true);
        targetButton.selectedColor = EditorGUILayout.ColorField("Selected Color", targetButton.selectedColor);
        targetButton.isSelected = EditorGUILayout.Toggle("Selected", targetButton.isSelected);

        base.OnInspectorGUI();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(targetButton);
            EditorSceneManager.MarkSceneDirty(targetButton.gameObject.scene);
        }
    }
}
