using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimePeriod { Plain, Cowboy, City, Cyberpunk };

[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    public string weaponName;
    public TimePeriod era;
    public float damage;
    public GameObject weaponPrefab;
}
