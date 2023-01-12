using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PortalUpgradeUI : MonoBehaviour
{
    [SerializeField] private Transform upgradeWindowTransform;

    [SerializeField] private List<string> upgradeList;

    public void ToggleWindow()
    {
        upgradeWindowTransform.gameObject.SetActive(!upgradeWindowTransform.gameObject.activeSelf);
    }

}
