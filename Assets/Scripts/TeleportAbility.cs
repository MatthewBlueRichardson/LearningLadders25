using UnityEngine;
using System.Collections;
using LearningLadders.EventSystem;
using LearningLadders.Audio;
using UnityEngine.InputSystem;

public class TeleportAbility : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClipSOEvent onPlaySfxEvent;
    [SerializeField] private AudioClipSO teleport;

    [Header("Cooldown")]
    [SerializeField] private float cooldown;

    [Header("Player")]
    [SerializeField] private GameObject player;

    [Header("Events")]
    [SerializeField] private FloatEvent onTeleportEvent;
    [SerializeField] private ParticleSystem preTelePS;
    [SerializeField] private ParticleSystem postTelePS;

    [Tooltip("Needs to be the same as the duration of the particle effect")]
    [SerializeField] private float teleportDelay = 0.3f;

    [SerializeField] private Animator playerAnimator;

    private bool onCooldown = false;
    private InputSystem_Actions controls;

    private void Awake()
    {
        controls = new InputSystem_Actions();
        controls.Player.Teleport.performed += ctx => Teleport();
    }

    public void OnEnable()
    {
        controls.Player.Enable();
    }

    public void OnDisable()
    {
        controls.Player.Disable();
    }

    private void Teleport()
    {
        if (onCooldown) return;
        StartCoroutine(StartTeleport());
        
    }

    IEnumerator StartTeleport()
    {
        onPlaySfxEvent.Invoke(teleport);
        preTelePS.Play();
        playerAnimator.SetTrigger("Shrink");
        //Teleport
        yield return new WaitForSeconds(teleportDelay);
        onTeleportEvent.Invoke(cooldown);
        postTelePS.Play();
        playerAnimator.SetTrigger("Grow");
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }
}
