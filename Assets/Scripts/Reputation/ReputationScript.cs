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
    
    public float currentRep;
    public float maxRep;
    public Image repBar;
    public TMP_Text repText;

    private bool canBeDamaged;

    void Start()
    {
        canBeDamaged = true;
        currentRep = maxRep;
        repText.text = currentRep.ToString();
    }

    void Update()
    {
        
    }
    public void IncreaseReputation(float repRestore)
    {
        currentRep += repRestore;
        currentRep = Mathf.Clamp(currentRep, 0f, maxRep);
        repBar.fillAmount = currentRep / maxRep;
        repText.text = currentRep.ToString();
        sfxEvent.Invoke(repUpSFX);
    }

    public void DamageReputation(float repDamage)
    {
        if (!canBeDamaged) return;
        StartCoroutine(GracePeriod());
        currentRep -= repDamage;
        currentRep = Mathf.Clamp(currentRep, 0f, maxRep);
        repBar.fillAmount = currentRep / maxRep;
        repText.text = currentRep.ToString();
        sfxEvent.Invoke(repDownSFX);

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
}
