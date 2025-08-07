using UnityEngine;
using System.Collections;
using LearningLadders.EventSystem;
using LearningLadders.Audio;
using UnityEngine.InputSystem;

public class TeleportAbility : MonoBehaviour
{
    [SerializeField] private AudioClipSOEvent onPlaySfxEvent;
    [SerializeField] private float cooldown;
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
        player.transform.position = worldPos;

        //onPlaySfxEvent.Invoke();
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }
}
