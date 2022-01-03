using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy
{
    private float specialAttackCD = 5.0f;
    private float timer = 0;
    private int shield = 100;
    private bool specialAttack = false;
    private float specialAttackDistance = 10.0f;

    public override void Update()
    {
        //PlayerController.onDeath += OnPlayerDeath;

        if (Vector3.Distance(transform.position, player.transform.position) <= data.rangeOfView && !playerDead)
        {
            playerOnRange = true;
            isRunning = true;
        }
        else
        {
            isRunning = false;
            playerOnRange = false;
        }

        if (playerOnRange && !playerDead)
        {
            ChasePlayer();
        }

        if (Vector3.Distance(transform.position, player.transform.position) > 2 && !playerDead)
        {
            attack = false;
        }
        else if (!playerDead)
        {
            attack = true;
            if (canAttack && enemyLife > 0 && !playerDead)
            {
                canAttack = false;
                player.GetComponent<PlayerController>().DamagePlayer(data.enemyAttackDMG);
                player.gameObject.GetComponent<PlayerController>().playHit();
                Debug.Log(this.name + " te ha hecho 10 de daño.");
                AudioSource audio = gameObject.GetComponent<AudioSource>();
                audio.Play();
            }
            else
            {
                if (timerForNextAttack > 0)
                {
                    timerForNextAttack -= Time.deltaTime;

                }
                else if (timerForNextAttack <= 0)
                {
                    canAttack = true;
                    timerForNextAttack = cooldown;
                }
            }
        }

        AnimationEnemy.SetBool("isRunning", isRunning);
        AnimationEnemy.SetBool("attack", attack);
        AnimationEnemy.SetBool("die", die);

        if (die)
        {
            enemySpeed = 0;
            speedToLook = 0;
            StartCoroutine(DyingDelay());
        }
    }

    void SpecialAttack()
    {
        timer += Time.deltaTime;

        if (canAttack && timer == specialAttackCD && Vector3.Distance(transform.position, player.transform.position) >= specialAttackDistance)
        {
            AnimationEnemy.SetBool("special", specialAttack);
            timer = 0;
        }
    }

    public override void ChasePlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > 2)
        {
            isRunning = true;
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, direction, speedToLook * Time.deltaTime);
            transform.position += transform.forward * enemySpeed * Time.deltaTime;
        }
    }
}
