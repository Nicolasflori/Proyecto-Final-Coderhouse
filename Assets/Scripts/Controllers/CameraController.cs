using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private List<GameObject> cameras;
    [SerializeField] private int indexCamera;

    // Start is called before the first frame update
    void Start()
    {
        cameras[0].SetActive(true);
        cameras[1].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            indexCamera++;
            if(indexCamera == cameras.Count)
            {
                indexCamera = 0;
            }
            SwitchCamera(indexCamera);
        }
    }

    private void SwitchCamera (int index)
    {
        foreach (var cam in cameras)
        {
            cam.SetActive(false);
        }

        cameras[index].SetActive(true);
    }
}
