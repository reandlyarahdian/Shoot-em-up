using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private ObjectPooling pool;
    [SerializeField] private float speed;
    [SerializeField] private float removalTime;
    private float removalTimer;

    List<GameObject> ObjUsed = new List<GameObject>();
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            mouseDir.z = 0;
            mouseDir = mouseDir.normalized;
            Fire(mouseDir);
        }

        removalTimer += Time.deltaTime;
        if (removalTimer >= removalTime)
        {
            removalTimer = 0;
            if (ObjUsed.Count > 0)
            {
                GameObject obj = ObjUsed[0];
                pool.ObjReturn(obj);
                ObjUsed.Remove(obj);
            }
        }
    }

    void Fire(Vector3 mouseDir)
    {
        GameObject obj = pool.ObjSpawn(transform.position, transform.rotation);
        if (obj) ObjUsed.Add(obj);

        obj.transform.position += mouseDir;
        obj.GetComponent<Rigidbody2D>().velocity = mouseDir * speed;
    }
}