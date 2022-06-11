using UnityEngine;

/// <summary>
/// Mono singleton Class. Extend this class to make singleton component.
/// Example: 
/// <code>
/// public class Foo : MonoSingleton<Foo>
/// </code>. To get the instance of Foo class, use <code>Foo.Instance</code>
/// Override <code>Init()</code> method instead of using <code>Awake()</code>
/// from this class.
/// </summary>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static bool isInitialized;

    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<T>(true);

                if (instance != null && !isInitialized)
                {
                    isInitialized = true;
                    instance.Init();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else if (instance != this)
        {
            Debug.LogError("Another instance of " + GetType() + " is already exist! Destroying this...");
            DestroyImmediate(this);
            return;
        }
        if (!isInitialized)
        {
            isInitialized = true;
            instance.Init();
        }
    }


    /// <summary>
    /// Use this instead of Awake method for initialization
    /// </summary>
    public virtual void Init() { }
}