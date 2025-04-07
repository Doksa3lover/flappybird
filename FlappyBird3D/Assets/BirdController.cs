using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class BirdController : MonoBehaviour
{
    public float flapForce = 10f;
    public Animator animator;


    private Rigidbody rb;
    private Vector2 moveInput;
    public float moveForce = 100f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        ///    rb = GetComponent<Rigidbody>();
         ///   if (animator == null)
                animator = GetComponent<Animator>();
       
    }

    IEnumerator Start()
    {
        GetComponent<Rigidbody>().useGravity = false;
        yield return new WaitForSeconds(2f);
        GetComponent<Rigidbody>().useGravity = true;
    }


    // This method MUST match the Input Action name exactly: MoveVertical
    public void OnMoveVertical(InputValue value)
    {
        moveInput.y = value.Get<float>();
        ///    Debug.Log("Vertical input: " + moveInput.y);
      ////  moveInput.y = value.Get<float>();

        // Only act if player is pressing W
        if (moveInput.y > 0f)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z); // reset vertical
            rb.AddForce(Vector3.up * flapForce, ForceMode.Impulse);

            animator.SetBool("IsFlying", true); // Trigger flying animation
        }
        else
        {
            animator.SetBool("IsFlying", false); // Trigger landing animation
        }
    }

    // This method MUST match the Input Action name exactly: MoveHorizontal
    public void OnMoveHorizontal(InputValue value)
    {
        moveInput.x = value.Get<float>();
    ///    Debug.Log("Horizontal input: " + moveInput.x);
    }

    private void FixedUpdate()
    {
        Vector3 force = new Vector3(moveInput.x, moveInput.y, 0f);
        if (force != Vector3.zero)
        {
        ///    Debug.Log("Applying force: " + force);
        }
        rb.AddForce(force * moveForce);
    }

    //*********************************************************************************************************************
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Game Over!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    //*********************************************************************************************************************
    private void LateUpdate()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}