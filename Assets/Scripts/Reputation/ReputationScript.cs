using LearningLadders.EventSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using LearningLadders.Audio;
public class ReputationScript : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private VoidEvent onGameOverEvent;

    [Header("Reputation Related")]
    [SerializeField] private float gracePeriod;
    [SerializeField] private AudioClipSOEvent sfxEvent;
    [SerializeField] private AudioClipSO repDownSFX;
    [SerializeField] private AudioClipSO repUpSFX;

    [SerializeField] private Animator repAnimator;

    public float currentRep;
    public float maxRep;
    public Image repBar;
    public TMP_Text repText;

    [Header("Killzone")]
    [Range(0f, 1f)]
    public float minDamage = 0.01f;
    [Range(0f, 1f)]
    public float maxDamage = 0.1f;
    public float damageIncrement;

    private bool canBeDamaged;
    private bool inKillzone;
    private float currentDamage;

    void Start()
    {
        canBeDamaged = true;
        currentRep = maxRep;
        repText.text = currentRep.ToString();
        currentDamage = minDamage;
    }

    void FixedUpdate()
    {
        if (inKillzone == true)
        {
            currentRep -= currentDamage;
            currentRep = Mathf.Clamp(currentRep, 0f, maxRep);
            repBar.fillAmount = currentRep / maxRep;
            repText.text = currentRep.ToString("F0");
            if(currentRep <= 0)
            {
                currentRep = 0;
                onGameOverEvent.Invoke(new Empty());
            }
        }
    }
    public void IncreaseReputation(float repRestore)
    {
        currentRep += repRestore;
        currentRep = Mathf.Clamp(currentRep, 0f, maxRep);
        repBar.fillAmount = currentRep / maxRep;
        repText.text = currentRep.ToString("F0");
        sfxEvent.Invoke(repUpSFX);
        repAnimator.SetTrigger("RepIncrease");
    }

    public void DamageReputation(float repDamage)
    {
        if (!canBeDamaged) return;
        StartCoroutine(GracePeriod());
        currentRep -= repDamage;
        currentRep = Mathf.Clamp(currentRep, 0f, maxRep);
        repBar.fillAmount = currentRep / maxRep;
        repText.text = currentRep.ToString("F0");
        sfxEvent.Invoke(repDownSFX);
        repAnimator.SetTrigger("RepDecrease");

        if (currentRep <= 0f)
        {
            onGameOverEvent.Invoke(new Empty());
        }
    }

    private IEnumerator GracePeriod()
    {
        canBeDamaged = false;
        yield return new WaitForSeconds(gracePeriod);
        canBeDamaged = true;
    }

    public void EnterKillzone(bool inZone)
    {
        inKillzone = inZone;
        repAnimator.SetTrigger("InZone");
    }

    public void ExitKillzone(bool inZone)
    {
        inKillzone = inZone;
        repAnimator.SetTrigger("OutOfZone");
    }

    public void IncreaseKillZoneDamage()
    {
        currentDamage += damageIncrement;
        currentDamage = Mathf.Clamp(currentDamage, minDamage, maxDamage);

        Debug.Log("KILLZONE DAMAGE HAS INCREASED TO: " + currentDamage);
    }
}
