using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData", menuName = "Player Data")]

public class PlayerData : ScriptableObject
{
    [SerializeField] public int life;
    [SerializeField] public float velocity;
    [SerializeField] public string playerName;
    [SerializeField] public int attackDMG;
}
