using SickscoreGames.HUDNavigationSystem;
using UnityEngine;

public class PlayerTagDirector_KH : MonoBehaviour
{
    private HUDNavigationSystem _navigationSystem = default;
    private HUDNavigationElement _navigationElement = default;

    void Start()
    {
        _navigationSystem = GetComponent<HUDNavigationSystem>();
        _navigationElement = GetComponent<HUDNavigationElement>();
    }

    private void GameObjectTagJudge()
    {
        if (gameObject.CompareTag("Player"))
        {
            _navigationSystem.enabled = true;
            _navigationElement.enabled = false;
        }
        else if (gameObject.CompareTag("Enemy") || gameObject.CompareTag("Boss"))
        {
            _navigationSystem.enabled = false;
            _navigationElement.enabled = true;
        }
    }
}
