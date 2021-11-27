using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Pool Data", menuName ="SO/Pool")]
public class PoolData : ScriptableObject
{
    public GameObject prefabs;
    public int NumberPool;
    public Enemy enemy;
}
