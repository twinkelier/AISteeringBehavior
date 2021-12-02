using UnityEngine;

public static class Support
{
    /// <summary>
    /// Draws a ray in the game scene
    /// </summary>
    /// <param name="position">starting point ray</param>
    /// <param name="direction">direction the ray has to go</param>
    /// <param name="color">color of the ray</param>
    public static void DrawRay(Vector3 position, Vector3 direction, Color color)
    {
        if (direction.sqrMagnitude > 0.001f)
        {
            Debug.DrawRay(position, direction, color);
            DrawSolidDisc(position + direction, .25f, color);
        }
    }

    /// <summary>
    /// Draws a label in the game scene
    /// </summary>
    /// <param name="position">position of the label</param>
    /// <param name="label">text of the label</param>
    /// <param name="color">color of the label</param>
    public static void DrawLabel(Vector3 position, string label, Color color)
    {
        GUIStyle style = new GUIStyle();
        style.normal.textColor = color;

#if UNITY_EDITOR
        if (label.Length > 0)
        {
            UnityEditor.Handles.BeginGUI();
            UnityEditor.Handles.Label(position, label, style);
            UnityEditor.Handles.EndGUI();
        }
#endif
    }

    /// <summary>
    /// Draws a solid disc at the given position
    /// </summary>
    /// <param name="position">position of the solid disc</param>
    /// <param name="radius">size of the solid disc</param>
    /// <param name="color">color of the solid disc</param>
    public static void DrawSolidDisc(Vector3 position, float radius, Color color)
    {
#if UNITY_EDITOR
        UnityEditor.Handles.color = color;
        UnityEditor.Handles.DrawSolidDisc(position, Vector3.up, radius);
#endif
    }
}

