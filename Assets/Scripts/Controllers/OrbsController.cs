using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbsController : MonoBehaviour
{

    [SerializeField] GameObject[] orbs;
    [SerializeField] GameObject active;

    // Start is called before the first frame update
    void Start()
    {
        EnableOrbs();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void EnableOrbs()
    {
        for (int i = 0; i < orbs.Length; i++)
        {
            Transform[] children = orbs[i].gameObject.GetComponentsInChildren<Transform>(true);
            foreach (Transform item in children)
            {
                if (item.name == "HealthOrb")
                {
                    item.gameObject.SetActive(true);
                }
            }
        }
    }
}
