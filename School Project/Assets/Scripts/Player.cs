using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{

    private BoxCollider2D boxCollider;
    public Animator animator;
    private Vector3 moveDelta;
    private RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        //MoveDelta reset
        moveDelta = new Vector3(x,y,0);

        transform.localScale = Vector3.one;

        if (moveDelta.x > 0)
            animator.SetTrigger("TriggerRight");
        else if (moveDelta.x < 0)
            animator.SetTrigger("TriggerLeft");
        else if (moveDelta.y > 0)
            animator.SetTrigger("TriggerUp");
        else if (moveDelta.y < 0)
            animator.SetTrigger("TriggerDown");
        else
        {
            animator.SetTrigger("NoMovement");
            animator.ResetTrigger("TriggerRight");
            animator.ResetTrigger("TriggerLeft");
            animator.ResetTrigger("TriggerDown");
            animator.ResetTrigger("TriggerUp");
        }

        //collision detection y axis
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Characters", "Blocking"));
        if (hit.collider == null)
        {
            //Movement
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }
        
        //collision detection x axis
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Characters", "Blocking"));
        if (hit.collider == null)
        {
            //Movement
            transform.Translate(moveDelta.x * Time.deltaTime,0, 0);
        }
    }

    
}
