using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OrbController : MonoBehaviour
{
    [SerializeField] private float reset = 5.0f;
    private GameObject player;
    public bool isActive;

    //Events
    public static event Action<int> onOrbHPRegen;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        isActive = false;

        Transform[] children = gameObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform item in children)
        {
            if (item.name == "HealthOrb" && item.gameObject.activeSelf)
            {
                isActive = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive == false)
        {
            reset -= Time.deltaTime;

            if (reset <= 0.0f)
            {
                Transform[] children = gameObject.GetComponentsInChildren<Transform>(true);
                foreach (Transform item in children)
                {
                    if (item.name == "HealthOrb")
                    {
                        isActive = true;
                        item.gameObject.SetActive(true);
                        reset = 5.0f;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isActive)
        {
            if (GameObject.Find("Player").GetComponent<PlayerController>().getPlayerLife() <= 90)
            {
                onOrbHPRegen?.Invoke(10);
            }
            isActive = false;
            AudioSource audio = gameObject.GetComponent<AudioSource>();
            audio.Play();

            Transform[] children = gameObject.GetComponentsInChildren<Transform>(true);
            foreach (Transform item in children)
            {
                if (item.name == "HealthOrb" && item.gameObject.activeSelf)
                {
                    item.gameObject.SetActive(false);
                }
            }

        }
    }
}
