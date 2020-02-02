using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private static Movement _instance;

    [SerializeField]
    private bool _canMove;
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
    [SerializeField, Range(0,4f)]
    private float _bodySize;
    [SerializeField]
    private bool _canJump;
    [SerializeField]
    private bool _canDoubleJump;
    [SerializeField]
    private bool _doubleJumpUsed = false;
    [SerializeField]
    private bool _onWall;
    private bool _onWallRight;
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
    public static Movement Instance { get => _instance; set => _instance = value; }
    public bool CanDoubleJump { get => _canDoubleJump; set => _canDoubleJump = value; }
    public bool CanWallJump { get => _canWallJump; set => _canWallJump = value; }
    public bool CanMove { get => _canMove; set => _canMove = value; }

    public LayerMask nonItemLayers;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(_instance.gameObject);
            _instance = this;
        }
    }

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
        if (_canMove)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) // space, up or W
            {
                if (!_onWall)
                {
                    if (!_canDoubleJump)
                    {
                        if (_canJump && _grounded)
                        {
                            _playerAnimations.SetBool("Jump", true);
                            AudioManager.Instance.Play("Jump");
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

                            AudioManager.Instance.Play("Jump");
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
                            AudioManager.Instance.Play("Jump");
                            _rb.AddForce(new Vector2(0.5f * (_jumpPower * 0.75f), 1 * _jumpPower) * 10, ForceMode2D.Impulse);
                            _onWall = false;
                        }
                        else
                        {
                            _playerAnimations.SetBool("WallJump", true);
                            AudioManager.Instance.Play("Jump");
                            _rb.AddForce(new Vector2(-0.5f * (_jumpPower * 0.75f), 1 * _jumpPower) * 10, ForceMode2D.Impulse);
                            _onWall = false;
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

            if (!_grounded)
            {
                _playerAnimations.SetBool("Jump", true);
            }
            else
            {
                _playerAnimations.SetBool("Jump", false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (_canMove)
        {
            #region Ground Check
            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(_bodySize, -_bodySize, 0), -Vector2.up, 5, nonItemLayers);
            RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(-_bodySize, -_bodySize, 0), -Vector2.up, 5, nonItemLayers);

            if (hit.collider != null)
            {
                float distance = Mathf.Abs(hit.point.y - transform.position.y) - _bodySize;

                if (distance < 0.1f && hit.collider.tag == "Ground")
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

                if (distance < 0.1f && hit2.collider.tag == "Ground")
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
                if (!_grounded)
                {
                    AudioManager.Instance.Play("Land");
                }
                _grounded = true;
            }
            else
            {
                _grounded = false;
            }
            #endregion

            #region Wall Check
            hit = Physics2D.Raycast(transform.position + new Vector3(-_bodySize, -_bodySize, 0), -Vector2.right, 5, nonItemLayers);
            RaycastHit2D hitHigh = Physics2D.Raycast(transform.position + new Vector3(-_bodySize, _bodySize, 0), -Vector2.right, 5, nonItemLayers);


            hit2 = Physics2D.Raycast(transform.position + new Vector3(_bodySize, -_bodySize, 0), Vector2.right, 5, nonItemLayers);
            RaycastHit2D hit2High = Physics2D.Raycast(transform.position + new Vector3(_bodySize, _bodySize, 0), Vector2.right, 5, nonItemLayers);

            if (hit.collider != null)
            {
                float distance = Mathf.Abs(hit.point.x - transform.position.x) - _bodySize;

                if (distance < 0.02f && !hit.collider.isTrigger)
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

                if (distance < 0.02f && !hitHigh.collider.isTrigger)
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

                if (distance < 0.02f && !hit2.collider.isTrigger)
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

                if (distance < 0.02f && !hit2High.collider.isTrigger)
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
                _doubleJumpUsed = true;
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

            if (_grounded && !_onWall)
            {
                _doubleJumpUsed = false;
            }

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
                    if (moveHor > 0.01f || moveHor < -0.01f)
                    {
                        _playerAnimations.SetBool("Walking", true);
                    }
                    else
                    {
                        _playerAnimations.SetBool("Walking", false);
                    }
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
}
