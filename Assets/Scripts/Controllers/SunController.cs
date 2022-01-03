using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunController : MonoBehaviour
{

    [SerializeField] private Vector3 DayNightCircle = new Vector3(5f, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = DayNightCircle;
    }

    // Update is called once per frame
    void Update()
    {

    }
}