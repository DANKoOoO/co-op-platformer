using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Controller : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private CircleCollider2D coli;
    private BoxCollider2D coli1;
    private EdgeCollider2D coli2;

    private enum State { idle, running, jumping, falling, crawling, crawlIdle }
    private State state = State.idle;

    //promenljive u inspektoru
    [SerializeField] private LayerMask ground;
    [SerializeField] private LayerMask edge;
    [SerializeField] private LayerMask otherPlayer;

    [SerializeField] private float speed = 20f;
    [SerializeField] private float speedCrawl = 1f;
    [SerializeField] private float jumpForce = 30f;
    [SerializeField] private int pera = 0;

    private bool canJump;

    public int Pera
    {
        get
        {
            return pera;
        }

        set { }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coli = GetComponent<CircleCollider2D>();
        coli1 = GetComponent<BoxCollider2D>();
        coli2 = GetComponent<EdgeCollider2D>();
        canJump = true;
    }


    private void Update()
    {
        Movement();
        VelocityState();
        anim.SetInteger("state", (int)state);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collectable")
        {
            SoundManager.PlaySound("feather");
            Destroy(collision.gameObject);
            pera += 1;
            FindObjectOfType<NextLvlScript>().DodajPero();
        }
    }

    private void Movement()
    {

        float hDirection = Input.GetAxis("Horizontal2");
        float vDirection = Input.GetAxis("Vertical2");

        //cucanj
        if (hDirection == 0 && vDirection < 0 && coli.IsTouchingLayers(ground))
        {
            state = State.crawlIdle;
        }

        //levo
        else if (hDirection < 0)
        {
            if ((vDirection < 0 && coli.IsTouchingLayers(ground)) || coli2.IsTouchingLayers(edge))
            {
                rb.velocity = new Vector2(-speedCrawl, rb.velocity.y);
                transform.localScale = new Vector2(-1, 1);
                state = State.crawling;
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                transform.localScale = new Vector2(-1, 1);
            }
        }

        //desno
        else if (hDirection > 0)
        {
            if ((vDirection < 0 && coli.IsTouchingLayers(ground)) || coli2.IsTouchingLayers(edge))
            {
                rb.velocity = new Vector2(speedCrawl, rb.velocity.y);
                transform.localScale = new Vector2(1, 1);
                state = State.crawling;
            }
            else
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                transform.localScale = new Vector2(1, 1);
            }
        }

        //skok
        if (Input.GetButtonDown("Jump2") && (coli.IsTouchingLayers(ground) || coli.IsTouchingLayers(otherPlayer)) && !coli.IsTouchingLayers(edge) && !coli2.IsTouchingLayers(edge) && canJump)
        {
            SoundManager.PlaySound("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            state = State.jumping;
            canJump = false;
        }

    }

    private void VelocityState()
    {
        if (state == State.jumping)
        {
            if (rb.velocity.y < 0.1f)
            {
                state = State.falling;
            }
        }

        else if (state == State.falling)
        {
            if (coli.IsTouchingLayers(ground) || coli.IsTouchingLayers(otherPlayer))
            {
                state = State.idle;
                canJump = true;
            }
        }

        else if (state == State.crawling)
        {
            if (Input.GetAxis("Vertical2") == 0 && Input.GetAxis("Horizontal2") == 0 && !coli2.IsTouchingLayers(edge))
            {
                state = State.idle;
            }
            else if (Input.GetAxis("Vertical2") == 0 && Input.GetAxis("Horizontal2") != 0 && !coli2.IsTouchingLayers(edge))
            {
                state = State.running;
            }
        }

        else if (state == State.crawlIdle)
        {
            if (Input.GetAxis("Vertical2") == 0 && Input.GetAxis("Horizontal2") == 0 && !coli2.IsTouchingLayers(edge))
            {
                state = State.idle;
            }
            else if (Input.GetAxis("Vertical2") == 0 && Input.GetAxis("Horizontal2") != 0 && !coli2.IsTouchingLayers(edge))
            {
                state = State.running;
            }
        }

        else if (Mathf.Abs(rb.velocity.x) > 5f)
        {
            //Moving
            state = State.running;
        }
        else
        {
            state = State.idle;
        }

    }
}
