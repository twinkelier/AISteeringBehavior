using UnityEngine;

namespace SimpleSteering
{
    public class BasicKeyboard : MonoBehaviour
    {
        [Header("Steering Settings")]
        [SerializeField]
        private float _maxSpeed = 1.0f;

        [Header("Steering RunTime")]
        [SerializeField]
        private Vector3 _position = Vector3.zero;
        [SerializeField]
        private Vector3 _velocity = Vector3.zero;

        private void Start()
        {
            _position = transform.position;
        }

        private void FixedUpdate()
        {
            Vector3 requestedDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            _velocity = requestedDirection.normalized * _maxSpeed;
            _position = _position + _velocity * Time.fixedDeltaTime;

            transform.position = _position;
            transform.LookAt(_position + _velocity.normalized);
        }

        private void OnDrawGizmos()
        {
            Support.DrawRay(transform.position, _velocity, Color.red);
            Support.DrawLabel(transform.position, name, Color.red);
        }
    }
}
