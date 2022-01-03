using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{

    [SerializeField] private GameObject rayPoint;
    private bool houseL;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHouse();
    }

    private void RaycastHouse()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayPoint.transform.position, rayPoint.transform.TransformDirection(Vector3.forward), out hit, 3f))
        {
            GameObject[] houseLights = GameObject.FindGameObjectsWithTag("LightHouse");
            foreach (GameObject i in houseLights)
            {
                i.SetActive(false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(rayPoint.transform.position, rayPoint.transform.TransformDirection(Vector3.forward) * 3f);
    }
}
