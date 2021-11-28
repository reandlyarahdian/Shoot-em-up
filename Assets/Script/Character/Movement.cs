using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    private bool move;
    Vector3 mousePos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            move = true;
            
        }
        if (move && transform.position != mousePos)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, mousePos, step);
        }
        else
        {
            move = false;
        }
        RotateObj();
    }

    void RotateObj()
    {
        Vector3 MouseFace = Input.mousePosition;
        MouseFace.z = 0;
        Vector3 objFace = Camera.main.WorldToScreenPoint(transform.position);
        MouseFace.x = MouseFace.x - objFace.x;
        MouseFace.y = MouseFace.y - objFace.y;
        float angle = Mathf.Atan2(MouseFace.y, MouseFace.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
