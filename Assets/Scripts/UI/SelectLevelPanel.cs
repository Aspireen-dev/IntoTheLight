using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevelPanel : BasePanel
{
    private const string LEVEL_SCENE_PREFIX = "Level_";
    private const string UNLOCKED_LEVELS_KEY = "UnlockedLevels";

    [SerializeField] private Button[] levelButtons;

    private void Start()
    {
        EnableLevelButtons();
    }

    public void EnableLevelButtons()
    {
        int unlockedLevels = PlayerPrefs.GetInt(UNLOCKED_LEVELS_KEY, 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool interactable = i < unlockedLevels;
            levelButtons[i].interactable = interactable;
        }
    }

    public void OnLevelButtonClick(int level)
    {
        string sceneName = LEVEL_SCENE_PREFIX + level;
        SceneManager.LoadScene(sceneName);
    }
}
