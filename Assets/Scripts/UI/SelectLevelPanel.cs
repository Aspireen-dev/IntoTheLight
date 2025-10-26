using UnityEngine;
using UnityEngine.UI;

public class SelectLevelPanel : BasePanel
{
    [SerializeField] private Button[] levelButtons;

    private void Start()
    {
        EnableLevelButtons();
    }

    public void EnableLevelButtons()
    {
        int unlockedLevels = PlayerSettingsManager.UnlockedLevels;

        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool interactable = i < unlockedLevels;
            levelButtons[i].interactable = interactable;
        }
    }

    public void OnLevelButtonClick(int level)
    {
        GlobalSceneManager.LoadLevel(level);
    }
}
