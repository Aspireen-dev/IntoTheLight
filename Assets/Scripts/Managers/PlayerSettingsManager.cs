using UnityEngine;

public static class PlayerSettingsManager
{
    private const string UNLOCKED_LEVELS_KEY = "UnlockedLevels";

    public static int UnlockedLevels
    {
        get => PlayerPrefs.GetInt(UNLOCKED_LEVELS_KEY, 1);
        set
        {
            if (UnlockedLevels < value)
            {
                PlayerPrefs.SetInt(UNLOCKED_LEVELS_KEY, value);
            }
        }
    }

    public static void UnlockNextLevel()
    {
        int currentLevel = GlobalSceneManager.GetCurrentLevel();

        if (currentLevel > 0)
        {
            UnlockedLevels = currentLevel + 1;
        }
    }
}
