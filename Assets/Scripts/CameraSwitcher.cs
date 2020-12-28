using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Switch();
    }

    public void Switch()
    {
        if (_animator == null)
            return;

        else if (_animator.GetBool("switch") == false)
        _animator.SetBool("switch", true);

        else if (_animator.GetBool("switch") == true)
            _animator.SetBool("switch", false);
    }
}
