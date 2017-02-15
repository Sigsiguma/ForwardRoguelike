using UnityEngine;
using System.Collections;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
    private static T instance = null;

    public static T Instance {
        get {
            if (instance == null) {
                instance = (T)FindObjectOfType(typeof(T));

                if (instance == null) {
                    Debug.Log(typeof(T) + "is nothing!");
                }
            }
            return instance;
        }
    }

    protected void Awake() {
        CheckInstance();
    }

    private void CheckInstance() {
        if (instance == null) {
            instance = this as T;
            return;
        } else if (instance == this) {
            return;
        }

        Destroy(gameObject);
    }

    private void OnDestroy() {
        if (instance == this) {
            instance = null;
        }
    }
}
