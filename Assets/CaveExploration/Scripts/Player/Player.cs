using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

namespace CaveExploration
{
	/// <summary>
	/// Handles player movement.
	/// </summary>
	[RequireComponent (typeof(Rigidbody2D))]
	[RequireComponent (typeof(MCAnimation))]
	public class Player : MonoBehaviour
	{
		/// <summary>
		/// Determines wether the player is facing right. Used to face the sprite in the movement direction.
		/// </summary>
		public bool IsFacingRight;

		/// <summary>
		/// The maximum walk speed.
		/// </summary>
		public float MaxWalkSpeed = 2f;

		/// <summary>
		/// The maximum run speed.
		/// </summary>
		public float MaxRunSpeed = 4f;

		/// <summary>
		/// The jump force.
		/// </summary>
		public float JumpForce = 500f;

		/// <summary>
		/// Gets or sets a value indicating whether this instance is dead.
		/// </summary>
		/// <value><c>true</c> if this instance is dead; otherwise, <c>false</c>.</value>
		public bool IsDead  { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance has jumped. Used for double jumping.
		/// </summary>
		/// <value><c>true</c> if this instance has jumped; otherwise, <c>false</c>.</value>
		public bool HasJumped { get; set; }

		public bool IsAttack {  get; set; }

		private float moveY;
		private float moveX;
		private float moveSpeed;

		/// <summary>
		/// Gets the current movement speed.
		/// </summary>
		/// <value>The move speed.</value>
		public float MoveSpeed { get { return moveSpeed; } }


		private float maxSpeed;
		private bool isRunning = false;
		private BottomCheck groundCheck;
		private Rigidbody2D rigidbody2d;
		private Jetpack jetpack;
		//private PlayerAnimation _animation;
		private MCAnimation animation;
		private Trails trails;
        private bool CanDash = true;
        private bool IsDash;
        public float DashCoolDown;
		private Vector3 dir;
		private Vector2 move;

        void Awake ()
		{
			//groundCheck = GetComponentInChildren<BottomCheck> ();

			//if (!groundCheck) {
			//	Debug.LogError ("Player requires BottomCheck sript on child, disabling script");
			//	enabled = false;
			//}

			rigidbody2d = GetComponent<Rigidbody2D> ();
			//jetpack = GetComponentInChildren<Jetpack> ();
			//_animation = GetComponent<PlayerAnimation> ();
			animation = GetComponent<MCAnimation> ();
			trails = GetComponentInChildren<Trails> ();
		}

		void OnEnable ()
		{
			IsDead = false;
			isRunning = false;
			moveY = 0f;
			moveX = 0f;
			moveSpeed = 0f;
			rigidbody2d.velocity = Vector2.zero;

			Events.instance.AddListener<PlayerKilledEvent> (OnDead);
		}

		void OnDisable ()
		{
			Events.instance.RemoveListener<PlayerKilledEvent> (OnDead);
		}
		

		private void OnDead (GameEvent e)
		{
			IsDead = true;
		}
	
		// Update is called once per frame
		void FixedUpdate ()
		{
			if (IsDead || animation.IsSpawning) {
				moveX = 0f;
				moveY = 0f;
				moveSpeed = 0f;
				return;
			}
			
			if(IsDash)
				return;

			// Clamp Move.
			moveX = Input.GetAxisRaw ("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");

			move = new Vector2(moveX, moveY);

            if (move != Vector2.zero)
			{
				moveSpeed = (moveSpeed < maxSpeed) ? moveSpeed + Mathf.Abs(move.sqrMagnitude) * 0.05f : maxSpeed;
				Debug.Log(moveSpeed);
			}
			else
			{
				moveSpeed = 0;
			}


			rigidbody2d.velocity = (move * moveSpeed);

			// Flip Sprite.
			if ((moveX > 0 && !IsFacingRight) || (moveX < 0 && IsFacingRight))
                Flip ();

		}

		void Update ()
		{
			//HasJumped = false;

            animation.isDying = IsDead;

            if (animation.IsSpawning)
            {
                IsDead = false;
                return;
            }

            if (IsDead)
				return;

            if (IsDash)
                return;

            //var isGrounded = groundCheck.IsGrounded;

            //// Jump.
            //if (isGrounded && Input.GetKeyDown (KeyCode.Space)) {
            //	rigidbody2d.AddForce (new Vector2 (0, JumpForce));
            //	HasJumped = true;
            //	if (jetpack) {
            //		jetpack.CanJet = false;
            //	}
            //} else if (isGrounded || (jetpack && jetpack.UsingJet)) {
            //	HasJumped = false;
            //} 

            var sp = Camera.main.WorldToScreenPoint(transform.position);
            dir = (Input.mousePosition - sp).normalized;

			maxSpeed = MaxWalkSpeed;

            // Run.
            if (/*isGrounded &&*/ Input.GetKeyDown(KeyCode.LeftShift) && rigidbody2d.velocity != Vector2.zero && CanDash) {
				StartCoroutine(DashCooldown());
            }

			//if ((!isRunning && moveX != 0) || (!isRunning && moveY != 0))
			//{
			//	if (maxSpeed > MaxWalkSpeed)
			//	{
			//		maxSpeed -= 0.02f;
			//	}
			//	else
			//	{
			//		maxSpeed = MaxWalkSpeed;
			//	}
			//}

		}

		private void Flip ()
		{
			IsFacingRight = !IsFacingRight;
			var theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}

        IEnumerator DashCooldown()
        {
            CanDash = false;

            IsDash = true;

            trails.TrailAct(true);

			rigidbody2d.AddForce(move * MaxRunSpeed);

			yield return new WaitForSeconds(.5f);

            IsDash = false;

            trails.TrailAct(false);

            yield return new WaitForSeconds(DashCoolDown);

            CanDash = true;

            yield break;
        }
    }
}
