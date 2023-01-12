using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public static ClickManager Instance { get; private set; }

    public enum NumberType
    {
        StampingCat, // Number of stamping cats
        DeliveryCat, // Number of delivery cats
        NewLetters, // 
        StampedLetters,
        RateOfNewLetters,
        Number

    }

    public static event EventHandler NumberOfNewLettersChanged;
    public static event EventHandler NumberOfStampedLettersChanged;
    public static event EventHandler NumberOfDeliveredLettersChanged;

    public static event EventHandler NumberOfStampingCatsChanged;
    public static event EventHandler NumberOfDeliveringCatsChanged;

    private int totalNumberOfStampingCats; // infinite
    private int totalNumberOfDeliveringCats; // maxed out 10

    [SerializeField] private int MaxNumberOfDeliveringCats;

    private float totalNumberOfNewLetters;
    private float totalNumberOfStampedLetters;
    private float totalNumberOfDeliveredLetters;

    private float currentNumberOfNewLetters;
    private float currentNumberOfStampedLetters;
    private float currentNumberOfDeliveredLetters;

    // 1 letter = 1 cent

    private float numberOfNewLettersPerClick;
    private float numberOfStampedLettersPerClick;

    float newLetterTicker;
    float stampedLetterTicker;
    float sentLetterTicker;

    private float newLetterPerSecond;
    private float stampedLetterPerSecond;
    private float sentLetterPerSecond;

    private int totalNumberOfClicks;

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
        totalNumberOfDeliveringCats = 1;
        totalNumberOfStampingCats = 0;

        totalNumberOfNewLetters = 0;
        totalNumberOfStampedLetters = 0;
        totalNumberOfDeliveredLetters = 0;

        currentNumberOfNewLetters = 0;
        currentNumberOfStampedLetters = 0;
        currentNumberOfDeliveredLetters = 0;

        numberOfNewLettersPerClick = 1;
        numberOfStampedLettersPerClick = 1;

        // automation variables
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

            var lettersToAdd = totalNumberOfStampingCats == 0 ? totalNumberOfStampingCats : totalNumberOfStampingCats * (stampedLetterPerSecond / 10);

            if (lettersToAdd > currentNumberOfNewLetters) return;

            AddStampedLetters(lettersToAdd);
        }
    }


    private void SentLetters_Tick()
    {
        sentLetterTicker += Time.deltaTime;

        if (sentLetterTicker >= 0.1f)
        {
            var lettersToAdd = totalNumberOfDeliveringCats == 0 ? totalNumberOfDeliveringCats : totalNumberOfDeliveringCats * (sentLetterPerSecond / 10);

            if (lettersToAdd > currentNumberOfStampedLetters) return;

            DeliverLetter(lettersToAdd);

            sentLetterTicker = 0;
        }
    }

    public void AddNewLetters(float amount)
    {
        currentNumberOfNewLetters += amount;
        totalNumberOfNewLetters += amount;

        NumberOfNewLettersChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddStampedLetters(float amount)
    {
        if (currentNumberOfNewLetters <= 0)
        {
            return;
        }

        var numOfLetters = amount;

        if (currentNumberOfNewLetters < amount)
        {
            numOfLetters = amount - currentNumberOfNewLetters;
            currentNumberOfNewLetters = 0;
        }
        else
        {
            currentNumberOfNewLetters -= amount;
        }

        currentNumberOfStampedLetters += numOfLetters;
        totalNumberOfStampedLetters += numOfLetters;

        NumberOfNewLettersChanged?.Invoke(this, EventArgs.Empty);
        NumberOfStampedLettersChanged?.Invoke(this, EventArgs.Empty);
    }

    public void DeliverLetter(float amount)
    {
        if (currentNumberOfStampedLetters <= 0)
        {
            return;
        }

        var numOfLetters = amount;

        if (currentNumberOfStampedLetters < amount)
        {
            numOfLetters = amount - currentNumberOfStampedLetters;
            currentNumberOfStampedLetters = 0;
        }
        else
        {
            currentNumberOfStampedLetters -= amount;
        }

        currentNumberOfDeliveredLetters += numOfLetters;
        totalNumberOfDeliveredLetters += numOfLetters;

        NumberOfStampedLettersChanged?.Invoke(this, EventArgs.Empty);
        NumberOfDeliveredLettersChanged?.Invoke(this, EventArgs.Empty);
    }


    public float GetCurrentNewLetters() => currentNumberOfNewLetters;

    public float GetCurrentStampedLetters() => currentNumberOfStampedLetters;

    public float GetCurrentDeliveredLetters() => currentNumberOfDeliveredLetters;

    public int GetNumberOfStampingCats() => totalNumberOfStampingCats;
    public int GetNumberOfDeliveringCats() => totalNumberOfDeliveringCats;

    public void AddNewLetters_OnClick()
    {
        totalNumberOfClicks++;
        AddNewLetters(numberOfNewLettersPerClick);
    }

    public void AddStampedLetters_OnClick()
    {
        totalNumberOfClicks++;
        AddStampedLetters(numberOfStampedLettersPerClick);
    }

    public void IncreaseStampingCat_OnClick()
    {
        totalNumberOfClicks++;
        // Have this number be changed in the future to allow for more cats to be purchased at once
        var numOfCats = 1;
        AddStampingCats(numOfCats);
        
    }

    public void IncreaseDeliveryCat_OnClick()
    {
        totalNumberOfClicks++;
        AddDeliveringCat();
    }

    // Adding in cats
    public void AddStampingCats(int numOfCats)
    {
        // Can add as many as you like
        totalNumberOfStampingCats += numOfCats;
        NumberOfStampingCatsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddDeliveringCat()
    {
        if(totalNumberOfDeliveringCats >= MaxNumberOfDeliveringCats)
        {
            Debug.Log("Maximum nunber of delivering cats reached");

            return;
        }

        // Have a limited amount of new cats
        totalNumberOfDeliveringCats++;
        NumberOfDeliveringCatsChanged?.Invoke(this, EventArgs.Empty);
    }


    // Increase the rate of automation
    public void IncreaseRateOfNewLetters(float increasedRate)
    {
        newLetterPerSecond += increasedRate;
    }

    public void IncreaseRateOfStampedLetters(float increasedRate)
    {
        stampedLetterPerSecond += increasedRate;
    }

    public void IncreaseRateOfDeliveredLetters(float increasedRate)
    {
        sentLetterPerSecond += increasedRate;
    }

    public void IncreaseNumberOfLettersStampedPerClick(int num)
    {
        numberOfStampedLettersPerClick += num;
    }

    public void IncreaseNumberOfNewLettersPerClick(int num)
    {
        numberOfNewLettersPerClick += num;
    }

}
