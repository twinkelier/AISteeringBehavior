using UnityEngine;

namespace SimpleSteering
{
    public class SimpleSeekClickPoint : MonoBehaviour
    {
        [Header("Steering Settings")]
        [SerializeField]
        private SimpleSteeringSettings _steeringSettings; // steering settings of this npc

        private Vector3 _position = Vector3.zero; // current position
        private Vector3 _positionTarget = Vector3.zero; // target position
        private Vector3 _velocity = Vector3.zero; // current velocity
        private Vector3 _velocityDesired = Vector3.zero; // desired velocity
        private Vector3 _steering = Vector3.zero; // steering force

        private void Start()
        {
            _position = transform.position;
            _positionTarget = _position;
        }

        private void Update()
        {
            // SEEK CLICK POINT SPECIFIC: determine target position with mouse position on click
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
                {
                    _positionTarget = hit.point;
                    _positionTarget.y = _position.y;
                }
            }
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
            Support.DrawSolidDisc(_positionTarget, .25f, Color.cyan);
            Support.DrawLabel(transform.position, name, Color.black);
        }
    }
}
