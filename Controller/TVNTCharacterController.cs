using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

namespace TVNT
{
    public class TVNTCharacterController : MonoBehaviour
    {

        public static bool flipVerticalInput = false;
        public static bool flipHorizontalInput = false;

        public enum MovementStyle
        {
            DISCRETE_JUMP,
            DISCRETE_WALK,
            CONTINUOS_JUMP,
            CONTINUOS_WALK
        };

        public MovementStyle movementStyle = MovementStyle.DISCRETE_JUMP;
        public LayerMask monsterLayerMask;
        //public int monsterAndBarrierLayerMask = (1 << 14 | 1 << 9);
        public LayerMask barrierLayerMask = (1 << 14 | 1 << 9);
        public Vector3 barrierOffset;
        public LayerMask groundLayerMask;
        public Vector3 groundOffset;
        public Animator myAnimator;
        [HideInInspector]
        public bool idle = true;
        public string idleAnimationName = "";
        public string walkAnimationName = "";
        public string jumpAnimationName = "";
        public string landAnimationName = "";
        public float moveTime = 0.4f;
        public Transform characterBase;
        public float jumpHeight = 0f;
        public float fallSpeed = 10f;

        [HideInInspector]
        public BoxCollider boxCollider;
        protected Rigidbody rb;
        protected int horizontal = 0;
        protected int vertical = 0;
        protected int prevHorizontal = 0;
        protected int prevVertical = 0;
        protected Transform parent = null;
        protected bool onMovingPlatform = false;
        protected bool onSlidingGround = false;
        protected bool skipTurn = false;
        protected GroundCollider parentGroundCollider = null;

        private float originalMoveTime;
        [HideInInspector]
        public bool setFirstParent = true;
        private float characterBaseScaleY;
        private bool jump = false;

        public bool canSmash = false;

        public Transform deathPE;

        public bool activate = false;

        //Health system
        public int lives = 1;
        private bool invincible = false;
        private float invincibleTime = 1.5f;
        private float currentInvincibleTime = 0f;
        public bool flashWhiteOnDamage = false;
        private float displayDamageTime = 0.125f; //In seconds
        public Material whiteMaterial;
        public MeshRenderer[] meshParts;
        public Material[] normalMaterials; //make sure the normal material slot match with the mesh parts array

        //Sound
        public AudioSource myAudioSource = null;
        public AudioClip walkSound = null;
        public AudioClip jumpSound = null;

        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody>();
            boxCollider = GetComponent<BoxCollider>();

            CheckGround();
            originalMoveTime = moveTime;
            characterBaseScaleY = characterBase.localScale.y;

            jump = (movementStyle == MovementStyle.CONTINUOS_JUMP || movementStyle == MovementStyle.DISCRETE_JUMP) ? true : false;
        }

        protected void AttemptMove()
        {
            Transform myParent = transform.parent;
            parent = transform.parent.GetComponent<GroundCollider>().levelTile;
            transform.parent = parent.parent;

            Vector3 start = transform.localPosition;
            if (onMovingPlatform == false)
            {
                start = parent.localPosition + new Vector3(0, PatternSettings.playerYOffset, 0);
            }
            Vector3 end = start + new Vector3(horizontal * PatternSettings.tiledSize, 0f, vertical * PatternSettings.tiledSize);

            if (horizontal > 0)
            {
                transform.localRotation = Quaternion.Euler(new Vector3(0, 90f, 0));
            }
            else if (horizontal < 0)
            {
                transform.localRotation = Quaternion.Euler(new Vector3(0, -90f, 0));
            }

            if (vertical > 0)
            {
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0f, 0));
            }
            else if (vertical < 0)
            {
                transform.localRotation = Quaternion.Euler(new Vector3(0, -180f, 0));
            }

            boxCollider.enabled = false;
            RaycastHit hitInfo;
            if (Physics.Linecast(parent.parent.TransformPoint(start + barrierOffset), parent.parent.TransformPoint(end + barrierOffset), out hitInfo, barrierLayerMask) == false)
            {
                //Debug.DrawLine(parent.parent.TransformPoint(start + barrierOffset), parent.parent.TransformPoint(end + barrierOffset));
                if (OccupyMoveToTile(parent.parent.TransformPoint(end)))
                {
                    idle = false;
                    if (onSlidingGround == false)
                    {
                        if (jump == false)
                        {
                            if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName(walkAnimationName) == false)
                            {
                                myAnimator.SetTrigger("Walk");
                            }
                        }
                        else
                        {
                            myAnimator.SetTrigger("Jump");
                        }
                    }
                    onSlidingGround = false;
                    onMovingPlatform = false;
                    prevHorizontal = horizontal;
                    prevVertical = vertical;

                    if (jump == false)
                    {
                        if (walkSound)
                        {
                            myAudioSource.loop = true;
                            myAudioSource.clip = walkSound;
                            myAudioSource.Play();
                        }
                    }
                    else
                    {
                        if (jumpSound)
                        {
                            myAudioSource.loop = false;
                            myAudioSource.clip = jumpSound;
                            myAudioSource.Play();
                        }
                    }
                    StartCoroutine(SmoothMovement(start, end));
                }
                else
                {
                    transform.parent = myParent;
                    horizontal = 0;
                    vertical = 0;
                    myAnimator.SetTrigger("Idle");
                    transform.position = transform.parent.position + new Vector3(0, PatternSettings.playerYOffset, 0);
                }
            }
            else
            {
                if (canSmash && hitInfo.transform.tag == "Smashable")
                {

                    Debug.Log("SmashMovement!");
                    idle = false;
                    hitInfo.transform.GetComponent<ISmashable>().Smash(0.175f);
                    horizontal = 0;
                    vertical = 0;
                    myAnimator.SetTrigger("Jump");
                    myAnimator.speed = 2;

                    if (jumpSound)
                    {
                        myAudioSource.loop = false;
                        myAudioSource.clip = jumpSound;
                        myAudioSource.Play();
                    }
                    StartCoroutine(SmashMovement(start, end));
                }
                else
                {
                    transform.parent = myParent;
                    horizontal = 0;
                    vertical = 0;
                    myAnimator.SetTrigger("Idle");
                    transform.position = transform.parent.position + new Vector3(0, PatternSettings.playerYOffset, 0);
                }
            }

            boxCollider.enabled = true;
        }

        private IEnumerator SmashMovement(Vector3 start, Vector3 end)
        {
            Vector3 moveDirection = (end - start);
            float t = 0;
            for (t = Time.deltaTime / moveTime; t < 1; t += Time.deltaTime / (moveTime * 0.5f))
            {
                characterBase.localPosition = new Vector3(0, jumpHeight * Mathf.Sin(Mathf.PI * t), 0);
                if (t < 0.5f)
                {
                    transform.localPosition = start + (moveDirection * t);
                }
                else
                {
                    transform.localPosition = start + (moveDirection * (1 - t));
                }
                yield return null;
            }
            characterBase.localPosition = Vector3.zero;
            transform.localPosition = start;
            myAnimator.speed = 1;
            horizontal = 0;
            vertical = 0;
            CheckGround();
        }

        //Called to get that little compression when the player lands
        /**private IEnumerator ScaleDown() {
			float timeToScaleDown = moveTime * 0.5f;
			float _jumpCompression = jumpCompression * (jumpHeight / jumpCompressionBaseHeight);
			for (float t = 0; t < 1; t += Time.deltaTime / timeToScaleDown) {
				if (jumping == false) {
					characterBase.localScale = new Vector3 (characterBase.localScale.x, 
						characterBaseScaleY * (1 - (_jumpCompression * Mathf.Sin (Mathf.PI * t))), 
						characterBase.localScale.z);
				} else {
					t = 1;
					characterBase.localScale = new Vector3(characterBase.localScale.x, characterBaseScaleY, characterBase.localScale.z);
				}
				yield return null;
			}
			characterBase.localScale = new Vector3(characterBase.localScale.x, characterBaseScaleY, characterBase.localScale.z);
		}**/

        private IEnumerator SmoothMovement(Vector3 start, Vector3 end)
        {
            Vector3 moveDirection = (end - start);
            float t = 0;
            for (t = Time.deltaTime / moveTime; t < 1; t += Time.deltaTime / moveTime)
            {
                if (jump)
                {
                    characterBase.localPosition = new Vector3(0, jumpHeight * Mathf.Sin(Mathf.PI * t), 0);
                }
                transform.localPosition = start + (moveDirection * t);
                if (t > 0.5f)
                {
                    if (parentGroundCollider)
                    {
                        parentGroundCollider.occupied = false;
                        parentGroundCollider = null;
                    }
                }
                yield return null;
            }
            if (jump)
            {
                characterBase.localPosition = Vector3.zero;
            }
            if (movementStyle == MovementStyle.CONTINUOS_WALK)
            {
                if (t - 1 < Time.deltaTime / moveTime && t - 1 > 0)
                {
                    transform.localPosition = start + (moveDirection * t);
                }
            }
            else
            {
                transform.localPosition = start + (moveDirection * 1);
            }

            moveTime = originalMoveTime;
            CheckGround();
        }

        public void CheckGround()
        {
            Vector3 start = transform.position;
            Vector3 end = start + new Vector3(0, -1f, 0);

            boxCollider.enabled = false;
            RaycastHit hitInfo;
            if (Physics.Linecast(start + groundOffset, end + groundOffset, out hitInfo, groundLayerMask))
            {
                transform.parent = hitInfo.transform;
                if (setFirstParent)
                {
                    parentGroundCollider = hitInfo.transform.GetComponent<GroundCollider>();
                    parentGroundCollider.occupied = true;
                    setFirstParent = false;
                }
                else
                {
                    if (targetGroundCollider)
                    {
                        parentGroundCollider = targetGroundCollider;
                    }
                }
                idle = true;
                if (hitInfo.transform.tag == "MovingPlatform")
                {
                    onMovingPlatform = true;
                    horizontal = 0;
                    vertical = 0;
                    //Fix in version 1.41
                    myAnimator.SetTrigger("Idle");
                }
                else if (hitInfo.transform.tag == "ChangeDirection")
                {
                    horizontal = prevHorizontal * -1;
                    vertical = prevVertical * -1;
                    skipTurn = true;
                }
                else if (hitInfo.transform.tag == "SlidingPlatform")
                {
                    IDriveableTile slidingTileScript = parentGroundCollider.levelTile.GetComponent<IDriveableTile>();
                    horizontal = slidingTileScript.GetHorizontal();
                    vertical = slidingTileScript.GetVertical();
                    skipTurn = true;
                    myAnimator.SetTrigger("Idle");
                    onSlidingGround = true;
                    moveTime = slidingTileScript.GetMoveSpeed();
                }
                else if (hitInfo.transform.tag == "ButtonTile")
                {
                    parentGroundCollider.levelTile.GetComponent<ISwitchable>().SwitchOn();
                }
            }
            else
            {
                Falling();
            }
            boxCollider.enabled = true;
        }

        private GroundCollider targetGroundCollider = null;

        private bool OccupyMoveToTile(Vector3 end)
        {
            boxCollider.enabled = false;
            RaycastHit hitInfo;
            if (Physics.Linecast(end + groundOffset, end + new Vector3(0, -1f, 0) + groundOffset, out hitInfo, groundLayerMask))
            {
                targetGroundCollider = hitInfo.transform.GetComponent<GroundCollider>();
                if (targetGroundCollider.occupied == false)
                {
                    targetGroundCollider.occupied = true;
                    return true;
                }
                else
                {
                    targetGroundCollider = null;
                    return false;
                }
            }
            targetGroundCollider = null;
            return true;
        }

        private void Falling()
        {
            Vector3 end = transform.localPosition + new Vector3(0, -50f, 0);
            myAnimator.SetTrigger("Idle");
            StartCoroutine(Fall(end));
        }

        private IEnumerator Fall(Vector3 end)
        {
            float sqrRemainingDistance = (transform.localPosition - end).sqrMagnitude;
            Vector3 initialScale = characterBase.localScale;
            while (sqrRemainingDistance > float.Epsilon)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, end, fallSpeed * Time.deltaTime);
                sqrRemainingDistance = (transform.localPosition - end).sqrMagnitude;
                yield return null;
            }
            CharacterDead();
        }

        public void PlayIdleAnimation()
        {
            if (jump == false)
            { //Set the idle animation only if the movement style of the character is walk based; jump transitions into idle by itself
                if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName(idleAnimationName) == false)
                {
                    myAnimator.SetTrigger("Idle");
                }
            }
            if (myAudioSource)
            {
                myAudioSource.Stop();
            }
        }

        void FixedUpdate()
        {
            if (invincible)
            {
                if (currentInvincibleTime > 0)
                {
                    currentInvincibleTime -= Time.deltaTime;
                }
                else
                {
                    invincible = false;
                }
            }
        }

        private IEnumerator DisplayDamage()
        {
            while (invincible)
            {
                if (flashWhiteOnDamage)
                {
                    for (int i = 0; i < meshParts.Length; i++)
                    {
                        meshParts[i].material = whiteMaterial;
                    }
                }
                yield return new WaitForSeconds(displayDamageTime);
                if (flashWhiteOnDamage)
                {
                    for (int i = 0; i < meshParts.Length; i++)
                    {
                        meshParts[i].material = normalMaterials[i];
                    }
                }
                yield return new WaitForSeconds(displayDamageTime * 2);
            }
        }

        public virtual void LifeLost(int currentLives)
        {
            //Turn the character white for a while and then turn them back
            currentInvincibleTime = invincibleTime;
            invincible = true;
            StartCoroutine(DisplayDamage());
        }

        public virtual void CharacterDead()
        {
            if (parentGroundCollider)
            {
                parentGroundCollider.occupied = false;
                parentGroundCollider = null;
            }
            if (targetGroundCollider)
            {
                targetGroundCollider.occupied = false;
                targetGroundCollider = null;
            }
            transform.parent = null;
            Destroy(gameObject);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "DungeonWeapon" && invincible == false)
            {
                lives--;

                if (lives > 0)
                {
                    LifeLost(lives);
                }
                else
                {
                    Transform _pe_Death = (Transform)Instantiate(deathPE, Vector3.zero, Quaternion.identity);
                    _pe_Death.position = new Vector3(transform.position.x, 2f, transform.position.z);
                    ParticleSystem ps = _pe_Death.GetComponent<ParticleSystem>();
                    float timeDelay = ps.startLifetime;
                    _pe_Death.GetComponent<TimedDestroy>().delay = timeDelay;
                    CharacterDead();
                }
            }
        }

    }
}