using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public void Scene2()
    {
        Debug.Log("Pressed!");
        SceneManager.LoadScene("Scene2");

    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Scene1");
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Scene3");
    }

    public void Quit()
    {
        Debug.Log("QUIT GAME");
        Application.Quit();
    }
}
