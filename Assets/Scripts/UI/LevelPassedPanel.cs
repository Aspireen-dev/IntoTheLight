using UnityEngine;

public class LevelPassedPanel : BasePanel
{
    [SerializeField] private GameObject _content;

    public void OnLevelPassed()
    {
        _content.SetActive(true);
    }

    public void OnMenuButtonClick()
    {
        GlobalSceneManager.LoadMenu();
    }

    public void OnNextLevelButtonClick()
    {
        GlobalSceneManager.LoadNextLevel();
    }
}
