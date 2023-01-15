using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stamp : MonoBehaviour
{
    [SerializeField] private AudioClip _stampSound;

    private void OnMouseDown()
    {
        ClickManager.Instance.AddStampedLetters_OnClick();
        SoundManager.Instance.PlaySound(_stampSound);
        Debug.Log("Clicked");
    }

}
