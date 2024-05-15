using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionSystem : MonoBehaviour
{
    public void SceneTransitionTitle()
    {
        SceneManager.LoadScene("Title");
    }
    public void SceneTransitionSelect()
    {
        SceneManager.LoadScene("Select");
    }
    public void SceneTransitionMain(int stageNumber)
    {
        SceneManager.LoadScene("Stage" + stageNumber);
    }
    public void SceneRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GameEnd()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
}
