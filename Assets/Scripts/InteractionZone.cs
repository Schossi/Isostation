using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionZone : MonoBehaviour
{
    public string Description;
    public InteractionType InteractionType;

    protected Player _player;
    protected bool _isActive;

    public bool IsCurrentlyActive => _isActive;

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void Activate(Player player)
    {
        _player = player;
        _isActive = true;
        StoryCoordinator.Instance.InteractionStarted(InteractionType);
    }

    public virtual void Deactivate()
    {
        _player = null;
        _isActive = false;
        Destroy(gameObject);
        StoryCoordinator.Instance.InteractionFinished(InteractionType);
    }
}
