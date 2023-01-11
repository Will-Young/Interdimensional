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

    void Start()
    {
        ClickManager.NumberOfStampedLettersChanged += ClickManager_NumberOfStampedLettersChanged;
        ClickManager.NumberOfNewLettersChanged += ClickManager_NumberOfNewLettersChanged;
        ClickManager.NumberOfDeliveredLettersChanged += ClickManager_NumberOfDeliveredLettersChanged;

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

        UpdateTotalNumberOfStampedLettersText(clickManager.GetStampedLetters().ToString());
    }

    private void ClickManager_NumberOfNewLettersChanged(object sender, EventArgs e)
    {
        var clickManager = sender as ClickManager;
        UpdateTotalNumberOfNewLettersText(clickManager.GetNewLetters().ToString());
    }

    private void ClickManager_NumberOfDeliveredLettersChanged(object sender, EventArgs e)
    {
        var clickManager = sender as ClickManager;
        UpdateTotalNumberOfDeliveredLettersText(clickManager.GetDeliveredLetters().ToString());
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
}
