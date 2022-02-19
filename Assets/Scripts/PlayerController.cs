using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    #region SerializedFields
    [SerializeField]
    private Rigidbody character;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private UIManager uiManager;

    [SerializeField]
    float swerveSpeed;

    [SerializeField]
    float maxSwerveAmount = 1f;

    [SerializeField]
    float forwardMovementSpeed;

    [SerializeField]
    Transform deadUI;

    

    #endregion


    private bool isFinished;
    private bool userStardedTheGame=false;
    private bool isLoadingDone;

    public float Speed;

    float lastFrameMousePos;
    float moveFactorX;

    public event Action<GameObject> OnCollectDiamond,OnTookDamage;
    public event Action OnLevelEnded;

    private void Awake()
    {
        uiManager.OnPlayerDied += Die;
        uiManager.OnLoadingDone += LetThePlayerPlay;
    }

    private void LetThePlayerPlay()
    {
        isLoadingDone = true;
    }

    private void Die()
    {
        isFinished = true;
        forwardMovementSpeed = 0;
        animator.SetBool("isDead", true);
        StartCoroutine(WaitForXsecondsAndDisplayDeadUI(3f));
    
    }

    private void Start()
    {
        userStardedTheGame = false;
        isFinished = false;
        isLoadingDone = false;
        deadUI.gameObject.SetActive(false);
        
    }

    private void ToggleRunningAnimation(float value)
    {
        animator.SetFloat("Blend", value);
    }

    private void FixedUpdate()
    {
        
        if (!isFinished && userStardedTheGame)
        {
                MoveForward();
                HandleHorizontalMovement();
        }
       
    }

    private void Update()
    {
        if (isLoadingDone)
        {
            HandleSwipe();
        }
    }

    private void HandleHorizontalMovement()
    {
        float swerveAmount = Time.deltaTime * swerveSpeed * moveFactorX;
        swerveAmount =25f *  Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);


        //transform.Translate(swerveAmount, 0, 0);

        character.AddForce(swerveAmount, 0, 0, ForceMode.Impulse);
        
    }

    private void HandleSwipe()
    {
        if (isFinished)
        {
            moveFactorX = 0f;
        }

        if (!isFinished)
        {

            if (Input.GetMouseButtonDown(0))
            {
                userStardedTheGame = true;
                uiManager.ToggleStartGameText(userStardedTheGame);
                ToggleRunningAnimation(10f);
                lastFrameMousePos = Input.mousePosition.x;

            }

            else if (Input.GetMouseButton(0))
            {

                moveFactorX = Input.mousePosition.x - lastFrameMousePos;
                lastFrameMousePos = Input.mousePosition.x;

            }
            else if (Input.GetMouseButtonUp(0))
            {
                moveFactorX = 0f;
            } 
        }

    }

    private void MoveForward()
    {
        
       character.velocity = new Vector3(0, 0, forwardMovementSpeed);
        
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Diamond"))
        {
            
            OnCollectDiamond?.Invoke(other.gameObject);
            
        }

        if (other.CompareTag("Barrier"))
        {
            animator.SetBool("hitObstacle", true);

          
            OnTookDamage?.Invoke(other.gameObject);
            StartCoroutine(OuchAnimationToRunningDelay());
            
        }

        if (other.CompareTag("EndOfLevel"))
        {
            OnLevelEnded?.Invoke();
            LevelEnded();
        }
    }
    IEnumerator OuchAnimationToRunningDelay()
    {
        
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("hitObstacle", false);

    }

    private void LevelEnded()
    {
        StartCoroutine(OnEndOfLevelRunningDelay());
        
        
    }

    IEnumerator OnEndOfLevelRunningDelay()
    {
        
        yield return new WaitForSeconds(1f);
        animator.SetBool("isLevelEnded", true);
        isFinished = true;
        ToggleRunningAnimation(0f);
    }

    IEnumerator WaitForXsecondsAndDisplayDeadUI(float value)
    {
        yield return new WaitForSeconds(value);
        deadUI.gameObject.SetActive(true);
    }
}
