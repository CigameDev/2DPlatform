using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    

    [SerializeField] private ContactFilter2D contactFilter;
    [SerializeField] private float groundDistance = 0.05f;
    [SerializeField] private float wallDistance = 0.2f;
    [SerializeField] private float ceilingDistance = 0.05f;
    
    private CapsuleCollider2D touchingCol;
    private RaycastHit2D[] groundHits = new RaycastHit2D[5];
    private RaycastHit2D[] wallHits = new RaycastHit2D[5];
    private RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    private Animator animator;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    private bool _isGround;
    public bool IsGround
    { 
        get { return _isGround; }
        set 
        { 
            _isGround = value;
            animator.SetBool(AnimationString.IS_GROUND, value);
        }
    }

    private bool _isOnWall;
    public bool IsOnWall
    {
        get { return _isOnWall; }
        set
        {
            _isOnWall = value;
            animator.SetBool(AnimationString.IS_ON_WALL, value);
        }
    }

    private bool _isCeiling;
    public bool IsCeiling
    {
        get { return _isCeiling; }
        set
        {
            _isCeiling = value;
            animator.SetBool(AnimationString.IS_CEILING, value);
        }
    }
    private void Awake()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        IsGround = touchingCol.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, contactFilter, wallHits, wallDistance) >0;
        IsCeiling = touchingCol.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance) >0;
    }
}
