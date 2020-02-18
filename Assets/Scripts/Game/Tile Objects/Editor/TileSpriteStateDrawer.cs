using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
// IngredientDrawerUIE
[CustomPropertyDrawer(typeof(TileSpriteState))]
public class TileSpriteStateDrawer : PropertyDrawer
{
    public FieldTileSpriteType typePlaceholder;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);
        // Draw label
        //////position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        float width = 40f;
        float labelW = 35f;
        float height = 20f;
        EditorGUIUtility.labelWidth = labelW;

        SerializedProperty matrixProperty = property.FindPropertyRelative("tileState");
 /*        for (int i = 0; i < 3; i++){
            GUILayout.BeginHorizontal();
            for(int j = 0; j < 3; j++){
                SerializedProperty itemProperty = matrixProperty.GetArrayElementAtIndex(i).GetArrayElementAtIndex(j);
                var rectVar = new Rect(position.x + height * i, position.y + width * j, position.width, position.height);
                EditorGUI.EnumPopup(rectVar, (FieldTileSpriteType)itemProperty); // matrixProperty = (FieldTileSpriteType)
            }
            GUILayout.EndHorizontal();
        } */
        //EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("level"), new GUIContent("lvl:","Current Level. Should be 0 to start with."));

        GUIStyle tableStyle = new GUIStyle ("box");
        tableStyle.padding = new RectOffset (10, 10, 10, 10);
        tableStyle.margin.left = 32;

        GUIStyle columnStyle = new GUIStyle ();
        columnStyle.fixedWidth = 65;

        GUIStyle rowStyle = new GUIStyle ();
        rowStyle.fixedHeight = 25;

        GUIStyle enumStyle = new GUIStyle ("popup");
        rowStyle.fixedWidth = 65;

/*         EditorGUILayout.BeginHorizontal (tableStyle);
        for (int x = 0; x < 3; x++) {
            EditorGUILayout.BeginVertical ();
            for (int y = 0; y < 3; y++) {
                EditorGUILayout.BeginHorizontal (rowStyle);
                //matrixProperty[x, y] = (FieldTileSpriteType)EditorGUILayout.EnumPopup(property.GetArrayElementAtIndex(x).GetArrayElementAtIndex(y), enumStyle);
                EditorGUILayout.PropertyField(matrixProperty);
                EditorGUILayout.EndHorizontal ();
            }
            EditorGUILayout.EndVertical ();
        }
        EditorGUILayout.EndHorizontal (); */

        EditorGUI.EndProperty();
        EditorGUI.indentLevel = indent;
    }
}