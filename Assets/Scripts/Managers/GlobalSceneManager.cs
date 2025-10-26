using UnityEngine;
using UnityEngine.SceneManagement;

public static class GlobalSceneManager
{
    private const string MENU_SCENE_NAME = "Menu";
    private const string LEVEL_SCENE_PREFIX = "Level_";

    public static void LoadMenu()
    {
        SceneManager.LoadScene(MENU_SCENE_NAME);
    }

    public static void LoadLevel(int level)
    {
        string sceneName = LEVEL_SCENE_PREFIX + level;
        SceneManager.LoadScene(sceneName);
    }

    public static void LoadNextLevel()
    {
        int currentLevel = GetCurrentLevel();

        if (currentLevel > 0)
        {
            LoadLevel(currentLevel + 1);
        }
    }

    public static int GetCurrentLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name.StartsWith(LEVEL_SCENE_PREFIX))
        {
            string levelNumberStr = currentScene.name.Substring(LEVEL_SCENE_PREFIX.Length);
            if (int.TryParse(levelNumberStr, out int currentLevel))
            {
                return currentLevel;
            }
        }
        Debug.LogError("Current scene is not a level scene or does not contain a valid level number.");
        return -1;
    }
}
