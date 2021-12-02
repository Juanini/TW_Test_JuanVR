using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerMovement : MonoBehaviour
{
    [BoxGroup("Properties")]
    public float movementSpeed;

    private float horizontalVal;
    private Rigidbody2D rigidBody;
    
    void Awake()
    {
        InitComponents();
    }

    void Update()
    {
        Move();
    }

    private void InitComponents()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Move()
    {
        horizontalVal = Input.GetAxis("Horizontal");
        float moveBy = horizontalVal * movementSpeed;
        rigidBody.velocity = new Vector2(moveBy, 0);
    }
}
