using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerMovement : MonoBehaviour
{
    [BoxGroup("Elements")]
    public FloatingJoystick joystick;


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
        #if UNITY_EDITOR || UNITY_STANDALONE
        horizontalVal = Input.GetAxis("Horizontal");
        #elif UNITY_IOS || UNITY_ANDROID
        horizontalVal = joystick.Horizontal;
        #endif

        float moveBy = horizontalVal * movementSpeed;
        rigidBody.velocity = new Vector2(moveBy, 0);
    }
}
