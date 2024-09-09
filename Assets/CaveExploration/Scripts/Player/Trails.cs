using System;
using UnityEngine;

namespace CaveExploration
{
    public class Trails : MonoBehaviour
    {
        private TrailRenderer trail;

        private void Start()
        {
            trail = GetComponent<TrailRenderer>();
            trail.enabled = true;
        }

        public void TrailAct(bool active)
        {
            trail.emitting = active;
        }
    }
}
