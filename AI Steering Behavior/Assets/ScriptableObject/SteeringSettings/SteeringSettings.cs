using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering
{
    [CreateAssetMenu(fileName = "SteeringSettings", menuName = "Settings/SteeringSettings")]
    public class SteeringSettings : ScriptableObject
    {
        [Header("Steering Settings")]
        [SerializeField]
        private float _mass = 70.0f; //mass in kg
        [SerializeField]
        private float _maxDesiredVelocity = 3.0f; // max desired velocity in m/s
        [SerializeField]
        private float _maxSteeringForce = 3.0f; // max steering force in m/s
        [SerializeField]
        private float _maxSpeed = 3.0f; // max vehicle speed in m/s

        [Header("Arrival")]
        [SerializeField]
        private float _arriveDistance = 1f;

        public float Mass => _mass;
        public float MaxDesiredVelocity => _maxDesiredVelocity;
        public float MaxSteeringForce => _maxSteeringForce;
        public float MaxSpeed => _maxSpeed;
        public float ArriveDistance => _arriveDistance;
    }
}
