using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerCantroller : MonoBehaviour
{
    [Header("Moverment")]
    public float movespeed;
    public float jumpPower;
    private Vector2 curMovemebtinput;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform camerContainor;
    public float minXLook;
    public float maxXLook;
    private float camCurXrot;
    public float lookSensitvity;
    private Vector2 mouseDelta;
    public bool canLook = true;

    public Action inventory;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        cameraLopk();
    }

    void Move()
    {
        Vector3 dir = transform.forward * curMovemebtinput.y + transform.right * curMovemebtinput.x;
        dir *= movespeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;

    }

    void cameraLopk()
    {
        camCurXrot += mouseDelta.y * lookSensitvity;
        camCurXrot = Mathf.Clamp(camCurXrot, minXLook, maxXLook);
        camerContainor.localEulerAngles = new Vector3(-camCurXrot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitvity, 0);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovemebtinput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovemebtinput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f),Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f),Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f),Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f),Vector3.down),
        };
        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }
    public void OnInventoryButton(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }
    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
