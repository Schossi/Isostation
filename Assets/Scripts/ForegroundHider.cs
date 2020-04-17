using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundHider : MonoBehaviour
{
    public SpriteRenderer Renderer;

    private float _visibility = 1.0f;
    private bool _isInside = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_isInside)
            _visibility -= Time.deltaTime * 2f;
        else
            _visibility += Time.deltaTime * 2f;

        _visibility = Mathf.Clamp(_visibility, 0.0f, 1.0f);

        var color = Renderer.color;
        color.a = _visibility;
        Renderer.color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Constants.PlayerLayer)
        {
            _isInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Constants.PlayerLayer)
        {
            _isInside = false;
        }
    }
}
