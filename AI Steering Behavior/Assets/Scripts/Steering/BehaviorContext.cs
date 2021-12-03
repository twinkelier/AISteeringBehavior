using UnityEngine;

namespace Steering
{
    public class BehaviorContext
    {
        public Vector3 Position;
        public Vector3 Velocity;
        public SteeringSettings Settings;

        public BehaviorContext(Vector3 position, Vector3 velocity, SteeringSettings settings)
        {
            Settings = settings;
            Position = position;
            Velocity = velocity;
        }
    }
}
