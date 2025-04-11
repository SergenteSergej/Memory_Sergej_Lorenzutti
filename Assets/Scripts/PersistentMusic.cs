using UnityEngine;

public class PersistentMusic : MonoBehaviour
{
    private static PersistentMusic instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject); //No duplicate
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject); //Music go on in the next scene

    }
}
