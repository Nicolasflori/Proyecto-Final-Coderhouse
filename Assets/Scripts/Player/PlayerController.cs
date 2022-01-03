using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected PlayerData data;
    [SerializeField] private Animator AnimationPlayer;
    [SerializeField] private AudioClip[] hits;
    [SerializeField] private LayerMask enemyLayer;

    public bool makeDamage = false;
    public bool healed = false;
    public bool win = false;
    public bool die = false;
    public ParticleSystem hit;

    private float cameraAxis;
    private bool isRunning = false; 
    private bool attack = false;
    private bool isAttacking = false;
    private bool isAnimationAttack = false;
    private int scoreMax;
    private AudioSource audioSource;
    private int playerLife;
    private float playerSpeed;

    //Events
    public static event Action onDeath;

    // Start is called before the first frame update
    private void Start()
    {
        AnimationPlayer.SetBool("isRunning", false);
        scoreMax = GameManager.instance.getScoreMax();
        audioSource = GetComponent<AudioSource>();
        playerLife = data.life;
        playerSpeed = data.velocity;
        OrbController.onOrbHPRegen += OrbHandler;
        HUDController.onToggleSound += OnSoundChange;
        GameManager.onWin += OnWinAnimation;
        win = false;

        Transform[] children = this.gameObject.GetComponentsInChildren<Transform>(true);
        foreach (Transform item in children)
        {
            if (item.name == "HitBlood")
            {
                hit = item.GetComponent<ParticleSystem>();
            }
        }
    }

    private void OrbHandler(int hp)
    {
        playerLife += hp;
    }

    // Update is called once per frame
    private void Update()
    {
        Movement();
        RotatePlayer();
        AnimationAttack();
        IsAlive();

        if (Input.GetKeyDown(KeyCode.Mouse0) && !attack)
        {
            Attack();
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ResetAttack();
        }

        win = GameManager.instance.win;

        AnimationPlayer.SetBool("isRunning", isRunning);
        AnimationPlayer.SetBool("attack", attack);
        AnimationPlayer.SetBool("die", die);
        AnimationPlayer.SetBool("win", win);
    }

    public int HealPlayer(int lifeHealed, int playerLife)
    {
        if (playerLife < 100)
            playerLife = playerLife + lifeHealed;
        return playerLife;
    }

    public void DamagePlayer(int damage)
    {
        playerLife = playerLife - damage;
        if (data.life > 0)
        {
            AudioClip clip = GetRandomClip();
            audioSource.PlayOneShot(clip);
        }
    }

    private AudioClip GetRandomClip()
    {
        return hits[UnityEngine.Random.Range(0, hits.Length)];
    }

    private void IsAlive()
    {
        if (playerLife <= 0)
        {
            die = true;
            isRunning = false;
            attack = false;
            playerSpeed = 0;
            onDeath?.Invoke();
        }
        else
        {
            die = false;
        }
    }

    private void Movement()
    {
        float ejeHorizontal = Input.GetAxisRaw("Horizontal");
        float ejeVertical = Input.GetAxisRaw("Vertical");

        if (ejeHorizontal != 0 || ejeVertical != 0)
        {
            isRunning = true;
            transform.Translate(playerSpeed * Time.deltaTime * new Vector3(ejeHorizontal, 0, ejeVertical));
        }
        else
        {
            isRunning = false;
        }
    }

    private void RotatePlayer()
    {
        cameraAxis += Input.GetAxis("Mouse X");
        Quaternion angulo = Quaternion.Euler(0, cameraAxis, 0);
        transform.localRotation = angulo;
    }

    private void Attack()
    {
        attack = true;

    }
    private void ResetAttack()
    {
        attack = false;
    }

    private void AnimationAttack()
    {
        if (AnimationPlayer.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            isAnimationAttack = true; 
        }
        else
        {
            isAnimationAttack = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Chest"))
        {
            GameManager.instance.addScore(1);
            AudioSource audio = gameObject.GetComponent<AudioSource>();
            audio.Play();
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Enemy") && isAnimationAttack)
        {
            if (!isAttacking)
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(data.attackDMG);
                other.gameObject.GetComponent<Enemy>().playHit();
                isAttacking = true;
                AudioClip audio = this.GetComponent<SwordHit>().clip;
                this.GetComponent<AudioSource>().PlayOneShot(audio);
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Portal"))
        {
            GameManager.instance.checkWin();
        }
    }

    public int getPlayerLife()
    {
        return playerLife;
    }

    public void setPlayerLife(int life)
    {
        playerLife = life;
    }

    public void playHit()
    {
        hit.Play();
    }

    public void OnSoundChange(bool active)
    {
        if (active)
        {
            this.gameObject.GetComponent<AudioListener>().enabled = true;
        }
        else
        {
            this.gameObject.GetComponent<AudioListener>().enabled = false;
        }
    }

    private void OnWinAnimation()
    {
        win = true;
    }
}