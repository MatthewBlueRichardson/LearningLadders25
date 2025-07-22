using LearningLadders.EventSystem;
using UnityEngine;
using UnityEngine.UI;
public class ReputationScript : MonoBehaviour
{
    [SerializeField] private VoidEvent onGameOverEvent;

    public float currentRep;
    public float maxRep;
    public Image repBar;

    void Start()
    {
        currentRep = maxRep;
    }

    void Update()
    {
        
    }
    public void IncreaseReputation(float repRestore)
    {
        currentRep += repRestore;
        currentRep = Mathf.Clamp(currentRep, 0f, maxRep);
        repBar.fillAmount = currentRep / maxRep;
    }

    public void DamageReputation(float repDamage)
    {
        currentRep -= repDamage;
        currentRep = Mathf.Clamp(currentRep, 0f, maxRep);
        repBar.fillAmount = currentRep / maxRep;

        if (currentRep <= 0f)
        {
            onGameOverEvent.Invoke(new Empty());
        }
    }
}
