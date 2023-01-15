using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Image upgradeImage;
    [SerializeField] private TextMeshProUGUI upgradeNameTextMesh;
    [SerializeField] private TextMeshProUGUI upgradeCostTextMesh;

    private UpgradeSO upgrade;

    private float upgradeCost;

    private void Start()
    {
        // Subscribe to event
        ClickManager.CurrentIncomeChanged += ClickManager_CurrentIncomeChanged;
        UpdateButtonVisual();
    }

    public void Upgrade_OnClick()
    {
        // Upgrade
    }

    private void UpdateEnable()
    {
        
    }

    public int CalculateUpgradeCost()
    {
        // Need to be able to calculate the cost of the upgrade depending on how many letters are being brought in
        return 0;
    }

    public void SetUpgrade(UpgradeSO upgrade)
    {
        this.upgrade = upgrade;

        upgradeNameTextMesh.text = upgrade.Name;
        upgradeCost = upgrade.UpgradeCost();
        upgradeCostTextMesh.text = $"${upgradeCost.ToString()}";
        upgradeImage = upgrade.Image;

        GetComponent<Button>().onClick.AddListener(() => UpgradeManager.Instance.Upgrade(upgrade));
        GetComponent<Button>().onClick.AddListener(() => Destroy(gameObject));
    }

    private void OnEnable()
    {
        UpdateButtonVisual();
    }

    private void OnDestroy()
    {
        
    }

    private void ClickManager_CurrentIncomeChanged(object sender, EventArgs e)
    {
        UpdateButtonVisual();
    }

    private void UpdateButtonVisual()
    {
        if(upgradeCost > ClickManager.Instance.GetCurrentIncome())
        {
            //Debug.Log($"{name} should not be interactable");
            GetComponent<Button>().interactable = false;
        }
        else
        {
            //Debug.Log($"{name} should be interactable");
            GetComponent<Button>().interactable = true;
        }        
    }
}
