using UnityEngine;

/// <summary>
/// Parent object helping for scriptableobject script
/// </summary>
public class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
{
    private static T instance;

    /// <summary>
    /// Stands for "instance".
    /// </summary>
    public static T i
    {
        get
        {
            if (instance == null)
                instance = Resources.Load<T>(typeof(T).ToString());

            return instance;
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance = this as T)
            instance = null;
    }

    protected virtual void Awake()
    {
        if (instance != null)
            Debug.LogWarning($"{typeof(T)} already exists.");
    }
}
