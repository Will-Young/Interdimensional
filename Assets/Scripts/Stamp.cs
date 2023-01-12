using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stamp : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AudioClip _stampSound;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clciked");
    }

    private void OnMouseDown()
    {
        ClickManager.Instance.AddStampedLetters_OnClick();
        SoundManager.Instance.PlaySound(_stampSound);
        Debug.Log("Clicked");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
