using System.Runtime.CompilerServices;
using UnityEngine;

public class WindAnimationTriggers : MonoBehaviour
{
    [SerializeField] private Animator windAnimator;
    [SerializeField] private GameObject parent;
    [SerializeField] private float animDelay;
    [SerializeField] private int currentWindTier;

    public void OnWindTierChanged(int windTier)
    {
        currentWindTier = windTier;
        float delay = Random.Range(0f, 1f);
        Invoke(nameof(PlayWindAnim), delay);

    }
    public void PlayWindAnim()
    {
        GetComponent<Renderer>().enabled = true;
        windAnimator.SetInteger("WindTier", currentWindTier);
        windAnimator.SetBool("Idle?", false);
    }

    public void FlipParent(bool isRight)
    {
        if (isRight == true)
        {
            parent.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else
        {
            parent.transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
    }

    public void ReturnToIdle()
    {
        GetComponent<Renderer>().enabled = false;
        windAnimator.SetBool("Idle?", true);
        windAnimator.SetTrigger("ReturnIdle");  
    }

}
