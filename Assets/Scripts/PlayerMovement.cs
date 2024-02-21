using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Controls")]
    [SerializeField] private float movespeed;
    [SerializeField] private float jumpPower;

    [Header("Thresholds")]
    [SerializeField] private GameObject leftThreshold;
    [SerializeField] private GameObject rightThreshold;

    [Header("Movement Controls")]
    [SerializeField] private KeyCode leftKey;
    [SerializeField] private KeyCode rightKey;
    [SerializeField] private KeyCode jumpKey;

    [Header("Ground Colliding")]
    [SerializeField] private bool _grounded;


    #region Components
    private Rigidbody2D _rb;
    private Animator _an;
    #endregion

    #region Variables
    private float _minX;
    private float _maxX;
    
    #endregion



    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _an = GetComponent<Animator>();

        _minX = leftThreshold.transform.position.x;
        _maxX = rightThreshold.transform.position.x;

    }

    // Start is called before the first frame update
    void Start()
    {
                
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float move = Input.GetAxisRaw("Horizontal") * Time.fixedDeltaTime * movespeed;

        if (move != 0f)
        {
            Debug.Log("Receiving input");
        }

        if (Input.GetKey(jumpKey) && _grounded)
        {
            Debug.Log("Performed a jump");
            _rb.AddForce(new Vector3(0, jumpPower, 0));
            _grounded = false;
        }


        float yVel = _rb.velocity.y;
        _rb.velocity = new Vector3(move, yVel, 0f);
        if (transform.position.x < _minX)
        {
            Vector3 newVector = transform.position;
            newVector.x = _minX;
            transform.position = newVector;
        }
        else if (_rb.position.x > _maxX)
        {
            Vector3 newVector = transform.position;
            newVector.x = _maxX;
            transform.position = newVector;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.gameObject.layer == 6)
        {
            Debug.Log("Collided with Ground");
            _grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("Stopped colliding Ground");
        if (collision.collider.gameObject.layer == 6)
        {
            _grounded = false;
        }
    }


}
