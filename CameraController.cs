using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CameraController : MonoBehaviour
{

    private GameObject player;
    private AudioSource gameMusic;

    private bool isPlaying;

    // Start is called before the first frame update
    void Start()
    {
        gameMusic = gameObject.GetComponent<AudioSource>();

        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player.transform.position.y <= 0)
        {
            gameObject.transform.position = new Vector3(0, player.transform.position.y, -10);

            if (isPlaying == false)
            {
                gameMusic.Play();
                isPlaying = true;
            }
        }


    }
}
