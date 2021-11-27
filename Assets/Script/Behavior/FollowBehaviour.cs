using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Following", menuName = "SO/Follow")]
public class FollowBehaviour : CenterRadius
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;

    public override Vector2 CalculateMove(Enemy agent, List<Transform> context, EnemyController flock)
    {
        //if no neighbors, return no adjustment
        if (context.Count == 0)
            return Vector2.zero;
        //add all points together and average
        Vector2 followMove = Vector2.zero;
        foreach (Transform item in context)
        {
            if (Vector3.Distance(flock.player.transform.position, center) < radius)
            {
                followMove += (Vector2)flock.player.transform.position;
            }
            else
            {
                followMove += (Vector2)item.position;
            }
        }
        followMove /= context.Count;

        //create offset from agent position
        followMove -= (Vector2)agent.transform.position;
        followMove = Vector2.SmoothDamp(agent.transform.up, followMove, ref currentVelocity, agentSmoothTime);
        return followMove;
    }
}
