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
    private bool _doubleJumpUsed = false;
    [SerializeField]
    private bool _onWall;
    private bool _onWallRight;
    private bool _onWallLeft;
    [SerializeField]
    private bool _canWallJump;
    private Rigidbody2D _rb;

    public LayerMask nonItemLayers;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHor = Input.GetAxis("Horizontal");

        if ((_rb.velocity.x < _maxSpeed && _rb.velocity.x > -_maxSpeed))
        {
            if (((_onWallRight && moveHor > 0) || (_onWallLeft && moveHor < 0) || !_onWall))
            {
                _rb.AddForce(Vector2.right * moveHor * _acceleration, ForceMode2D.Impulse);
            }
        }
        if(moveHor <= 0.01f && moveHor >= -0.01f && (_rb.velocity.x > 0.4f || _rb.velocity.x < 0.4f))
        {
            _rb.AddForce(-new Vector2(_rb.velocity.x, 0) * _breakPower);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (!_onWall)
            {
                if (!_canDoubleJump)
                {
                    if (_canJump && _grounded)
                    {
                        _rb.AddForce(Vector2.up * _jumpPower * 500);
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
                        _rb.AddForce(Vector2.up * _jumpPower * 500);
                    }
                }
            }
            else
            {
                if (_canWallJump)
                {
                    if (_onWallRight)
                    {
                        _rb.AddForce(new Vector2(0.5f * (_jumpPower * 0.75f), 1 * _jumpPower) * 500);
                    }
                    else
                    {
                        _rb.AddForce(new Vector2(-0.5f * (_jumpPower * 0.75f), 1 * _jumpPower) * 500);
                    }
                }
            }
        }

        if (_grounded)
        {
            _doubleJumpUsed = false;
        }

        #region Ground Check
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(_bodySize, -_bodySize, 0), -Vector2.up, 5, nonItemLayers);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(-_bodySize, -_bodySize, 0), -Vector2.up, 5, nonItemLayers);

        if (hit.collider != null)
        {
            float distance = Mathf.Abs(hit.point.y - transform.position.y) - _bodySize;

            if (distance < 0.02f)
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

            if (distance < 0.02f)
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

        if(_groundedLeft || _groundedRight)
        {
            _grounded = true;
        }
        else
        {
            _grounded = false;
        }
        #endregion

        #region Wall Check
        hit = Physics2D.Raycast(transform.position + new Vector3(_bodySize, -_bodySize, 0), -Vector2.right, 5, nonItemLayers);
        RaycastHit2D hitHigh = Physics2D.Raycast(transform.position + new Vector3(_bodySize, _bodySize, 0), -Vector2.right, 5, nonItemLayers);


        hit2 = Physics2D.Raycast(transform.position + new Vector3(-_bodySize, -_bodySize, 0), Vector2.right, 5, nonItemLayers);
        RaycastHit2D hit2High = Physics2D.Raycast(transform.position + new Vector3(-_bodySize, _bodySize, 0), Vector2.right, 5, nonItemLayers);

        if (hit.collider != null)
        {
            float distance = Mathf.Abs(hit.point.x - transform.position.x) - _bodySize;

            if (distance < 0.02f)
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

            if (distance < 0.02f)
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

            if (distance < 0.02f)
            {
                _onWallLeft = true;
            }
            else
            {
                _onWallLeft = false;
            }
        }
        else if (hit2High.collider != null)
        {
            float distance = Mathf.Abs(hit2High.point.x - transform.position.x) - _bodySize;

            if (distance < 0.02f)
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
            _rb.gravityScale = 7;
        }
        #endregion
    }
}
