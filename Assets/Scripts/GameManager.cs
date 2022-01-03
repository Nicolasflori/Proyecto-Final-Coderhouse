using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private GameObject player;
    private GameObject playerPause;
    private GameObject boss;
    private bool pauseState;
    private int scoreMax;

    public bool playerDie = false;
    public int currentScore;
    public bool win;

    //Events
    public static event Action onWin;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            ClearValues();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ClearValues();
        player = GameObject.Find("Player");
        Cursor.lockState = CursorLockMode.Confined;
        PlayerController.onDeath += GameOver;
        HUDController.onPause += PauseState;
        HUDController.onUnPause += UnPauseState;
        HUDController.onLoad += Load;
        HUDController.onSave += Save;
        pauseState = false;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Cursor.lockState = CursorLockMode.Confined;
        }

        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (!pauseState)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }

            if (win)
            {
                AudioSource audio = gameObject.GetComponent<AudioSource>();
                audio.Play();
                win = false;
                currentScore = 0;
                StartCoroutine(WinDelay1());
            }
        }
        else if (SceneManager.GetActiveScene().name == "Level 2")
        {
            if (!pauseState)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }

            if (win)
            {
                AudioSource audio = gameObject.GetComponent<AudioSource>();
                audio.Play();
                win = false;
                currentScore = 0;
                StartCoroutine(WinDelay2());
            }
        }
        else if (SceneManager.GetActiveScene().name == "Level 3")
        {
            if (!pauseState)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
            }

            if (win)
            {
                AudioSource audio = gameObject.GetComponent<AudioSource>();
                audio.Play();
                win = false;
                currentScore = 0;
                StartCoroutine(WinDelay3());
            }
        }

        if (SceneManager.GetActiveScene().name == "End" && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        //if (SceneManager.GetActiveScene().name == "Level 3")
        //{
        //    StartCoroutine(WinDelay4());
        //}
    }

    private void PauseState()
    {
        //Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        playerPause = GameObject.Find("Player");
        playerPause.GetComponent<PlayerController>().enabled = false;
        pauseState = true;
    }

    private void UnPauseState()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        playerPause = GameObject.Find("Player");
        playerPause.GetComponent<PlayerController>().enabled = true;
        pauseState = false;
    }

    private void GameOver()
    {
        playerDie = false;
        currentScore = 0;
        StartCoroutine(DieDelay());
    }

    //corutina para esperar 5 segundos y pasar al 2do nivel al ganar
    IEnumerator WinDelay1()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Level 2");
    }

    IEnumerator WinDelay2()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("Level 3");
    }

    IEnumerator WinDelay3()
    {
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("End");
    }

    //IEnumerator WinDelay4()
    //{
    //   yield return new WaitForSeconds(4);
    //    SceneManager.LoadScene("Main Menu");
    //}

    //corutina para esperar 5 segundos
    IEnumerator DieDelay()
    {
        yield return new WaitForSeconds(5);
        //SceneManager.LoadScene("MainMenu");
        UnityEditor.EditorApplication.isPlaying = false;
    }

    private void ClearValues()
    {
        scoreMax = 5;
        currentScore = 0;
        win = false;
    }

    public int getScoreMax()
    {
        return scoreMax;
    }

    public void addScore(int score)
    {
        currentScore += score;
        Debug.Log("currentScore");
    }

    public void checkWin()
    {
        if (currentScore >= scoreMax)
        {
            win = true;
            Debug.Log("WIN");
            onWin?.Invoke();
        }
        else
        {
            Debug.Log("Primero debes juntar los 5 cofres!");
        }
    }

    //Clase serializada para guardar escena en disco (json)
    [System.Serializable]
    class GameData
    {
        public GameObject playerSave;
        public string scene;
    }

    public void Save()
    {
        //Crear objeto de la clase serializada
        GameData data = new GameData();
        //Asignar valores
        data.playerSave = player;
        data.scene = SceneManager.GetActiveScene().name;
        //Convertir objeto en string
        string json = JsonUtility.ToJson(data);
        //Guardado en ruta por defecto
        File.WriteAllText(Application.persistentDataPath+"/SaveGame.js", json);
    }

    public void Load()
    {
        //Definir ruta
        string path = Application.persistentDataPath + "/SaveGame.js";
        //Confirmar si existe 
        if (File.Exists(path))
        {
            //Leer file
            string json = File.ReadAllText(path);
            //Transformar json a objeto 
            GameData data = JsonUtility.FromJson<GameData>(json);

            Time.timeScale = 1;
            SceneManager.LoadScene(data.scene);
            player = data.playerSave;
            
        }
    }
}
