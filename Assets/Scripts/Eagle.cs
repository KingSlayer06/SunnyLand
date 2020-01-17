using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    private Animator _Animator;
    private BoxCollider2D _Collider;

    private enum State { Alive, Die };
    private State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        _Animator = GetComponent<Animator>();
        _Collider = GetComponent<BoxCollider2D>();
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.4f);
        GameObject.Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
