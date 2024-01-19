// References:
// - https://diegogiacomelli.com.br/unitytips-hierarchy-window-group-header/

using UnityEngine;
using UnityEditor;
using System;

[InitializeOnLoad]
public static class HierarchyHeader
{
    static HierarchyHeader()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HierarchyWindowItemOnGUI;
    }

    private static void HierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (gameObject != null && gameObject.name.StartsWith("---", StringComparison.Ordinal))
        {
            EditorGUI.DrawRect(selectionRect, Color.black);
            EditorGUI.LabelField
            (
                selectionRect, 
                $"<color=white>{gameObject.name.Replace("---", "")}</color>",
                new GUIStyle()
                {
                    alignment = TextAnchor.MiddleCenter,
                    fontStyle = FontStyle.Bold,
                    richText = true
                }
            );
        }
    }
}