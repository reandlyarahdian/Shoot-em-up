using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private List<GameObject> avail = new List<GameObject>();
    [SerializeField] private PoolData[] pools;
    [SerializeField] private PoolData poolData;
    [SerializeField] private Transform player;

    private void Start()
    {
        if(poolData == null)
        {
            return;
        }

        if(player == null)
        {
            player = FindObjectOfType<PlayerManager>().transform;
        }

        for(int i = 0; i < poolData.NumberPool; i++)
        {
            GameObject obj = Instantiate(poolData.prefabs,transform);
            ObjReturn(obj);
        }
    }

    public void Data(int poolNum)
    {
        poolData = pools[poolNum];
    }

    public void ObjReturn(GameObject obj)
    {
        if (!avail.Contains(obj))
        {
            obj.transform.position = player.position;
            obj.transform.rotation = player.rotation;
            obj.SetActive(false);
            avail.Add(obj);
        }
    }

    private GameObject ObjRequest()
    {
        GameObject obj = null;
        if(avail.Count > 0)
        {
            obj = avail[0];
            avail.Remove(obj);
        }
        return obj;
    }

    public GameObject ObjSpawn(Vector3 pos, Quaternion rot)
    {
        GameObject obj = ObjRequest();
        if (obj)
        {
            obj.transform.position = pos;
            obj.transform.rotation = rot;
            obj.SetActive(true);
        }
        return obj;
    }
}
