using LearningLadders.EventSystem;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ReputationScript : MonoBehaviour
{
    [SerializeField] private VoidEvent onGameOverEvent;

    public float currentRep;
    public float maxRep;
    public Image repBar;
    public TMP_Text repText;

    void Start()
    {
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
    }

    public void DamageReputation(float repDamage)
    {
        currentRep -= repDamage;
        currentRep = Mathf.Clamp(currentRep, 0f, maxRep);
        repBar.fillAmount = currentRep / maxRep;
        repText.text = currentRep.ToString();

        if (currentRep <= 0f)
        {
            onGameOverEvent.Invoke(new Empty());
        }
    }
}
