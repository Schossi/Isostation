using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float Speed;
    public Animator CharacterAnimator;
    public bool LookLeft = false;

    private bool _isInside;
    private float _velocity;

    private float? _target = null;
    private Action _onArrival = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float direction = 0f;

        if (_target.HasValue)
        {
            if (_target.Value > transform.position.x)
            {
                direction = 1f;
            }
            else if (_target.Value < transform.position.x)
            {
                direction = -1f;
            }
            else
            {
                _onArrival?.Invoke();
                _target = null;
                _onArrival = null;
            }
        }

        _velocity = direction * Time.deltaTime * (_isInside ? Speed : Speed / 2f);

        if (_target.HasValue)
        {
            if (_velocity > Math.Abs(transform.position.x - _target.Value))
                _velocity = _target.Value - transform.position.x;
        }

        transform.position = new Vector3(transform.position.x + _velocity, transform.position.y, transform.position.z);

        CharacterAnimator.transform.localScale = new Vector3(LookLeft ? -1f : 1f, 1f);

        CharacterAnimator.SetFloat("WalkSpeed", _isInside ? 2f : 1f);
        CharacterAnimator.SetBool("IsWalking", Mathf.Abs(_velocity) > 0f);
        CharacterAnimator.SetBool("IsOutside", !_isInside);
    }

    public void MoveTo(float x, Action onArrival)
    {
        _target = x;
        _onArrival = onArrival;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Constants.StationLayer)
        {
            _isInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Constants.StationLayer)
        {
            _isInside = false;
        }
    }
}
