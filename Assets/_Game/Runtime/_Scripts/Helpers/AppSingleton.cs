using UnityEngine;

/// <summary>
/// Parent object helping for singleton script and don't destroy on load
/// </summary>
public class AppSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    /// <summary>
    /// Stands for "instance".
    /// </summary>
    public static T i => instance;

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }
}