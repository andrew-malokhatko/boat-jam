using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Объект не уничтожается при загрузке новой сцены
        }
        else
        {
            Destroy(gameObject); // Удаляем дубликат, если он уже есть
        }
    }
}
