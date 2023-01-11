using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalUpgrade : MonoBehaviour
{
    [SerializeField] private Transform upgradeWindowTransform;

    public void ToggleWindow()
    {
        upgradeWindowTransform.gameObject.SetActive(!upgradeWindowTransform.gameObject.activeSelf);
    }
}
