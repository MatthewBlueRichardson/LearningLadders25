using LearningLadders.EventSystem;
using System.Linq.Expressions;
using UnityEngine;

public class PlayerParticleTriggers : MonoBehaviour
{
    [SerializeField] private VoidEvent repPE;

    [SerializeField] private ParticleSystem repPickupPS;

    public void RepItemPickupPE()
    {
        print("RepItem");
        repPickupPS.Play();
    }
}
