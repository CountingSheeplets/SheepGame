using UnityEngine;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

public abstract class Singleton<T> : Singleton where T : MonoBehaviour
{
    #region  Fields
    [CanBeNull]
    private static T _instance;

    [NotNull]
    // ReSharper disable once StaticMemberInGenericType
    private static readonly object Lock = new object();

    [SerializeField]
    private bool _persistent = false;
    #endregion

    #region  Properties
    [NotNull]
    public static T Instance
    {
        get
        {
            if (Quitting)
            {
                Debug.LogWarning($"[{nameof(Singleton)}<{typeof(T)}>] Instance will not be returned because the application is quitting.");
                // ReSharper disable once AssignNullToNotNullAttribute
                return null;
            }
            lock (Lock)
            {
                if (_instance != null)
                    return _instance;
                var instances = FindObjectsOfType<T>();
                var count = instances.Length;
                if (count > 0)
                {
                    if (count == 1){
                        _instance = instances[0];
                        (_instance as Singleton<T>).OnInit();
                        return _instance;
                    }
                    Debug.LogWarning($"[{nameof(Singleton)}<{typeof(T)}>] There should never be more than one {nameof(Singleton)} of type {typeof(T)} in the scene, but {count} were found. The first instance found will be used, and all others will be destroyed.");
                    for (var i = 1; i < instances.Length; i++)
                        Destroy(instances[i]);
                    _instance = instances[0];
                    (_instance as Singleton<T>).OnInit();
                    return _instance;
                }

                Debug.Log($"[{nameof(Singleton)}<{typeof(T)}>] An instance is needed in the scene and no existing instances were found, so a new instance will be created.");
                _instance = new GameObject($"({nameof(Singleton)}){typeof(T)}").AddComponent<T>();
                (_instance as Singleton<T>).OnInit();
                return _instance;
            }
        }
    }
    #endregion

    #region  Methods
    private void Awake()
    {
        if (_persistent)
            DontDestroyOnLoad(gameObject);
        else
            SceneManager.sceneUnloaded += OnSceneUnload;
        OnAwake();
        Debug.LogWarning("Singleton Awoken: "+this.GetType().Name);
    }
    protected virtual void OnSceneUnload<Scene> (Scene scene) { Quitting = true;}
    protected virtual void OnAwake() { }
    protected virtual void OnInit() { }
    #endregion
}

public abstract class Singleton : MonoBehaviour
{
    #region  Properties
    public static bool Quitting { get; set; }
    #endregion

    #region  Methods
    private void OnApplicationQuit()
    {
        Quitting = true;
    }
    #endregion
}