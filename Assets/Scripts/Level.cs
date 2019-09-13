using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] int delayInSeconds = 2;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().ResetGame();
    }
    public void LoadGameOver()
    {
        StartCoroutine(DelayLevel("Game Over"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator DelayLevel(string sceneName)
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator DelayLevel(int sceneNumber)
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(sceneNumber);
    }
}
