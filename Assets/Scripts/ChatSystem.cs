using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatSystem : MonoBehaviour
{
    private static ChatSystem _instance;
    public static ChatSystem Instance => _instance;

    public ChatWindow Me;
    public ChatWindow Other;

    public float CharacterDelay;

    private bool _isMe;
    public ChatWindow ActiveWindow => _isMe ? Me : Other;
    public ChatWindow InactiveWindow => !_isMe ? Me : Other;
    public bool InProgress => ActiveWindow.InProgress;

    public ChatSystem()
    {
        _instance = this;
    }

    private void Start()
    {
        Me.Hide();
        Other.Hide();
    }

    public void StartLine(string line)
    {
        _isMe = line.StartsWith(".");

        if (_isMe)
        {
            line = line.Substring(1, line.Length - 1);
        }

        ActiveWindow.Show();
        InactiveWindow.Hide();

        ActiveWindow.StartLine(line);
    }

    public void ForceShow()
    {
        ActiveWindow.ForceShow();
    }

    public void Finish()
    {
        Me.Hide();
        Other.Hide();
    }
}
