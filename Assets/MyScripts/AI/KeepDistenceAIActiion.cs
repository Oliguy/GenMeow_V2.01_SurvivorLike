using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Tools;
using UnityEngine.Serialization;

namespace MoreMountains.TopDownEngine
{
    /// <summary>
    /// Requires a CharacterMovement ability. Makes the character move up to the specified MinimumDistance in the direction of the target. 
    /// </summary>
    [AddComponentMenu("TopDown Engine/Character/AI/Actions/AIActionKeepDistence2D")]
    //[RequireComponent(typeof(CharacterMovement))]
    public class KeepDistenceAIActiion : AIAction
    {
        /// the minimum distance from the target this Character can reach on the x axis.
        [FormerlySerializedAs("MinimumDistance")]
        [Tooltip("the minimum distance from the target this Character can reach on the x axis.")]
        public float minDistance = 1.2f;
        protected float _minBuffDistance;

        protected Vector2 _direction;
        protected CharacterMovement _characterMovement;
        protected int _numberOfJumps = 0;

        /// <summary>
        /// On init we grab our CharacterMovement ability
        /// </summary>
        public override void Initialization()
        {
            if (!ShouldInitialize) return;
            base.Initialization();
            _characterMovement = this.gameObject.GetComponentInParent<Character>()?.FindAbility<CharacterMovement>();
            _minBuffDistance = minDistance * 1.2f;
        }

        /// <summary>
        /// On PerformAction we move
        /// </summary>
        public override void PerformAction()
        {
            Move();
        }

        /// <summary>
        /// Moves the character towards the target if needed
        /// </summary>
        protected virtual void Move()
        {
            if (_brain.Target == null)
            {
                return;
            }
            float distance = Vector3.Distance(this.transform.position, _brain.Target.position);
            _direction = (_brain.Target.position - this.transform.position).normalized;
            if (distance< minDistance)
            {

                _characterMovement.SetMovement( - _direction);
            }
            else if(distance > _minBuffDistance)
            {
                _characterMovement.SetMovement(_direction);
            }
            else
            {
                _characterMovement.SetMovement(Vector2.zero);
            }

        }

        /// <summary>
        /// On exit state we stop our movement
        /// </summary>
        public override void OnExitState()
        {
            base.OnExitState();

            _characterMovement?.SetHorizontalMovement(0f);
            _characterMovement?.SetVerticalMovement(0f);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, minDistance);
        }
    }
}