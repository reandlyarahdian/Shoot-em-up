using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Center Radius", menuName ="SO/Radius")]
public class CenterRadius : Behaviour
{
    public float radius;
    public Vector2 center;
    public override Vector2 CalculateMove(Enemy agent, List<Transform> context, EnemyController flock)
    {
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float t = centerOffset.magnitude / radius;
        if (t < 0.9f)
        {
            return Vector2.zero;
        }

        return centerOffset * t * t;
    }
}
