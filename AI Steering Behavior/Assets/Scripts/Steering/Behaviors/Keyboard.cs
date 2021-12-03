using UnityEngine;

namespace Steering
{
    public class Keyboard : Behavior
    {
        public override Vector3 CalculateSteeringForce(float dt, BehaviorContext context)
        {
            Vector3 requestedDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            if (requestedDirection != Vector3.zero)
                PositionTarget = context.Position + requestedDirection.normalized * context.Settings.MaxDesiredVelocity;
            else
                PositionTarget = context.Position;

            DesiredVelocity = (PositionTarget - context.Position).normalized * context.Settings.MaxDesiredVelocity;
            return DesiredVelocity - context.Velocity;
        }

        public override void OnDrawGizmos(BehaviorContext context)
        {
            base.OnDrawGizmos(context);
        }

        public override void Start(BehaviorContext context)
        {
            base.Start(context);
        }
    }
}