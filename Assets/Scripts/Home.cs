using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour {

    Material backgound;
    Vector2 offset;
    public float xVelocity, yVelocity;

	// Use this for initialization
	void Start () {
        backgound = GetComponent<Renderer>().material;
        offset = new Vector2(xVelocity, yVelocity);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown)
        {
            SceneManager.LoadScene(1);
        }
        backgound.mainTextureOffset += offset * Time.deltaTime;
	}
}
