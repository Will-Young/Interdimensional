using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI totalNumberOfStampedLetters;
    [SerializeField] private TextMeshProUGUI totalNumberOfNewLetters;
    [SerializeField] private TextMeshProUGUI totalNumberOfDeliveredLetters;

    
    [SerializeField] private TextMeshProUGUI numberOfStampignCatsText;
    [SerializeField] private TextMeshProUGUI numberOfDeliveryCatsText;

    void Start()
    {
        ClickManager.NumberOfStampedLettersChanged += ClickManager_NumberOfStampedLettersChanged;
        ClickManager.NumberOfNewLettersChanged += ClickManager_NumberOfNewLettersChanged;
        ClickManager.NumberOfDeliveredLettersChanged += ClickManager_NumberOfDeliveredLettersChanged;

        ClickManager.NumberOfStampingCatsChanged += ClickManager_NumberOfStampingCatsChanged;
        ClickManager.NumberOfDeliveringCatsChanged += ClickManager_NumberOfDeliveringCatsChanged;

        UpdateTotalNumberOfStampedLettersText("0");
        UpdateTotalNumberOfNewLettersText("0");
        UpdateTotalNumberOfDeliveredLettersText("0");
    }


    void Update()
    {
        
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



    // Function to calculate cost of upgrade
}
