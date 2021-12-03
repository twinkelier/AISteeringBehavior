using System.Collections.Generic;
using UnityEngine;

namespace Steering
{
    [RequireComponent(typeof(Steering))]
    public class SimpleBrain : MonoBehaviour
    {
        // supported behaviors
        public enum BehaviorEnum { Keyboard, SeekClickPoint, Seek, Flee, Pursue, Evade, Wander, FollowPath, Hide, NotSet }

        [Header("Manual")]
        [SerializeField]
        private BehaviorEnum _behavior; // the requested behavior
        [SerializeField]
        private GameObject _target; // the target we are working with

        private Steering _steering;

        public SimpleBrain()
        {
            _behavior = BehaviorEnum.NotSet;
            _target = null;
        }

        private void Start()
        {
            // try to get the target and steering target
            if (_behavior == BehaviorEnum.Keyboard || _behavior == BehaviorEnum.SeekClickPoint)
            {
                _target = null;
            }
            else
            {
                if (_target == null)
                {
                    _target = GameObject.Find("Player");
                }
                if (_target == null)
                {
                    _target = GameObject.Find("Target");
                }
            }

            print(_behavior.ToString());

            // get steering
            _steering = GetComponent<Steering>();

            List<IBehavior> behaviors = new List<IBehavior>();
            switch (_behavior)
            {
                case BehaviorEnum.Keyboard:
                    behaviors.Add(new Keyboard());
                    _steering.SetBehaviors(behaviors, _behavior.ToString());
                    break;
                case BehaviorEnum.SeekClickPoint:
                    break;
                case BehaviorEnum.Seek:
                    break;
                case BehaviorEnum.Flee:
                    break;
                case BehaviorEnum.Pursue:
                    break;
                case BehaviorEnum.Evade:
                    break;
                case BehaviorEnum.Wander:
                    break;
                case BehaviorEnum.FollowPath:
                    break;
                case BehaviorEnum.Hide:
                    break;
                case BehaviorEnum.NotSet:
                    break;

                default:
                    Debug.LogError($"Behavior of tye {_behavior} is not implemented yet!");
                    break;
            }
        }
    }
}
