using CaveExploration;
using System.Collections;
using UnityEngine;

namespace CaveExploration
{
    public enum ThrowType
    {
        Default,
        Fire,
        Water,
        Earth,
        Plant,
        Item1,
        Item2,
        Item3,
        Item4
    }

    [RequireComponent(typeof(ThrowLight))]
    public class ThrowEquiped : MonoBehaviour
    {
        public GameObject[] Throwables;

        public int[] Capacity;

        private string key;

        private ThrowLight throwLight;

        private bool isEquiped;

        private void Awake()
        {
            throwLight = GetComponent<ThrowLight>();
            throwLight.Throwable = Throwables[ThrowType.Default.GetHashCode()];
            throwLight.Capacity = Capacity[ThrowType.Default.GetHashCode()];
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetAxis("FM") != 0)
            {
                ThrowableThings(ThrowType.Fire);
            }
            //if (Input.GetAxis("WM") != 0)
            //{
            //    ThrowableThings(ThrowType.Water);
            //}
            //if (Input.GetAxis("EM") != 0)
            //{
            //    ThrowableThings(ThrowType.Earth);
            //}
            //if (Input.GetAxis("PM") != 0)
            //{
            //    ThrowableThings(ThrowType.Plant);
            //}
            //if (Input.GetAxis("I1") != 0)
            //{
            //    ThrowableThings(ThrowType.Item1);
            //}
            //if (Input.GetAxis("I2") != 0)
            //{
            //    ThrowableThings(ThrowType.Item2);
            //}
            //if (Input.GetAxis("I3") != 0)
            //{
            //    ThrowableThings(ThrowType.Item3);
            //}
            //if (Input.GetAxis("I4") != 0)
            //{
            //    ThrowableThings(ThrowType.Item4);
            //}
        }

        private void ThrowableThings(ThrowType hash)
        {
            if (isEquiped)
            {
                throwLight.Throwable = Throwables[ThrowType.Default.GetHashCode()];
                throwLight.Capacity = Capacity[ThrowType.Default.GetHashCode()];
                isEquiped = false;
            }
            else
            {
                throwLight.Throwable = Throwables[hash.GetHashCode()];
                throwLight.Capacity = Capacity[hash.GetHashCode()];
                isEquiped = true;
            }

        }
    }
}