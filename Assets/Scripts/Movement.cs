using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody2D _Rigidbody2D;
    private Animator _Animator;
    private CapsuleCollider2D _CapsuleCollider2D;

    private enum PlayerState { Idle, Run, Jump, Fall, Crouch, Hurt};
    private PlayerState playerstate = PlayerState.Idle;

    [SerializeField] private float _Speed = 5f;
    [SerializeField] private float _JumpForce = 20f;
    [SerializeField] private float _HurtForce = 10f;
    [SerializeField] private LayerMask _LayerMask;

    // Start is called before the first frame update
    void Start()
    {
        _Rigidbody2D = GetComponent<Rigidbody2D>();
        _Animator = GetComponent<Animator>();
        _CapsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerstate != PlayerState.Hurt)
        {
            InputManager();
        }
        AnimController();
        _Animator.SetInteger("State",(int)playerstate);

    }

    private void InputManager()
    {
        //Move Left
        if (Input.GetAxis("Horizontal") < 0)
        {
            _Rigidbody2D.velocity = new Vector2(-_Speed, _Rigidbody2D.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }

        //Move Right
        if (Input.GetAxis("Horizontal") > 0)
        {
            _Rigidbody2D.velocity = new Vector2(_Speed, _Rigidbody2D.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

        //Jump
        if ((Input.GetAxis("Jump") > 0) && _CapsuleCollider2D.IsTouchingLayers())
        {
            _Rigidbody2D.velocity = new Vector2(_Rigidbody2D.velocity.x, _JumpForce);
            playerstate = PlayerState.Jump;
        }

        //Crouch
        if(Input.GetAxis("Vertical") < 0 )
        {
            playerstate = PlayerState.Crouch;
        }
    }

    private void AnimController()
    {
        //Jump
       if(playerstate == PlayerState.Jump)
        {
            if(Mathf.Abs(_Rigidbody2D.velocity.y) < 0.5f)
            {
                playerstate = PlayerState.Fall;
            }
        }

       //Falling
       else if(playerstate == PlayerState.Fall)
        {
            if(_CapsuleCollider2D.IsTouchingLayers())
            {
                playerstate = PlayerState.Idle;
            }
        }

       else if(playerstate == PlayerState.Hurt)
        {
            if(Mathf.Abs(_Rigidbody2D.velocity.x) < 0.1f)
            {
                playerstate = PlayerState.Idle;
            }
        }

       //Running
       else if(Mathf.Abs(_Rigidbody2D.velocity.x) > 2f)
        {
            playerstate = PlayerState.Run;
        }

       //Idle
       else
        {
            playerstate = PlayerState.Idle;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemies")
        {
            if(playerstate == PlayerState.Fall)
            {
                Destroy(collision.gameObject);
            }

            else
            {
                playerstate = PlayerState.Hurt;
                if (collision.gameObject.transform.position.x > transform.position.x)
                {
                    _Rigidbody2D.velocity = new Vector2(-_HurtForce, transform.position.y);
                }
                else
                {
                    _Rigidbody2D.velocity = new Vector2(_HurtForce, transform.position.y);
                }
            }
        }
    }
}
