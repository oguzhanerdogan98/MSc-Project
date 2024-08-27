using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro; //Text mesh pro imported for change text.
public class PlayerMovement : MonoBehaviour
{
    public float Speed = 8f;
    public EndlessRoad EndlessRoad; //Referance of endless road class
    public GameObject RestartGamePanel;
    public List<BoxCollider> Checkers; //List of AI obstacle controllers.
    public bool isGameOver = false;
    public int Score;
    public TMP_Text ScoreText;
    public bool canSlide = true;
    public bool isJumping = true;


    private float slideStartX;
    private float slideFinishX;
    private float slideStartY;
    private float slideFinishY;
    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponent<Animator>();
        canSlide = true;
    }
    private void Update()
    {
        if (!isGameOver)
        {
            Score = (int)transform.position.x;
            ScoreText.text = ("Score: " + Score);
        }
        if (!EndlessRoad.isAISelected)
        {
            animator.SetBool("isJumping", !isGrounded);
            Jump();
        }
        MoveStraight();
    }

    private void MoveStraight()
    {
        transform.position += Vector3.right * Speed * Time.deltaTime; //moves in x axis
        if (!EndlessRoad.isAISelected && canSlide && !isJumping) 
            Slide(); //for sliding when player selected 
        else
            AIMove(); // Just for check this method is empty.  
    }

    private void Jump()
    {
        canSlide = false;
        if (Input.GetMouseButtonDown(0))
        {
            slideStartY = Input.mousePosition.y;
        }
        if (Input.GetMouseButtonUp(0))
        {
            slideFinishY = Input.mousePosition.y;
            if(slideStartY + 500f < slideFinishY)
            {
                rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
                isGrounded = false;
                isJumping = true;
            }
            canSlide = true;
        }
    }

    private void Slide()
    {
        if (Input.GetMouseButtonDown(0)) // when we touch the screen gets position
        {
            slideStartX = Input.mousePosition.x;
        }

        if (Input.GetMouseButtonUp(0)) // When we take our finger out of the screen make swipe by taking the difference
        {
            slideFinishX = Input.mousePosition.x;
            if(slideFinishX > slideStartX && transform.position.z > -4f)
            {
                transform.DOMoveZ(transform.position.z - 6f, 0.5f);
            }
            else if(slideStartX > slideFinishX && transform.position.z < 4f)
            {
                transform.DOMoveZ(transform.position.z + 6f, 0.5f);
            }
        }
    }

    public void AIMove()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) //When Model hits the obstacle this method run
        {
            isGameOver = true;
            Speed = 0f;
            RestartGamePanel.SetActive(true);
            Debug.Log("Çarptým");
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            isJumping = false;
        }
    }
}
