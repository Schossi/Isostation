using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MinX;
    public float MaxX;

    public float Speed;

    public GameObject ForegroundFlakes;
    public Animator CharacterAnimator;
    public AudioSource WindSource;
    public TMPro.TMP_Text InteractionText;
    public bool SuspendMovement;
    public bool ForceLeft = false;

    private bool _isInside = false;

    private float _velocity = 0f;
    private float _lean = 0f;
    private Transform _sprite;
    private bool _isGoingLeft;
    private InteractionZone _interactionZone;

    // Start is called before the first frame update
    void Start()
    {
        _sprite = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        var lastVelocity = _velocity;

        if (SuspendMovement)
            _velocity = 0f;
        else
            _velocity = Input.GetAxis("Horizontal") * Time.deltaTime * (_isInside ? Speed : Speed / 2f);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x + _velocity, MinX, MaxX), transform.position.y, transform.position.z);

        float leanGoal;
        if (lastVelocity > _velocity)
        {
            leanGoal = 5f;
        }
        else if (lastVelocity < _velocity)
        {
            leanGoal = -5f;
        }
        else
        {
            leanGoal = 0f;
        }

        if (ForceLeft)
            _isGoingLeft = true;
        else if (_velocity > 0f)
            _isGoingLeft = false;
        else if (_velocity < 0f)
            _isGoingLeft = true;

        CharacterAnimator.SetFloat("WalkSpeed", _isInside ? 2f : 1f);

        WindSource.volume = _isInside ? 0.3f : 1.0f;

        _lean = Mathf.MoveTowards(_lean, leanGoal, 150f * Time.deltaTime);

        CharacterAnimator.transform.localScale = new Vector3(_isGoingLeft ? 1f : -1f, 1f);
        //transform.eulerAngles = new Vector3(0f, 0f, _lean);

        CharacterAnimator.SetBool("IsWalking", Mathf.Abs(_velocity) > 0f);
        CharacterAnimator.SetBool("IsOutside", !_isInside);
        //CharacterAnimator.speed = _velocity / Time.deltaTime;

        if (_interactionZone && !_interactionZone.IsCurrentlyActive && Input.GetButtonUp("Action"))
        {
            InteractionText.text = string.Empty;
            _interactionZone.Activate(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Constants.StationLayer)
        {
            _isInside = true;
            ForegroundFlakes.SetActive(false);
        }
        else if (collision.gameObject.layer == Constants.InteractionLayer)
        {
            _interactionZone = collision.gameObject.GetComponent<InteractionZone>();
            InteractionText.text = ">" + _interactionZone.Description;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Constants.StationLayer)
        {
            _isInside = false;
            ForegroundFlakes.SetActive(true);
        }
        else if (collision.gameObject.layer == Constants.InteractionLayer)
        {
            _interactionZone = null;
            InteractionText.text = string.Empty;
        }
    }
}
