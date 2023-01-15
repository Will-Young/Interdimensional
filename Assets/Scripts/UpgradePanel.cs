using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePanel : MonoBehaviour
{

    [SerializeField] private List<UpgradeSO> upgrades;

    [SerializeField] private Transform UpgradeButtonPrefab;
    [SerializeField] private Transform ContentTransform;

    void Start()
    {
        CreateUpgradesButtons();
    }
     private void CreateUpgradesButtons()
    {
        foreach(var upgrade in upgrades)
        {
            var ButtonTransform = Instantiate(UpgradeButtonPrefab, ContentTransform);
            ButtonTransform.GetComponent<UpgradeButton>().SetUpgrade(upgrade);
        }
    }
}
