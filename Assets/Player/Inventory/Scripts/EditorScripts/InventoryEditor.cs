using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Inventory))]
[CanEditMultipleObjects]
public class InventoryEditor : Editor
{
    SerializedProperty inventoryUI;
    SerializedProperty items;
    SerializedProperty quantities;
    void OnEnable()
    {
        inventoryUI = serializedObject.FindProperty("_inventoryPanel");
        items = serializedObject.FindProperty("_itemList");
        quantities = serializedObject.FindProperty("_quantityList");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.LabelField("Place the parent object of the Inventory Slots here:");
        serializedObject.Update();
        EditorGUILayout.PropertyField(inventoryUI);
        serializedObject.ApplyModifiedProperties();

        serializedObject.Update();

        int listSize = Mathf.Min(items.arraySize, quantities.arraySize);

        for (int i = 0; i < listSize; i++)
        {
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.PropertyField(items.GetArrayElementAtIndex(i), GUIContent.none, true, GUILayout.MinWidth(100));

            EditorGUILayout.PropertyField(quantities.GetArrayElementAtIndex(i), GUIContent.none, true, GUILayout.MinWidth(50));

            EditorGUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
