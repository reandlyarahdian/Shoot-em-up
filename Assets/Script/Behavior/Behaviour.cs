using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Behaviour : ScriptableObject
{
    public abstract Vector2 CalculateMove(Enemy agent, List<Transform> context, EnemyController flock);
}
