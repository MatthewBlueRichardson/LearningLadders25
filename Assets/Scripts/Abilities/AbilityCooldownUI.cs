using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldownUI : MonoBehaviour
{
    [Header("Explosion Ability")]
    [SerializeField] private Image ability1Image;
    [SerializeField] private Animator ability1Animator;
    [SerializeField] private Animator ability1KeyAnimator;

    private bool isA1Cooldown = false;
    private float explodeCooldown;

    [Header("Teleport Ability")]
    [SerializeField] private Image ability2Image;
    [SerializeField] private Animator ability2Animator;
    [SerializeField] private Animator ability2KeyAnimator;

    private bool isA2Cooldown = false;
    private float teleportCooldown;

    void Start()
    {
        ability1Image.fillAmount = 0;
    }

    private void Update()
    {
        UpdateA1CooldownUI();
        UpdateA2CooldownUI();
    }

    public void Ability1Activated(float cooldown1)
    {
        ability1Image.fillAmount = 1;
        explodeCooldown = cooldown1;
        isA1Cooldown = true;

        ability1Animator.SetBool("In", false); // Lower transparency of this ability's UI icon
        ability1KeyAnimator.SetTrigger("Press");
    }

    public void Ability2Activated(float cooldown2)
    {
        ability2Image.fillAmount = 1;
        teleportCooldown = cooldown2;
        isA2Cooldown = true;

        ability2Animator.SetBool("In", false); // Lower transparency of this ability's UI icon
        ability2KeyAnimator.SetTrigger("Press");
    }

    public void UpdateA1CooldownUI()
    {
        if (isA1Cooldown == false)
        {
            ability1Image.fillAmount = 0;
        }

        if (isA1Cooldown)
        {
            ability1Image.fillAmount -= 1 / explodeCooldown * Time.deltaTime;

            if (ability1Image.fillAmount <= 0)
            {
                isA1Cooldown = false;
                ability1Image.fillAmount = 0;

                ability1Animator.SetBool("In", true);
            }
        }
    }

    public void UpdateA2CooldownUI()
    {
        if (isA2Cooldown == false)
        {
            ability2Image.fillAmount = 0;
        }

        if (isA2Cooldown)
        {
            ability2Image.fillAmount -= 1 / teleportCooldown * Time.deltaTime;

            if (ability2Image.fillAmount <= 0)
            {
                isA2Cooldown = false;
                ability2Image.fillAmount = 0;

                ability2Animator.SetBool("In", true);
            }
        }
    }

}
