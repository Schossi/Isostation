using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public GameObject Player;
    public GameObject PressButtonText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Action"))
        {
            Player.SetActive(true);
            StoryCoordinator.RepairCharacter(Player.transform.Find("Character").gameObject);
            PressButtonText.SetActive(false);
            Destroy(gameObject);
        }
    }
}
