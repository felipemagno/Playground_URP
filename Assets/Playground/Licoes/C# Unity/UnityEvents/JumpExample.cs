using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpExample : MonoBehaviour
{
    [SerializeField] float jumpForce = 200f;
    Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Jump()
    {
        _rigidbody.AddForce(Vector3.up * jumpForce);
    }
}
