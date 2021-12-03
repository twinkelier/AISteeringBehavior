using UnityEngine;

namespace Steering
{
    public abstract class Behavior : IBehavior
    {
        [Header("Behavior Runtime")]
        public Vector3 DesiredVelocity;
        public Vector3 PositionTarget;

        /// <summary>
        /// Allow the behavior to initialize
        /// </summary>
        /// <param name="context">All the context information needed to perform the task at hand</param>
        public virtual void Start(BehaviorContext context)
        {
            PositionTarget = context.Position;
        }

        /// <summary>
        /// Calculate the steering force contributed by this behavior
        /// </summary>
        /// <param name="dt">The delta time for this ste</param>
        /// <param name="context">All the context information needed to perform the task at hand</param>
        /// <returns></returns>
        public abstract Vector3 CalculateSteeringForce(float dt, BehaviorContext context);
        
        public virtual void OnDrawGizmos(BehaviorContext context)
        {
            Support.DrawRay(context.Position, DesiredVelocity, Color.red);
        }
    }
}
