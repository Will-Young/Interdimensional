using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    ClickManager clickManager;

    [SerializeField] private TextMeshProUGUI totalNumberOfStampedLetters;
    [SerializeField] private TextMeshProUGUI totalNumberOfNewLetters;
    [SerializeField] private TextMeshProUGUI totalNumberOfDeliveredLetters;

    
    [SerializeField] private TextMeshProUGUI numberOfStampignCatsText;
    [SerializeField] private TextMeshProUGUI numberOfDeliveryCatsText;

    [SerializeField] private TextMeshProUGUI numStampingCatStat;
    [SerializeField] private TextMeshProUGUI numDeliveringCatStat;
    [SerializeField] private TextMeshProUGUI numLetterStampedYou;
    [SerializeField] private TextMeshProUGUI numLetterStampedCat;
    [SerializeField] private TextMeshProUGUI totalIncome;

    [SerializeField] private TextMeshProUGUI newPerSecond;
    [SerializeField] private TextMeshProUGUI stampedPerSecond;
    [SerializeField] private TextMeshProUGUI deliveredPerSecond;

    float refreshTimer;

    private void Awake()
    {
        refreshTimer = 1f;
    }

    void Start()
    {
        clickManager = ClickManager.Instance;

        ClickManager.NumberOfStampedLettersChanged += ClickManager_NumberOfStampedLettersChanged;
        ClickManager.NumberOfNewLettersChanged += ClickManager_NumberOfNewLettersChanged;
        ClickManager.NumberOfDeliveredLettersChanged += ClickManager_NumberOfDeliveredLettersChanged;

        ClickManager.NumberOfStampingCatsChanged += ClickManager_NumberOfStampingCatsChanged;
        ClickManager.NumberOfDeliveringCatsChanged += ClickManager_NumberOfDeliveringCatsChanged;

        UpdateTotalNumberOfStampedLettersText("0");
        UpdateTotalNumberOfNewLettersText("0");
        UpdateTotalNumberOfDeliveredLettersText("0");

        UpdateStatUI();
        UpdateRateUI();
    }


    void Update()
    {
        refreshTimer -= Time.deltaTime;

        if(refreshTimer <= 0)
        {
            refreshTimer = 1f;
            UpdateStatUI();
            UpdateRateUI();
        }


    }

    private void ClickManager_NumberOfStampedLettersChanged(object sender, EventArgs e)
    {
        var clickManager = sender as ClickManager;

        UpdateTotalNumberOfStampedLettersText(Mathf.RoundToInt(clickManager.GetCurrentStampedLetters()).ToString());
    }

    private void ClickManager_NumberOfNewLettersChanged(object sender, EventArgs e)
    {
        var clickManager = sender as ClickManager;
        UpdateTotalNumberOfNewLettersText(Mathf.RoundToInt(clickManager.GetCurrentNewLetters()).ToString());
    }

    private void ClickManager_NumberOfDeliveredLettersChanged(object sender, EventArgs e)
    {
        var clickManager = sender as ClickManager;
        UpdateTotalNumberOfDeliveredLettersText(Mathf.RoundToInt(clickManager.GetCurrentDeliveredLetters()).ToString());
    }

    private void ClickManager_NumberOfStampingCatsChanged(object sender, EventArgs e)
    {
        var clickManager = sender as ClickManager;
        UpdateStampingCatsNumber(clickManager.GetNumberOfStampingCats());
    }

    private void ClickManager_NumberOfDeliveringCatsChanged(object sender, EventArgs e)
    {
        var clickManager = sender as ClickManager;
        UpdateDeliveringCatsNumber(clickManager.GetNumberOfDeliveringCats());
    }




    private void UpdateTotalNumberOfStampedLettersText(string text)
    {
        totalNumberOfStampedLetters.text = text;
    }

    private void UpdateTotalNumberOfNewLettersText(string text)
    {
        totalNumberOfNewLetters.text = text;
    }

    private void UpdateTotalNumberOfDeliveredLettersText(string text)
    {
        totalNumberOfDeliveredLetters.text = text;
    }

    private void UpdateStampingCatsNumber(int number)
    {
        numberOfStampignCatsText.text = number.ToString();
    }

    private void UpdateDeliveringCatsNumber(int number)
    {
        numberOfDeliveryCatsText.text = number.ToString();
    }


    ///               ///
    /// Button Clicks ///
    ///               ///

    private void UpdateStatUI()
    {
        numStampingCatStat.text = clickManager.GetNumberOfStampingCats().ToString();
        numDeliveringCatStat.text = clickManager.GetNumberOfDeliveringCats().ToString();
        numLetterStampedYou.text = clickManager.GetTotalNumberOfStampedLettersClickedByYou().ToString();
        numLetterStampedCat.text = clickManager.GetTotalNumberOfStampedLettersByCats().ToString();
        totalIncome.text = clickManager.GetTotalIncome().ToString();
    }

    private void UpdateRateUI()
    {
        newPerSecond.text = clickManager.GetNewLettersPerSecond().ToString();
        stampedPerSecond.text = clickManager.GetStampedLettersPerSecond().ToString();
        deliveredPerSecond.text = clickManager.GetDeliveredLettersPerSecond().ToString();
    }

}
