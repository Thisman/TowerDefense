using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private Dictionary<string, Texture2D> cursors = new Dictionary<string, Texture2D>();

    private void Awake()
    {
        Texture2D[] loadedCursors = Resources.LoadAll<Texture2D>("Images/Cursors");

        foreach (var cursor in loadedCursors)
        {
            cursors[cursor.name] = cursor;
        }
    }

    private void Start()
    {
    }

    public void SetCursor(string cursorName)
    {
    }

    public void ResetCursor()
    {
    }
}
