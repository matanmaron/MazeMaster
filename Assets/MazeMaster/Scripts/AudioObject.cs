using UnityEngine;

public class AudioObject : MonoBehaviour
{
    void Start()
    {
        if (Settings.MuteSFX)
        {
            Destroy(gameObject);
        }
        Destroy(gameObject, 10f);
    }
}
