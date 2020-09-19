using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CoroutineJump : MonoBehaviour
{
    [SerializeField] float jumpForce = 200f;
    [SerializeField] float jumpDelay = 2f;
    Rigidbody _rigidbody;

    Coroutine _coroutine;
    bool flipFlop = true;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        
        _coroutine = StartCoroutine(Jump(jumpDelay));
        //StartCoroutine("Jump");
        
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (flipFlop)
            {
                StopCoroutine(_coroutine);
                //StopCoroutine("Jump");
            }
            else
            {
                _coroutine = StartCoroutine(Jump(jumpDelay));
                //StartCoroutine("Jump");
            }
            flipFlop = !flipFlop;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator Jump(float delay)
    {
        while (true)
        {
            _rigidbody.AddForce(Vector3.up * jumpForce);
            yield return new WaitForSeconds(delay);              
        }
    }

}
