using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Image upgradeImage;
    [SerializeField] private TextMeshProUGUI upgradeTextMesh;
    [SerializeField] private string upgradeText;



    private int upgradeCost;

    private void Awake()
    {
        upgradeTextMesh.text = upgradeText;
    }

    private void Start()
    {
        // Subscribe to event
    }

    public void Upgrade_OnClick()
    {
        // Upgrade
    }

    private void UpdateEnable()
    {
        
    }
}
