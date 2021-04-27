using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public void LoadGame()
    {
        FindObjectOfType<AudioManager>().Play("Select");
        SceneManager.LoadScene(1);

    }

    public void LoadHowToPlay()
    {
        FindObjectOfType<AudioManager>().Play("Select");
        SceneManager.LoadScene(3);
    }
    public void QuitGame()
    {
        Application.Quit();

    }

}
