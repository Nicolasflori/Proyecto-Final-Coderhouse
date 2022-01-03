using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    private GameObject options;
    private bool optActive = false;
    // Start is called before the first frame update
    void Start()
    {
        //options = GameObject.Find("OptionsPanel");
        //options.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickNewGame()
    {
        SceneManager.LoadScene("Level 1");
        Time.timeScale = 1;
    }

    public void OnClickQuit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void OnClickOptions()
    {
        if  (!optActive){
            options.SetActive(true);
            optActive = true;
        }
        else
        {
            options.SetActive(false);
            optActive = false;
        }
        
    }

    public void OnClickLoad()
    {
        GameManager.instance.Load();
        Time.timeScale = 1;
    }
}
