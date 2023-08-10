using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    private readonly int WalkRight = Animator.StringToHash(nameof(WalkRight));
    
    private Rigidbody2D _rigidBody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector3 _initialPosition;
    private float _inputHorizontal;
    private bool _isJumping;
    private float _walkSpeed = 3f;
    private float _jumpVelocity = 6f;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _initialPosition = transform.position;
    }

    private void OnEnable()
    {
        Death.playerDied += ResetOnDeath;
    }

    private void OnDisable()
    {
        Death.playerDied -= ResetOnDeath;
    }

    private void Update()
    {
        float nextX = 0f;
        float nextY;

        _inputHorizontal = Input.GetAxis("Horizontal");

        if (_inputHorizontal != 0)
        {
            ChangeAnimation();
            nextX = _inputHorizontal * _walkSpeed;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && _isJumping == false)
        {
            nextY = _jumpVelocity;
        }
        else
        {
            nextY = _rigidBody.velocity.y;
        }

        _rigidBody.velocity = new Vector2(nextX, nextY);
        AdjustFallPhysics();
    }

    private void AdjustFallPhysics() 
    {
        float fallMultiplier = 2.5f;
        float lowJumpMultiplier = 2f;

        if (_rigidBody.velocity.y < 0)
        {
            _rigidBody.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
        else if (_rigidBody.velocity.y > 0 && Input.GetKey(KeyCode.Space) == false)
        {
            _rigidBody.velocity += Vector2.up * Physics.gravity.y * lowJumpMultiplier * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Platform>())
        {
            _isJumping = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Platform>())
        {
            _isJumping = true;
        }
    }

    private void ChangeAnimation()
    {
        if (_inputHorizontal > 0)
        {
            _animator.SetBool(WalkRight, true);
        }
        else if (_inputHorizontal < 0)
        {
            _animator.SetBool(WalkRight, false);
        }
    }

    private void ResetOnDeath() 
    {
        transform.position = _initialPosition;
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        Color originalColor = _spriteRenderer.color;
        Color blink = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);
        var waitTime = new WaitForSecondsRealtime(0.3f);

        for (int i = 0; i < 5; i++)
        {
            _spriteRenderer.material.color = blink;
            yield return waitTime;
            _spriteRenderer.material.color = originalColor;
            yield return waitTime;
        }
    }
}
