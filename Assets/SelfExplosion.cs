using LearningLadders.Audio;
using LearningLadders.EventSystem;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelfExplosion : MonoBehaviour
{
    public Rigidbody2D torsoRb;

    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float explosionForce = 500f;
    [SerializeField] private float cooldown = 2f;
    [SerializeField] private LayerMask explosionLayers;
    [SerializeField] private InputSystem_Actions controls;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private AudioClipSOEvent sfxEvent;
    [SerializeField] private AudioClipSO explosionSound;

    private bool onCooldown = false;

    void Awake()
    {
        controls = new InputSystem_Actions();

        controls.Player.Explode.performed += ctx => Explode();
    }

    void OnEnable()
    {
        controls.Player.Enable();
    }

    void OnDisable()
    {
        controls.Player.Disable();
    }

    void Explode()
    {
        if (onCooldown == false)
        {
            //Find all colliders in the explosion radius
            Collider2D[] objects = Physics2D.OverlapCircleAll(torsoRb.transform.position, explosionRadius, explosionLayers);

            foreach (Collider2D obj in objects)
            {
                Rigidbody2D rb = obj.attachedRigidbody;
                if (rb != null && rb != GetComponent<Rigidbody2D>()) //Avoid pushing yourself
                {
                    Vector2 direction = rb.position - (Vector2)transform.position;
                    direction.Normalize();
                    rb.AddForce(direction * explosionForce);
                }
            }
            ps.Play();
            sfxEvent.Invoke(explosionSound);
            StartCoroutine(Cooldown());
        }        
    }

    IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    void OnDrawGizmosSelected()
    {
        //Show explosion radius in editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(torsoRb.transform.position, explosionRadius);
    }
}
