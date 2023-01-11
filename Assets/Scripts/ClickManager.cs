using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public static ClickManager Instance { get; private set; }

    public static event EventHandler NumberOfNewLettersChanged;
    public static event EventHandler NumberOfStampedLettersChanged;
    public static event EventHandler NumberOfDeliveredLettersChanged;

    private float totalNumberOfCats;
    private float totalNumberOfNewLetters;
    private float totalNumberOfStampedLetters;
    private float totalNumberOfDeliveredLetters;

    private float numberOfNewLettersPerClick;
    private float numberOfStampedLettersPerClick;

    private float speedOfDelivery;

    float newLetterTicker;
    float stampedLetterTicker;
    float sentLetterTicker;

    private float newLetterPerSecond;
    private float stampedLetterPerSecond;
    private float sentLetterPerSecond;

    private void Awake()
    {
        Instance = this;

        newLetterTicker = 0;
        stampedLetterTicker = 0;
        sentLetterTicker = 0;
    }

    void Start()
    {

        // Setting up some variables, should be put into an init;
        totalNumberOfCats = 1;
        totalNumberOfNewLetters = 0;
        totalNumberOfStampedLetters = 0;
        totalNumberOfDeliveredLetters = 0;
        speedOfDelivery = 0f;

        numberOfNewLettersPerClick = 1;
        numberOfStampedLettersPerClick = 1;

        newLetterPerSecond = 0;
        stampedLetterPerSecond = 0;
        sentLetterPerSecond = 0.1f;
    }

    private void Update()
    {
        NewLetters_Tick();
        StampedLetters_Tick();
        SentLetters_Tick();
    }

    private void NewLetters_Tick()
    {
        newLetterTicker += Time.deltaTime;

        if (newLetterTicker >= 0.1f)
        {
            newLetterTicker = 0;

            AddNewLetters(newLetterPerSecond / 10);
            
        }
    }

    
    private void StampedLetters_Tick()
    {
        stampedLetterTicker += Time.deltaTime;

        if (stampedLetterTicker >= 0.1f)
        {
            stampedLetterTicker = 0;

            var lettersToAdd = stampedLetterPerSecond / 10;

            if (lettersToAdd > totalNumberOfNewLetters) return;

            AddStampedLetters(lettersToAdd);     
        }
    }

    
    private void SentLetters_Tick()
    {
        sentLetterTicker += Time.deltaTime;

        if (sentLetterTicker >= 0.1f)
        {
            var lettersToAdd = sentLetterPerSecond / 10;

            if (lettersToAdd > totalNumberOfStampedLetters) return;

            DeliverLetter(lettersToAdd);

            sentLetterTicker = 0;
        }
    }

    public void AddNewLetters(float amount)
    {
        totalNumberOfNewLetters += amount;

        NumberOfNewLettersChanged?.Invoke(this, EventArgs.Empty);

        print($"Total New: {totalNumberOfNewLetters}");
    }

    public void AddStampedLetters(float amount)
    {
        if(totalNumberOfNewLetters <= 0)
        {
            return;
        }

        var numOfLetters = amount;

        if (totalNumberOfNewLetters < amount)
        {
            numOfLetters = amount - totalNumberOfNewLetters;
            totalNumberOfNewLetters = 0;
        }
        else
        {
            totalNumberOfNewLetters -= amount;
        }

        totalNumberOfStampedLetters += numOfLetters;

        NumberOfNewLettersChanged?.Invoke(this, EventArgs.Empty);
        NumberOfStampedLettersChanged?.Invoke(this, EventArgs.Empty);

        print($"Total Stamped: {totalNumberOfStampedLetters}");
    }

    public void DeliverLetter(float amount)
    {
        if (totalNumberOfStampedLetters <= 0)
        {
            return;
        }

        var numOfLetters = amount;

        if (totalNumberOfStampedLetters < amount)
        {
            numOfLetters = amount - totalNumberOfStampedLetters;
            totalNumberOfStampedLetters = 0;
        }
        else
        {
            totalNumberOfStampedLetters -= amount;
        }

        totalNumberOfDeliveredLetters += numOfLetters;

        NumberOfStampedLettersChanged?.Invoke(this, EventArgs.Empty);
        NumberOfDeliveredLettersChanged?.Invoke(this, EventArgs.Empty);

        print($"Total Delivered: {totalNumberOfDeliveredLetters}");
    }


    public float GetNewLetters() => totalNumberOfNewLetters;

    public float GetStampedLetters() => totalNumberOfStampedLetters;

    public float GetDeliveredLetters() => totalNumberOfDeliveredLetters;

    public void AddNewLetters_OnClick()
    {
        AddNewLetters(numberOfNewLettersPerClick);
    }

    public void AddStampedLetters_OnClick()
    {
        AddStampedLetters(numberOfStampedLettersPerClick);
    }
}
