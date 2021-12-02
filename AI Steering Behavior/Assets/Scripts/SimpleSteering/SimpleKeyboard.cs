using UnityEngine;

namespace SimpleSteering
{
    public class SimpleKeyboard : MonoBehaviour
    {
        [Header("Steering Settings")]
        [SerializeField]
        private SteeringSettings _steeringSettings; // steering settings of this npc

        private Vector3 _position = Vector3.zero; // current position
        private Vector3 _positionTarget = Vector3.zero; // target position
        private Vector3 _velocity = Vector3.zero; // current velocity
        private Vector3 _velocityDesired = Vector3.zero; // desired velocity
        private Vector3 _steering = Vector3.zero; // steering force

        private void Start()
        {
            _position = transform.position;
        }

        private void Update()
        {
            // KEYBOARD SPECIFIC: get requested direction from input
            Vector3 requestedDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            // KEYBOARD SPECIFIC: determine target position
            if (requestedDirection != Vector3.zero)
                _positionTarget = _position + requestedDirection.normalized * _steeringSettings.MaxDesiredVelocity;
            else
                _positionTarget = _position;
        }

        private void FixedUpdate()
        {
            _velocityDesired = (_positionTarget - _position).normalized * _steeringSettings.MaxDesiredVelocity;
            Vector3 steeringForce = _velocityDesired - _velocity;

            // STEERING GENERAL: calculate steering force
            _steering = Vector3.zero;
            _steering += steeringForce;

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

        private void OnDrawGizmos()
        {
            Support.DrawRay(transform.position, _velocity, Color.red);
            Support.DrawRay(transform.position, _velocityDesired, Color.blue);
            Support.DrawLabel(transform.position, name, Color.black);
        }
    }
}
