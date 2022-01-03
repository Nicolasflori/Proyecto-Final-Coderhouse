using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "Enemy Data")]

public class EnemyData : ScriptableObject
{
    [SerializeField] public string enemyName;
    [SerializeField] public float enemySpeed;
    [SerializeField] public int enemyAttackDMG;
    [SerializeField] public float attackRange;
    [SerializeField] public int enemyHP;
    [SerializeField] public float rangeOfView;
}
