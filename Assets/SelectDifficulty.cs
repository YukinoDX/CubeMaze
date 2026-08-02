using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectDifficulty : MonoBehaviour
{
    public void ClickEasy()
    {
        CreateMaze.size = 5;
        LoadMainScene();
    }

    public void ClickNormal()
    {
        CreateMaze.size = 7;
        LoadMainScene();
    }

    public void ClickHard()
    {
        CreateMaze.size = 9;
        LoadMainScene();
    }

    private void LoadMainScene()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
    }
}
