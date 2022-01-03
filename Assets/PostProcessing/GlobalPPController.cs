using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GlobalPPController : MonoBehaviour
{
    private PostProcessVolume globalVolume;

    // Start is called before the first frame update
    void Start()
    {
        globalVolume = GetComponent<PostProcessVolume>();
        HUDController.onTogglePP += OnPPChange;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnPPChange(bool active)
    {
        if (active)
        {
            globalVolume.enabled = true;
        }
        else
        {
            globalVolume.enabled = false;
        }
    }
}
