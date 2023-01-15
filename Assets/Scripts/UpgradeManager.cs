using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    private ClickManager _clickManager;



    public enum UpgradeTypes
    {
        
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _clickManager = ClickManager.Instance;
    }

    public void Upgrade(UpgradeSO upgrade)
    {
        switch (upgrade.NumberType)
        {
            case ClickManager.NumberType.RateOfNewLetters:
                RateOfNewLetters(upgrade);
                break;
            case ClickManager.NumberType.RateOfStampedLetters:
                RateOfStampedLetters(upgrade);
                break;
            case ClickManager.NumberType.RateOfDeliverdLetters:
                RateOfDeliverdLetters(upgrade);
                break;
            case ClickManager.NumberType.NewLettersPerClick:
                NewLettersPerClick(upgrade);
                break;
            case ClickManager.NumberType.StampedLettersPerClick:
                StampedLettersPerClick(upgrade);
                break;
        }
    }

    private void RateOfNewLetters(UpgradeSO upgrade)
    {
        _clickManager.IncreaseRateOfNewLetters(upgrade.Number);
    }

    private void RateOfStampedLetters(UpgradeSO upgrade)
    {
        _clickManager.IncreaseRateOfStampedLetters(upgrade.Number);
    }

    private void RateOfDeliverdLetters(UpgradeSO upgrade)
    {
        _clickManager.IncreaseRateOfDeliveredLetters(upgrade.Number);
    }

    private void NewLettersPerClick(UpgradeSO upgrade)
    {
        _clickManager.IncreaseNumberOfNewLettersPerClick((int)upgrade.Number);
    }

    private void StampedLettersPerClick(UpgradeSO upgrade)
    {
        _clickManager.IncreaseNumberOfLettersStampedPerClick((int)upgrade.Number);
    }


}
