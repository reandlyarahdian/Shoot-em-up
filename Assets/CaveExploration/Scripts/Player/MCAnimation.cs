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

        private bool eventRaised = false;

        public void FinishedSpawning()
        {
            Events.instance.Raise(new PlayerSpawnedEvent(transform));
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