using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    private Rigidbody2D _rigidbody;
    private int _currentJumpsCount;


    public LayerMask GroundLayer;
    public GameObject GroundPoint;
    public GameObject FrontPoint;
    public float JumpPower;
    public float Speed;
    public int JumpsAvailable;

	// Use this for initialization
	void Start () {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        bool isGrounded = Physics2D.OverlapCircle(GroundPoint.transform.position, 0.1f, GroundLayer);

        if (isGrounded && _rigidbody.velocity.y < 0)
        {
            _currentJumpsCount = 0;
        }

        if (Input.GetButtonDown("Jump") || Input.touchCount > 0)
        {
            if(_currentJumpsCount < JumpsAvailable)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, JumpPower);
                //_rigidbody.AddForce(new Vector2(0, JumpPower));
                _currentJumpsCount++;
            }
        }

        _rigidbody.velocity = new Vector2(Speed, _rigidbody.velocity.y);
    }
}
