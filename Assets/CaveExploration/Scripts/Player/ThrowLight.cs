using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

namespace CaveExploration
{
	/// <summary>
	/// Handles the players ability to throw light.
	/// </summary>
	public class ThrowLight : MonoBehaviour
	{
		/// <summary>
		/// The number of lights held by the plyer at game start.
		/// </summary>
		public int Capacity { get; set; }

		/// <summary>
		/// The throwable prefab.
		/// </summary>
		public GameObject Throwable { get; set; }

		/// <summary>
		/// The force that light is thrown.
		/// </summary>
		public float Force = 5f;

		/// <summary>
		/// Speech options to show when there are no more lights to be thrown.
		/// </summary>
		public string[] SpeechOnEmpty;

		public float CoolDown;

		private CharacterSpeech speech;
		private int currentThrowableCount;
		private GameObject currentThrowable;
		private Vector3 dir;
		private bool CanThrow = true;
		private bool IsThrow;
		public float AnimWait;
		private MCAnimation animation;
		private float isCool = 1f;
		public GameObject outPlace;

        void Start ()
		{
            Capacity = 3;
            currentThrowableCount = Capacity;

            if (!Throwable) {
				Debug.LogError ("No throwable set, disabling script");
				enabled = false;
			}

			var speechObj = GameObject.FindGameObjectWithTag ("Speech");
			
			if (speechObj) {
				speech = speechObj.GetComponent<CharacterSpeech> ();
			}

			animation = GetComponent<MCAnimation> ();	
		}
	
		void Update ()
		{
            currentThrowableCount = Capacity;

            if (IsThrow) {
				return;
			}

			//if (IntroductorySpeech.instance.InProgress)
			//	return;
		
			if (currentThrowable != null && !currentThrowable.activeSelf) {
				currentThrowable = null;
			}

			if (currentThrowable != null)
				return;


            var canThrow = Throwable.name == "Melee" ? true : CanThrow;
            isCool = Throwable.name == "Melee" ? 0.5f : CoolDown;
            Force = Throwable.name == "Melee" ? 0.0f : 5f;

            if (Input.GetMouseButtonDown(0)) {

				if (currentThrowableCount-- <= 0) {
					if (speech && HasSpeechOptions ()) {
						speech.Speak (SpeechOnEmpty [Random.Range (0, SpeechOnEmpty.Length)]);
					}
					return;
				}

				var sp = Camera.main.WorldToScreenPoint (transform.position);
				dir = (Input.mousePosition - sp).normalized;

                if (canThrow) {
					StartCoroutine(throwableCooldown());
				}
            }

        }

		private bool HasSpeechOptions ()
		{
			return SpeechOnEmpty != null && SpeechOnEmpty.Length > 0;
		}

		IEnumerator throwableCooldown()
		{
			CanThrow = false;

			IsThrow = true;

            if (Force > 1)
            {
                animation.isCast = true;

                yield return new WaitForSeconds(AnimWait);

                currentThrowable = ObjectManager.instance.GetObject(Throwable.name, outPlace.transform.position);

                currentThrowable.GetComponent<Rigidbody2D>().AddForce(dir * Force);
            }
            else
            {
                animation.isAttacking = true;

                currentThrowable = ObjectManager.instance.GetObject(Throwable.name, outPlace.transform.position);

                currentThrowable.GetComponent<Rigidbody2D>().AddForce(dir * Force);

                yield return new WaitForSeconds(AnimWait);
            }


            animation.isCast = false;
            animation.isAttacking = false;

            yield return new WaitForSeconds(isCool);

            IsThrow = false;

            CanThrow = true;

			yield break;
        }
	}
}
