using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float JumpForce;
    private Rigidbody rigidBody;
    private Vector3 MovementDirecton;

    private RaycastHit hit;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float RayLenght;
    private bool CanJump;

    [SerializeField] private Transform InitPoint;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        CanJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if(MovementDirecton != Vector3.zero)
        {
            rigidBody.AddForce(MovementDirecton * MovementSpeed);
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, RayLenght, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            CanJump = true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * RayLenght, Color.white);
        }
    }
    public void OnForceMovement(InputAction.CallbackContext context)
    {
        MovementDirecton = context.ReadValue<Vector3>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (CanJump)
        {
            rigidBody.AddForce(Vector3.up * JumpForce);
            CanJump = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "YLimit")
        {
            rigidBody.velocity = Vector3.zero;
            transform.position = InitPoint.position;
        }
    }
}