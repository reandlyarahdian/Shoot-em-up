using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private List<Enemy> avail = new List<Enemy>();
    [SerializeField] private PoolData poolData;
    public GameObject player;
    private float squareMaxSpeed;
    private float squareNeighborRadius;
    private float squareAvoidanceRadius;
    [SerializeField]private float maxSpeed;
    [SerializeField]private float neighborRadius;
    const float AgentDensity = 0.08f;
    public Behaviour behavior;
    [SerializeField]private float avoidanceRadiusMultiplier;

    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    private void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier; ;
        if (poolData == null)
        {
            return;
        }

        for (int i = 0; i < poolData.NumberPool; i++)
        {
            Enemy obj = Instantiate(poolData.enemy, transform);
            ObjReturn(obj);
        }

        for (int i = 0; i < poolData.NumberPool; i++)
        {
            Enemy agent = ObjSpawn(this.transform.position * UnityEngine.Random.Range(0, neighborRadius), Quaternion.Euler(Vector3.forward * UnityEngine.Random.Range(0f, 360f)));
            agent.Initialize(this);
            avail.Add(agent);
        }
    }

    void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<Movement>().gameObject;
        }
        foreach (Enemy agent in avail)
        {
            List<Transform> context = GetNearbyObjects(agent);

            Vector2 move = behavior.CalculateMove(agent, context, this);
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
            Debug.Log(move);
            
        }

        
    }

    private List<Transform> GetNearbyObjects(Enemy agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach (Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }

    public void ObjReturn(Enemy obj)
    {
        if (!avail.Contains(obj))
        {
            obj.transform.position = Vector3.zero;
            obj.transform.rotation = Quaternion.identity;
            obj.gameObject.SetActive(false);
            avail.Add(obj);
        }
    }

    private Enemy ObjRequest()
    {
        Enemy obj = null;
        if (avail.Count > 0)
        {
            obj = avail[0];
            avail.Remove(obj);
        }
        return obj;
    }

    public Enemy ObjSpawn(Vector3 pos, Quaternion rot)
    {
        Enemy obj = ObjRequest();
        if (obj)
        {
            obj.transform.position = pos;
            obj.transform.rotation = rot;
            obj.gameObject.SetActive(true);
        }
        return obj;
    }
}
