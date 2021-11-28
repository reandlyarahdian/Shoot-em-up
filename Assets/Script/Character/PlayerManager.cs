using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private Shoot shoot;
    private Movement movement;

    [SerializeField] PowerUp[] powerUps;
    [SerializeField] PowerUp[] guns;

    private void Start()
    {
        shoot = GetComponentInChildren<Shoot>();
        movement = GetComponent<Movement>();
    }

    public void GunData(int i)
    {
        shoot.speed = guns[i].MoveSpeed;
    }

    public void PowerData(int i)
    {
        movement.speed = powerUps[i].MoveSpeed;
    }
}
