using UnityEngine;

namespace SimpleSteering
{
    public enum PetStates
    {
        Following,
        Pursue
    }

    public class PetFollowingAndAttack : MonoBehaviour
    {
        [Header("Steering Settings")]
        [SerializeField]
        private SimpleSteeringSettings _steeringSettings; // steering settings of this npc

        [Header("")]
        [SerializeField]
        private Transform _playerTransform;
        
        private Vector3 _position = Vector3.zero; // current position
        private Vector3 _positionTarget = Vector3.zero; // target position
        private Vector3 _velocity = Vector3.zero; // current velocity
        private Vector3 _velocityDesired = Vector3.zero; // desired velocity
        private Vector3 _steering = Vector3.zero; // steering force

        private Transform _enemyTransform;

        [SerializeField]
        private PetStates _state = PetStates.Following;

        private void Start()
        {
            _position = transform.position;
            _positionTarget = _position;


            _enemyTransform = FindObjectOfType<EnemyTag>().GetComponent<Transform>();
        }

        private void Update()
        {
            ActOnState();
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

        /// <summary>
        /// Acts on the state its given
        /// </summary>
        private void ActOnState()
        {
            if (_enemyTransform || _playerTransform != null)
            {
                switch (_state)
                {
                    case PetStates.Following:

                        // checks if the distance between the enemy is less than 5 and checks if the distance between the player is less than 3
                        if (Vector3.Distance(transform.position, _enemyTransform.position) < 5 && Vector3.Distance(transform.position, _playerTransform.position) < 3f)
                        {
                            _state = PetStates.Pursue;
                        }

                        // sets the position target to the player transform
                        _positionTarget = _playerTransform.position;
                        break;

                    case PetStates.Pursue:

                        // checks if the distance between the player is greater than 10
                        if (Vector3.Distance(transform.position, _playerTransform.position) > 10)
                        {
                            _state = PetStates.Following;
                        }

                        // sets the position target to the enemy transform
                        _positionTarget = _enemyTransform.position;
                        break;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Support.DrawRay(transform.position, _velocity, Color.green);
            Support.DrawRay(transform.position, _velocityDesired, Color.yellow);
            Support.DrawLabel(transform.position, name, Color.black);
        }
    }
}