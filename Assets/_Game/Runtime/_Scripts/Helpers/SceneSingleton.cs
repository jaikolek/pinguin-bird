using UnityEngine;

/// <summary>
/// Parent object helping for singleton script on scene
/// </summary>
public class SceneSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    /// <summary>
    /// Stands for "instance".
    /// </summary>
    public static T i => instance;

    protected virtual void OnDestroy()
    {
        if (instance = this as T)
            instance = null;
    }

    protected virtual void Awake()
    {
        if (instance == null)
            instance = this as T;
        else
            Debug.LogWarning($"{typeof(T)} already exists.");
    }
}