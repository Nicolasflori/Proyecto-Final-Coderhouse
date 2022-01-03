using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Text hpText;
    [SerializeField] private Text chestText;
    [SerializeField] private Image gameover;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject optionsPanel;
    private bool optionsFlag;
    private int currentScore;
    private GameObject player;

    //Events
    public static event Action onPause;
    public static event Action onUnPause;
    public static event Action<bool> onTogglePP;
    public static event Action<bool> onToggleSound;
    public static event Action<bool> onToggleMusic;
    public static event Action onLoad;
    public static event Action onSave;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        PlayerController.onDeath += OnDeathHandler;
        Enemy.onEnemyDeath += OnEnemyKillController;
        gameover.gameObject.SetActive(false);
        optionsPanel.gameObject.SetActive(false);
        currentScore = 0;
        optionsFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        chestText.text = GameManager.instance.currentScore.ToString();
        if(hpText.text != "0")
        {
            hpText.text = player.gameObject.GetComponent<PlayerController>().getPlayerLife().ToString();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && optionsFlag == false)
        {
            onPause?.Invoke();
            optionsPanel.gameObject.SetActive(true);
            optionsFlag = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && optionsFlag == true)
        {
            onUnPause?.Invoke();
            optionsPanel.gameObject.SetActive(false);
            optionsFlag = false;
        }
    }

    private void OnDeathHandler()
    {
        gameover.gameObject.SetActive(true);
    }

    private void OnEnemyKillController()
    {
        currentScore = currentScore + 5;
        scoreText.text = currentScore.ToString();
    }

    public void OnClickSave()
    {
        onSave?.Invoke();
    }

    public void OnClickLoad()
    {
        onLoad?.Invoke();
    }

    public void OnClickQuit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnTogglePP(bool active)
    {
        if (active)
        {
            onTogglePP?.Invoke(active);
        }
        else
        {
            onTogglePP?.Invoke(active);
        }
    }

    public void OnToggleSound(bool active)
    {
        if (active)
        {
            onToggleSound?.Invoke(active);
        }
        else
        {
            onToggleSound?.Invoke(active);
        }
    }

    public void OnToggleMusic(bool active)
    {
        if (active)
        {
            onToggleMusic?.Invoke(active);
        }
        else
        {
            onToggleMusic?.Invoke(active);
        }
    }

}
