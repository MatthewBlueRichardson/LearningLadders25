using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RepDamageText : MonoBehaviour
{
    [SerializeField] private TMP_Text repText;

    public void SetDamageText(float repDamage)
    {
        repText.text = repDamage.ToString();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}

