using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private string[] _showCursorScenes;
    [SerializeField] private GameObject _optionMenu;

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        UpdateCursorVisibility(SceneManager.GetActiveScene().name);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateCursorVisibility(scene.name);
    }

    void Update()
    {
        if (_optionMenu != null && _optionMenu.activeInHierarchy)
        {
            Cursor.visible = true;
        }
        else
        {
            UpdateCursorVisibility(SceneManager.GetActiveScene().name);
        }
    }

    void UpdateCursorVisibility(string sceneName)
    {
        bool showCursor = false;
        foreach (string _scene in _showCursorScenes)
        {
            if (_scene == sceneName)
            {
                showCursor = true;
                break;
            }
        }
        Cursor.visible = showCursor;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
