using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 direction;
    public float forwardSpeed;
    public float MaxSpeed = 40f;

    private int desiredLane = 1;//0:left, 1:middle, 2:right
    public float laneDistance = 2.5f;//The distance between tow lanes
    public float jumpForce;
    public float Gravity = -20;

    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public Animator animator;
    private bool isSliding = false;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    private void Update()
    {
        //tap to play
        if (!PlayerManager.isGameStarted)
            return;

        if (animator != null) 
        {
            animator.SetBool("isGameStarted", true);
        }

        //increase speed
        if (PlayerManager.gameOver == false)
        {
            if (forwardSpeed < MaxSpeed)
            {
                forwardSpeed += 0.5f * Time.deltaTime;
            }

        }
        

        direction.z = forwardSpeed;
        direction.y += Gravity*Time.deltaTime;

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.15f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);
        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || SwipeMAnager.swipeUp)
            {
                Jump();
            }
        }

        if (SwipeMAnager.swipeDown && !isSliding)
        {
            StartCoroutine(Slide());
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || SwipeMAnager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || SwipeMAnager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }


        Vector3 targetPosition = transform.position.z *transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        //transform.position = targetPosition;
        transform.position =Vector3.Lerp(transform.position, targetPosition, 70 * Time.deltaTime);
        controller.center = controller.center;
    }


    private void FixedUpdate()
    {
        //tap to play
        if (!PlayerManager.isGameStarted)
            return;
        controller.Move(direction * Time.fixedDeltaTime);
    }


    private void Jump()
    {
        direction.y = jumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "obstacle")
        {
            FindObjectOfType<AudioManager>().PlaySound("GameOver");


            PlayerManager.gameOver = true;
            //  Destroy(gameObject);
            Time.timeScale = 0;
            forwardSpeed = MaxSpeed = 0;
            

            //int lastRunScore = int.Parse(scoreScript.scoreText.text.ToString());
            //PlayerPrefs.SetInt("lastRunScore", lastRunScore);
            //Time.timeScale = 1;
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("isSliding", true);
        controller.center = new Vector3(0, -0.3f, 0);
        controller.height = 0.5f;

        yield return new WaitForSeconds(1.3f);
        controller.center = new Vector3(0, 0, 0);
        controller.height = 2;

        animator.SetBool("isSliding", false);
        isSliding = false;
    }
}
