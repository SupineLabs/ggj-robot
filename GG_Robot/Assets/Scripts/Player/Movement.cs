using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField, Range(0,5)]
    private float _acceleration;
    [SerializeField]
    private float _maxSpeed;
    [SerializeField]
    private float _breakPower;
    [SerializeField, Range(5, 10)]
    private float _jumpPower;
    [SerializeField]
    private bool _grounded = false;
    private bool _groundedRight = false;
    private bool _groundedLeft = false;
    [SerializeField]
    private float _bodySize;
    [SerializeField]
    private bool _canJump;
    [SerializeField]
    private bool _canDoubleJump;
    [SerializeField]
    private bool _doubleJumpUsed = false;
    [SerializeField]
    private bool _onWall;
    [SerializeField]
    private bool _onWallRight;
    [SerializeField]
    private bool _onWallLeft;
    [SerializeField]
    private bool _canWallJump;
    [SerializeField]
    private bool _invertedControls;
    private Rigidbody2D _rb;
    private Animator _playerAnimations;
    private SpriteRenderer _playerSprite;

    private bool _movingRight;
    private bool _movingLeft;
    private bool _jump;
    private bool _doubleJump;
    private bool _wallJump;

    public bool InvertedControls { get => _invertedControls; set => _invertedControls = value; }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnimations = GetComponent<Animator>();
        _playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (!_onWall)
            {
                if (!_canDoubleJump)
                {
                    if (_canJump && _grounded)
                    {
                        _playerAnimations.SetBool("Jump", true);
                        _rb.AddForce(Vector2.up * _jumpPower * 10, ForceMode2D.Impulse);
                    }
                    else
                    {
                        _playerAnimations.SetBool("Jump", false);
                    }
                }
                else
                {
                    if ((_canJump && _grounded) || (_canJump && !_doubleJumpUsed))
                    {
                        if (!_grounded)
                        {
                            _doubleJumpUsed = true;
                        }
                        if (_doubleJumpUsed)
                        {
                            _playerAnimations.SetBool("DoubleJump", true);
                        }
                        else
                        {
                            _playerAnimations.SetBool("Jump", true);
                        }
                        _rb.AddForce(Vector2.up * _jumpPower * 10, ForceMode2D.Impulse);

                    }
                    else
                    {
                        _playerAnimations.SetBool("DoubleJump", false);
                        _playerAnimations.SetBool("Jump", false);
                    }
                }
            }
            else
            {
                _playerAnimations.SetBool("DoubleJump", false);
                _playerAnimations.SetBool("Jump", false);
                if (_canWallJump)
                {
                    if (_onWallRight)
                    {
                        _playerAnimations.SetBool("WallJump", true);
                        _rb.AddForce(new Vector2(0.5f * (_jumpPower * 0.75f), 1 * _jumpPower) * 10, ForceMode2D.Impulse);
                    }
                    else
                    {
                        _playerAnimations.SetBool("WallJump", true);
                        _rb.AddForce(new Vector2(-0.5f * (_jumpPower * 0.75f), 1 * _jumpPower) * 10, ForceMode2D.Impulse);
                    }
                }
                else
                {
                    _playerAnimations.SetBool("WallJump", false);
                }
            }
        }
        else
        {
            _playerAnimations.SetBool("DoubleJump", false);
            _playerAnimations.SetBool("Jump", false);
            _playerAnimations.SetBool("WallJump", false);
        }

        if (_grounded && !_onWall)
        {
            _doubleJumpUsed = false;
        }
    }

    private void FixedUpdate()
    {
        #region Ground Check
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(_bodySize, -_bodySize, 0), -Vector2.up, 5);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(-_bodySize, -_bodySize, 0), -Vector2.up, 5);

        if (hit.collider != null)
        {
            float distance = Mathf.Abs(hit.point.y - transform.position.y) - _bodySize;

            if (distance < 0.02f && hit.collider.tag == "Ground")
            {
                _groundedRight = true;
            }
            else
            {
                _groundedRight = false;
            }
        }
        else
        {
            _groundedRight = false;
        }
        if (hit2.collider != null)
        {
            float distance = Mathf.Abs(hit2.point.y - transform.position.y) - _bodySize;

            if (distance < 0.02f && hit2.collider.tag == "Ground")
            {
                _groundedLeft = true;
            }
            else
            {
                _groundedLeft = false;
            }
        }
        else
        {
            _groundedLeft = false;
        }

        if (_groundedLeft || _groundedRight)
        {
            _grounded = true;
        }
        else
        {
            _grounded = false;
        }
        #endregion

        #region Wall Check
        hit = Physics2D.Raycast(transform.position + new Vector3(-_bodySize, -_bodySize, 0), -Vector2.right, 5);
        RaycastHit2D hitHigh = Physics2D.Raycast(transform.position + new Vector3(-_bodySize, _bodySize, 0), -Vector2.right, 5);


        hit2 = Physics2D.Raycast(transform.position + new Vector3(_bodySize, -_bodySize, 0), Vector2.right, 5);
        RaycastHit2D hit2High = Physics2D.Raycast(transform.position + new Vector3(_bodySize, _bodySize, 0), Vector2.right, 5);

        if (hit.collider != null)
        {
            float distance = Mathf.Abs(hit.point.x - transform.position.x) - _bodySize;

            if (distance < 0.04f)
            {
                _onWallRight = true;
            }
            else
            {
                _onWallRight = false;
            }
        }
        else if (hitHigh.collider != null)
        {
            float distance = Mathf.Abs(hitHigh.point.x - transform.position.x) - _bodySize;

            if (distance < 0.04f)
            {
                _onWallRight = true;
            }
            else
            {
                _onWallRight = false;
            }
        }
        else
        {
            _onWallRight = false;
        }

        if (hit2.collider != null)
        {
            float distance = Mathf.Abs(hit2.point.x - transform.position.x) - _bodySize;

            if (distance < 0.04f)
            {
                _onWallLeft = true;
            }
            else
            {
                _onWallLeft = false;
            }
        }
        if (hit2High.collider != null)
        {
            float distance = Mathf.Abs(hit2High.point.x - transform.position.x) - _bodySize;

            if (distance < 0.04f)
            {
                _onWallLeft = true;
            }
            else
            {
                _onWallLeft = false;
            }
        }
        else
        {
            _onWallLeft = false;
        }

        if ((_onWallLeft || _onWallRight) && !_grounded)
        {
            _onWall = true;
        }
        else
        {
            _onWall = false;
        }

        if (_onWall && _rb.velocity.y < 0)
        {
            _rb.gravityScale = 1f;
        }
        else
        {
            _rb.gravityScale = 7f;
        }
        #endregion

        float moveHor = Input.GetAxis("Horizontal");

        if (_invertedControls)
        {
            moveHor = -moveHor;
        }

        if (moveHor < 0)
        {
            _playerSprite.flipX = true;
        }
        else
        {
            _playerSprite.flipX = false;
        }

        if ((_rb.velocity.x < _maxSpeed && _rb.velocity.x > -_maxSpeed))
        {
            if (((_onWallRight && moveHor > 0) || (_onWallLeft && moveHor < 0) || !_onWall))
            {
                _rb.AddForce(Vector2.right * moveHor * _acceleration, ForceMode2D.Impulse);
                _playerAnimations.SetBool("Walking", true);
            }
            else
            {
                _playerAnimations.SetBool("Walking", false);
            }
        }
        if (moveHor <= 0.01f && moveHor >= -0.01f && (_rb.velocity.x > 0.4f || _rb.velocity.x < 0.4f))
        {
            _rb.AddForce(-new Vector2(_rb.velocity.x, 0) * _breakPower);
        }
    }
}
