using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : MonoBehaviour 
{
    [SerializeField] private float _Left;
    [SerializeField] private float _Right;
    [SerializeField] private float _Speed;
    private Rigidbody2D _RigidBody2D;
    private Transform _Transform;

    private bool _FacingLeft = true;
    // Start is called before the first frame update
    void Start()
    {
        _RigidBody2D = GetComponent<Rigidbody2D>();
        _Transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(_Transform.position.x) > _Right)
        {
            _RigidBody2D.velocity = new Vector2(_Speed, _RigidBody2D.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        if(Mathf.Abs(_Transform.position.x) < _Left)
        {
            _RigidBody2D.velocity = new Vector2(_Speed, _RigidBody2D.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
       
    }
}
