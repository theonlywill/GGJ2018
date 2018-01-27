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
}