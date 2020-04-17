using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatZone : InteractionZone
{
    [TextArea(10, 20)]
    public string Text;

    private int _currentLine = -1;

    private string[] _lines;

    public override void Start()
    {
        base.Start();

        _lines = Text.Split('\n');
    }

    public override void Update()
    {
        base.Update();

        if (!_isActive)
            return;

        if (Input.GetButtonUp("Action"))
        {
            nextLine();
        }
    }

    public override void Activate(Player player)
    {
        base.Activate(player);

        player.SuspendMovement = true;
        nextLine();
    }

    public override void Deactivate()
    {
        _player.SuspendMovement = false;

        base.Deactivate();
    }

    private void nextLine()
    {
        if (ChatSystem.Instance.InProgress)
        {
            ChatSystem.Instance.ForceShow();
        }
        else
        {
            _currentLine++;

            if (_currentLine >= _lines.Length)
            {
                ChatSystem.Instance.Finish();
                Deactivate();
            }
            else
            {
                ChatSystem.Instance.StartLine(_lines[_currentLine]);
            }
        }
    }
}
