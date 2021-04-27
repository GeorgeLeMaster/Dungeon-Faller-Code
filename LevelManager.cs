using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAgain()
    {
        SaveSystem.SavePlayer(GameObject.Find("Player").GetComponent<PlayerCollision>());
        FindObjectOfType<AudioManager>().Play("Select");

        SceneManager.LoadScene(1);
    }


    public void OpenShop()
    {
        SaveSystem.SavePlayer(GameObject.Find("Player").GetComponent<PlayerCollision>());
        FindObjectOfType<AudioManager>().Play("Select");

        SceneManager.LoadScene(2);

    }

    public void MainMenu()
    {
        FindObjectOfType<AudioManager>().Play("Select");

        SceneManager.LoadScene(0);


    }

    public void QuitGame()
    {
        SaveSystem.SavePlayer(GameObject.Find("Player").GetComponent<PlayerCollision>());
        Application.Quit();

    }
}
