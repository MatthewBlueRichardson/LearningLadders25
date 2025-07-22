using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RepDamageText : MonoBehaviour
{
    public float lifetime = 0f;

    [SerializeField] private TMP_Text repText;

    void Start()
    {

    }

    public void SetDamageText(float repDamage)
    {
        repText.text = repDamage.ToString();
        Destroy(gameObject, lifetime);
    }
}

