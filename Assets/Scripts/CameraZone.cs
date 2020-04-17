using UnityEngine;
using System.Collections;

public class CameraZone : MonoBehaviour
{
    public float Size;

    private float _factor = 0f;

    private Vector3 _startPosition;
    private float _startSize;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetMove()
    {
        _startPosition = MainCamera.Position;
        _startSize = MainCamera.Size;

        _factor = 0f;
    }

    public void MoveCamera()
    {
        _factor = Mathf.Clamp(_factor + Time.deltaTime * 2f, 0f, 1f);

        MainCamera.Position = Vector3.Slerp(_startPosition, transform.position, _factor);
        MainCamera.Size = Mathf.Lerp(_startSize, Size, _factor);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Constants.PlayerLayer)
        {
            MainCamera.Instance.AddZone(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == Constants.PlayerLayer)
        {
            MainCamera.Instance.RemoveZone(this);
        }
    }
}
