using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Steering
{
    using BehaviorList = List<IBehavior>;

    public class Steering : MonoBehaviour
    {
        [Header("Steering Settings")]
        [SerializeField]
        private SteeringSettings _steeringSettings; // steering settings for all behaviors
        [SerializeField]
        private string _labelText; // name of this steering object shown in the game scene

        private Vector3 _position = Vector3.zero; // current position
        private Vector3 _velocity = Vector3.zero; // current velocity
        private Vector3 _steering = Vector3.zero; // steering force
        private BehaviorList _behaviors = new BehaviorList(); // all behaviors for this steering object

        private void Start()
        {
            _position = transform.position;
        }

        private void FixedUpdate()
        {
            // STEERING GENERAL: calculate steering force
            _steering = Vector3.zero;
            foreach (IBehavior behavior in _behaviors)
            {
                _steering += behavior.CalculateSteeringForce(Time.fixedDeltaTime, new BehaviorContext(_position, _velocity, _steeringSettings));
            }

            _steering.y = 0.0f;

            // STEERING GENERAL: clamp steering force to maximum steering force and apply mass
            _steering = Vector3.ClampMagnitude(_steering, _steeringSettings.MaxSteeringForce);
            _steering /= _steeringSettings.Mass;

            // STEERING GENERAL: update velocity with steering force, and update position
            _velocity = Vector3.ClampMagnitude(_velocity + _steering, _steeringSettings.MaxSpeed);
            _position += _velocity * Time.fixedDeltaTime;

            // update object with new position and rotation 
            transform.position = _position;
            transform.LookAt(_position + Time.fixedDeltaTime * _velocity);
        }

        public void SetBehaviors(BehaviorList behaviors, string label = "")
        {
            // remember the new settings

            _labelText = label;
            _behaviors = behaviors;

            // start all behaviors
            foreach (IBehavior behavior in _behaviors)
            {
                behavior.Start(new BehaviorContext(_position, _velocity, _steeringSettings));
            }
        }

        private void OnDrawGizmos()
        {
            Support.DrawRay(transform.position, _velocity, Color.red);
            Support.DrawLabel(transform.position, _labelText, Color.black);

            // allow all behaviors to draw feedback as well
            foreach (IBehavior behavior in _behaviors)
            {
                behavior.OnDrawGizmos(new BehaviorContext(_position, _velocity, _steeringSettings));
            }
        }
    }
}
