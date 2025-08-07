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

        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, mousePos - Camera.main.ScreenToViewportPoint(mousePos), Mathf.Infinity);

        if (hit.transform.tag == "GameOver" || hit.transform.tag == "Stackable" || hit.transform.tag == "PartOfPlatform") return;

        player.transform.position = worldPos;

        onPlaySfxEvent.Invoke(teleport);
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }
}
