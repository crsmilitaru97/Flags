using UnityEditor;
using UnityEditor.UI;

//10.10.22

[CustomEditor(typeof(FZText))]

public class FZTextEditor : TextEditor
{
    public override void OnInspectorGUI()
    {
        FZText targetText = (FZText)target;
        targetText.timeInterval = EditorGUILayout.FloatField("Time Interval", targetText.timeInterval);
        base.OnInspectorGUI();
    }
}
