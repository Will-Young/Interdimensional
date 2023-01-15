using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeReference] private ParticleSystem particleSystem;

    private void Start()
    {
        particleSystem.maxParticles = Mathf.RoundToInt(ClickManager.Instance.GetNewLettersPerSecond());
    }

    private void OnMouseDown()
    {
        Debug.Log("Mouse Down for Portal");
        ClickManager.Instance.SetIsPortalClicked(true);

        particleSystem.maxParticles = Mathf.RoundToInt(ClickManager.Instance.GetNewLettersPerSecond()) * Mathf.RoundToInt(ClickManager.Instance.GetNewLettersOnClick());
    }

    private void OnMouseUp()
    {
        Debug.Log("Mouse Up for Portal");
        ClickManager.Instance.SetIsPortalClicked(false);

        particleSystem.maxParticles = Mathf.RoundToInt(ClickManager.Instance.GetNewLettersPerSecond());
    }
}
