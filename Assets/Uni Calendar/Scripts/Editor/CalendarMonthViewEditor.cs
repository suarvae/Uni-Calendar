using UnityEditor;

[CustomEditor(typeof(CalendarMonthView))]
public class CalendarMonthViewEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawPropertiesExcluding(serializedObject, "m_Script", "updateToCurrentDayInEditor", "cellImageColor", "dateFont", "lastValidatedDate");

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Setup", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(serializedObject.FindProperty("updateToCurrentDayInEditor"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("cellImageColor"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("dateFont"));

        serializedObject.ApplyModifiedProperties();
    }
}
