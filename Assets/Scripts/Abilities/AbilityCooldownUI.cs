using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldownUI : MonoBehaviour
{
    [SerializeField] private Image ability1Image;
    [SerializeField] private KeyCode ability1;

    private bool isA1Cooldown = false;
    private float explodeCooldown;


    void Start()
    {
        ability1Image.fillAmount = 0;
    }

    private void Update()
    {
        if (isA1Cooldown == false)
        {
            ability1Image.fillAmount = 0;
        }

        if (isA1Cooldown)
        {
            ability1Image.fillAmount -= 1 / explodeCooldown * Time.deltaTime;

            if(ability1Image.fillAmount <= 0)
            {
                isA1Cooldown = false;
                ability1Image.fillAmount = 0;
            }
        }
    }

    public void Ability1Activated(float cooldown1)
    {
        ability1Image.fillAmount = 1;
        explodeCooldown = cooldown1;
        isA1Cooldown = true;
    }
 
}
