using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem
{
    public static LevelInfo currentLevel;


    public static void LoadLevel(LevelInfo i_level)
    {
        currentLevel = i_level;
        SceneManager.LoadScene(i_level.levelNumber.ToString());
    }

    public static int GetNumStars(int i_level)
    {
        return 0;
    }

    public static void FinishLevel()
    {
        // TODO: do a cool looking sequence instead of just ending

        // is there a next level?
        int newLevelNum = 2;

        if (currentLevel)
        {
            newLevelNum = currentLevel.levelNumber + 1;
        }
        else
        {
#if UNITY_EDITOR
            // figure out our current level
            Scene activeScene = SceneManager.GetActiveScene();
            if(activeScene != null)
            {
                if(int.TryParse( activeScene.name, out newLevelNum))
                {
                    newLevelNum++;
                }
            }
#endif
        }
        List<LevelInfo> levels = new List<LevelInfo>(Resources.LoadAll<LevelInfo>("Levels"));
        bool foundlevel = false;
        for(int i = 0; i < levels.Count; i++)
        {
            if(levels[i].levelNumber == newLevelNum)
            {
                string sceneNameToLoad = levels[i].levelNumber.ToString();
                Scene newScene = SceneManager.GetSceneByName(sceneNameToLoad);
                if(newScene != null)
                {
                    SceneManager.LoadScene(sceneNameToLoad);
                }
                else
                {
                    Debug.LogError("Scene with name " + sceneNameToLoad + " is not in build settings!!!");
                    SceneManager.LoadScene("LevelSelect");
                }
                
                foundlevel = true;
                break;
            }
        }

        // no more levels??
        // You beat the game!!!!
        // todo: hook up ending sequence?
        SceneManager.LoadScene("LevelSelect");
    }
}