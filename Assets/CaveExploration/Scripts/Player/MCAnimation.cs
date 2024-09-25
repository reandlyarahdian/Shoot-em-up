using System.Collections;
using UnityEngine;

namespace CaveExploration
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(Animator))]
    public class MCAnimation : MonoBehaviour
    {
        private int walkingHash = Animator.StringToHash("walkSpeed");
        private int attackBool = Animator.StringToHash("isAttack");
        private int castBool = Animator.StringToHash("isCast");
        private int dyingBool = Animator.StringToHash("isDying");

        private Animator _animator;
        private Player _player;

        public bool isAttacking;
        public bool isCast;
        public bool isDying;
        private bool isSpawning = true;

        /// <summary>
        /// Gets a value indicating whether this instance is spawning.
        /// </summary>
        /// <value><c>true</c> if this instance is spawning; otherwise, <c>false</c>.</value>
        public bool IsSpawning { get { return isSpawning; } }

        private bool eventRaised = false;

        /// <summary>
        /// Sets spawning as finished.
        /// </summary>
        public void FinishedSpawning()
        {
            isSpawning = false;

            if (!eventRaised)
            {
                Events.instance.Raise(new PlayerSpawnedEvent(transform));
                eventRaised = true;
            }
        }

        void Awake()
        {
            _player = GetComponent<Player>();
            _animator = GetComponent<Animator>();
        }

        void OnEnable()
        {
            ResetAnimation();
            isDying = false;
            isAttacking = false;
            isCast = false;
            isSpawning = true;
            eventRaised = false;
        }

        private void ResetAnimation()
        {
            _animator.SetFloat(walkingHash, 0);
            _animator.SetBool(dyingBool, false);
            _animator.SetBool(castBool, false);
            _animator.SetBool(attackBool, false);
        }

        void LateUpdate()
        {
            if (isSpawning)
            {
                return;
            }
            ResetAnimation();

            HandleGroundedJetAnimation();

            if (isAttacking)
                HandleAttackAnimation();
            if (isCast)
                HandleCastAnimation();
            if(isDying)
                HandleDyingAnimation();
        }

        private void HandleGroundedJetAnimation()
        {
            var moveSpeed = _player.MoveSpeed;

            _animator.SetFloat(walkingHash, moveSpeed);
        }

        private void HandleAttackAnimation()
        {
            _animator.SetBool(attackBool, true);
        }

        private void HandleCastAnimation()
        {
            _animator.SetBool(castBool, true);
        }

        private void HandleDyingAnimation()
        {
            _animator.SetBool(dyingBool, true);
        }
    }
}