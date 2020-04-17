using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class StoryCoordinator : MonoBehaviour
{
    private static StoryCoordinator _instance;
    public static StoryCoordinator Instance => _instance;

    public NPC NPC;
    public Player Player;

    public ParticleSystem DrillParticles;
    
    public GameObject Pot;
    public GameObject Cutting;
    public GameObject Dishes;
    public GameObject Knife;

    public InteractionZone Greeting;
    public InteractionZone WorkConversation;
    public InteractionZone StewConversation;
    public InteractionZone DinnerConversation;

    public InteractionZone TurnOnDrill;
    public InteractionZone TurnOffDrill;

    public CameraZone EndingZone;

    public StoryCoordinator()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Dishes.SetActive(false);

        WorkConversation.gameObject.SetActive(false);
        StewConversation.gameObject.SetActive(false);
        DinnerConversation.gameObject.SetActive(false);

        TurnOnDrill.gameObject.SetActive(false);
        TurnOffDrill.gameObject.SetActive(false);

        NPC.gameObject.SetActive(true);
        NPC.CharacterAnimator.SetBool("Work", true);
        NPC.LookLeft = false;

        RepairCharacter(NPC.CharacterAnimator.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteractionStarted(InteractionType interaction)
    {
        switch (interaction)
        {
            case InteractionType.DinnerConversation:
                Player.CharacterAnimator.SetBool("Sit", true);
                Player.ForceLeft = true;
                Player.transform.position = new Vector3(5.8f, Player.transform.position.y, Player.transform.position.z);
                break;
        }
    }

    public void InteractionFinished(InteractionType interaction)
    {
        switch (interaction)
        {
            case InteractionType.Greeting:
                TurnOnDrill.gameObject.SetActive(true);
                break;
            case InteractionType.TurnOnDrill:
                DrillParticles.Play();
                NPC.LookLeft = true;
                NPC.CharacterAnimator.SetBool("Work", false);
                NPC.MoveTo(-15.5f, () => WorkConversation.gameObject.SetActive(true));
                Cutting.SetActive(false);
                break;
            case InteractionType.WorkConversation:
                NPC.LookLeft = false;
                NPC.MoveTo(-1.5f, () =>
                {
                    NPC.LookLeft = true;
                    StewConversation.gameObject.SetActive(true);
                });
                break;
            case InteractionType.StewConversation:
                TurnOffDrill.gameObject.SetActive(true);
                break;
            case InteractionType.TurnOffDrill:
                DrillParticles.Stop();
                NPC.LookLeft = false;
                Pot.SetActive(false);
                Dishes.SetActive(true);
                NPC.MoveTo(3f, () =>
                {
                    DinnerConversation.gameObject.SetActive(true);
                    NPC.CharacterAnimator.SetBool("Sit", true);
                });
                break;
            case InteractionType.DinnerConversation:
                Knife.SetActive(true);
                NPC.CharacterAnimator.SetBool("Kill", true);
                Player.CharacterAnimator.SetBool("Die", true);
                StartCoroutine(moveToEnd());
                break;
        }
    }

    public static void RepairCharacter(GameObject character)
    {
        StoryCoordinator.Instance.StartCoroutine(repairCharacter(character));
    }
    private static IEnumerator repairCharacter(GameObject character)
    {
        yield return 0;

        foreach (var skin in character.GetComponentsInChildren<SpriteSkin>())
        {
            skin.enabled = false;
        }

        yield return 0;

        foreach (var skin in character.GetComponentsInChildren<SpriteSkin>())
        {
            skin.enabled = true;
        }
    }

    private IEnumerator moveToEnd()
    {
        yield return new WaitForSeconds(2.5f);
        MainCamera.Instance.AddZone(EndingZone);
    }
}
