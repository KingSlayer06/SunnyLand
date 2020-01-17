using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diamond : MonoBehaviour
{
    private Animator _Animator;
    private BoxCollider2D _Collider;
    private int _DiamondCount = 0;
    [SerializeField] Text text; 

    private enum State { Gem, Item};
    private State state = State.Gem;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            state = State.Item;
            _Animator.SetInteger("Diamond", (int)state);
            _DiamondCount += 1;
            text.text = _DiamondCount.ToString();
            StartCoroutine(delay());
            
        }
    }
}
