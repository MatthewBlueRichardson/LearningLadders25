using System.Runtime.CompilerServices;
using UnityEngine;

public class WindAnimationTriggers : MonoBehaviour
{
    [SerializeField] private Animator windAnimator;
    [SerializeField] private GameObject parent;

    public void OnWindTierChanged(int windTier)
    {
        //GetComponent<Renderer>().enabled = true;
        windAnimator.SetInteger("WindTier", windTier);
    }

    public void FlipParent(bool isRight)
    {
        if (isRight == true)
        {
            parent.transform.Rotate(0f, 0f, 0f);
        }
        else
        {
            parent.transform.Rotate(0f, 180f, 0f);
        }
    }

    public void ReturnToIdle()
    {
        //windAnimator.SetTrigger("Idle");
        //GetComponent<Renderer>().enabled = false;
    }

}
