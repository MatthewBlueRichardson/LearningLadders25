using UnityEngine;
using UnityEngine.UI;
public class ReputationScript : MonoBehaviour
{
    public float currentRep;
    public float maxRep;
    public Image repBar;

    void Start()
    {
        currentRep = maxRep;
    }

    void Update()
    {
        repBar.fillAmount = Mathf.Clamp(currentRep / maxRep, 0, 1);
    }
}
