using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatWindow : MonoBehaviour
{
    private string _currentLine;
    private int _currentCharacter;
    private float _currentDelay = 0f;
    private bool _inProgress;

    public GameObject Object;
    public TMPro.TextMeshProUGUI Text;

    public bool InProgress => _inProgress;

    private void Update()
    {
        if (!_inProgress)
            return;

        _currentDelay += Time.deltaTime;
        if (_currentDelay > ChatSystem.Instance.CharacterDelay)
        {
            _currentDelay = 0f;

            if (_currentCharacter >= _currentLine.Length)
            {
                _inProgress = false;
            }
            else
            {
                _currentCharacter++;
                Text.text = _currentLine.Substring(0, _currentCharacter);
            }
        }
    }

    public void StartLine(string line)
    {
        Text.text = string.Empty;
        _currentCharacter = -1;
        _currentLine = line;
        _inProgress = true;
    }

    public void ForceShow()
    {
        Text.text = _currentLine;
        _inProgress = false;
    }

    public void Show()
    {
        Object.SetActive(true);
    }

    public void Hide()
    {
        Object.SetActive(false);
    }
}
