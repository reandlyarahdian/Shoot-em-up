using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimeNum : MonoBehaviour
{
    [SerializeField] int n;
    void Start()
    {
        PrimeSum(n);
    }

    private void PrimeSum(int n)
    {
        int c = 0;
        int num = 2;
        int prime = 0;
        while (c != n)
        {
            int count = 0;
            for(int i = 2; i <= Math.Sqrt(num); i++)
            {
                if(num % i == 0)
                {
                    count++;
                    break;
                }
            }
            if(count == 0)
            {
                c++;
                prime += num;
            }
            num++;
        }

        Debug.Log(prime);
    }
}
