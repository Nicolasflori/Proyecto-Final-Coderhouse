using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopBackgroundMusic : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        HUDController.onToggleMusic += OnMusicChange;
    }

    void Update()
    {
        player = GameObject.Find("Player");

        if (player.GetComponent<PlayerController>().die || GameManager.instance.win)
        {
            AudioSource audio = gameObject.GetComponent<AudioSource>();
            audio.Stop();
        }
    }

    private void OnMusicChange(bool active)
    {
        if (active)
        {
            AudioSource audio = gameObject.GetComponent<AudioSource>();
            audio.Play();
        }
        else
        {
            AudioSource audio = gameObject.GetComponent<AudioSource>();
            audio.Stop();
        }
    }
}
