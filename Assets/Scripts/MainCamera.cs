using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MainCamera : MonoBehaviour
{
    private static MainCamera _instance;
    public static MainCamera Instance => _instance;

    public static Vector3 Position
    {
        get
        {
            return _instance.transform.position;
        }
        set
        {
            _instance.transform.position = value;
        }
    }
    public static float Size
    {
        get
        {
            return _instance._camera.orthographicSize;
        }
        set
        {
            _instance._camera.orthographicSize = value;
        }
    }

    private Camera _camera;

    private List<CameraZone> _currentZones = new List<CameraZone>();

    public MainCamera()
    {
        _instance = this;
    }

    public void AddZone(CameraZone zone)
    {
        _currentZones.Add(zone);

        if (_currentZones.Count > 0)
            _currentZones.Last().ResetMove();
    }
    public void RemoveZone(CameraZone zone)
    {
        _currentZones.Remove(zone);

        if (_currentZones.Count > 0)
            _currentZones.Last().ResetMove();
    }

    // Use this for initialization
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentZones.Count > 0)
            _currentZones.Last().MoveCamera();
    }
}
