using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public static ClickManager Instance { get; private set; }

    [Serializable]
    public enum NumberType
    {
        NewLettersPerClick,
        StampedLettersPerClick,
        RateOfNewLetters,
        RateOfStampedLetters,
        RateOfDeliverdLetters
    }
   

    public static event EventHandler NumberOfNewLettersChanged;
    public static event EventHandler NumberOfStampedLettersChanged;
    public static event EventHandler NumberOfDeliveredLettersChanged;

    public static event EventHandler NumberOfStampingCatsChanged;
    public static event EventHandler NumberOfDeliveringCatsChanged;

    public static event EventHandler RateOfNewLettersChanged;

    public static event EventHandler CurrentIncomeChanged;

    [SerializeField] private Animator stampAnimator;
    [SerializeField] private int MaxNumberOfDeliveringCats;

    private int totalNumberOfStampingCats = 0; // infinite
    private int totalNumberOfDeliveringCats = 1; // maxed out 10

    private float totalNumberOfNewLetters = 0;
    private float totalNumberOfStampedLetters = 0;
    private float totalNumberOfDeliveredLetters = 0;

    private float currentNumberOfNewLetters = 0;
    private float currentNumberOfStampedLetters = 0;
    private float currentNumberOfDeliveredLetters = 0;

    private float currentIncome = 0;

    private float totalIncome = 0;

    // 1 letter = 1 cent

    private float numberOfNewLettersPerClick = 1;
    private float numberOfStampedLettersPerClick = 1;

    private float newLetterTicker = 0;
    private float stampedLetterTicker = 0;
    private float sentLetterTicker = 0;

    private float newLetterPerSecond = 0;
    private float stampedLetterPerSecond = 0.5f;
    private float deliveredLetterPerSecond = 0.5f;

    private float totalNumberOfStampClicks = 0;
    private float totalNumberOfPortalClicks = 0;

    private float totalNumberOfStampsByCats = 0;
    private float totalNumberOfDeliveredByCats = 0;

    private bool portalBeingClicked;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        portalBeingClicked = false;
    }

    private void Update()
    {
        NewLetters_Tick();
        StampedLetters_Tick();
        DeliveredLetters_Tick();
    }

    private void NewLetters_Tick()
    {
        newLetterTicker += Time.deltaTime;

        if (newLetterTicker >= 0.1f)
        {
            newLetterTicker = 0;

            if (portalBeingClicked)
            {
                if (newLetterPerSecond == 0)
                {
                    AddNewLetters(numberOfNewLettersPerClick/10);
                    return;
                }

                AddNewLetters((newLetterPerSecond  * numberOfNewLettersPerClick / 10));
            }
            else
            {
                AddNewLetters(newLetterPerSecond / 10);
            }   
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

            AddStampedLetters(lettersToAdd, false);
        }
    }


    private void DeliveredLetters_Tick()
    {
        sentLetterTicker += Time.deltaTime;

        if (sentLetterTicker >= 0.1f)
        {
            var lettersToAdd = totalNumberOfDeliveringCats == 0 ? totalNumberOfDeliveringCats : totalNumberOfDeliveringCats * (deliveredLetterPerSecond / 10);

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

    public void AddStampedLetters(float amount, bool isClick)
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

        if (isClick)
        {
            totalNumberOfStampClicks += numOfLetters;
        }
        else
        {
            totalNumberOfStampsByCats += numOfLetters;
        }

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

        AddIncome(numOfLetters/100);

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
        totalNumberOfPortalClicks += numberOfNewLettersPerClick;
        AddNewLetters(numberOfNewLettersPerClick);
    }

    public void AddStampedLetters_OnClick()
    {
        AddStampedLetters(numberOfStampedLettersPerClick, true);
        stampAnimator.SetTrigger("ButtonPush");
    }

    public void IncreaseStampingCat_OnClick()
    {
        // Have this number be changed in the future to allow for more cats to be purchased at once
        var numOfCats = 1;
        AddStampingCats(numOfCats);
        
    }

    public void IncreaseDeliveryCat_OnClick()
    {
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

        RateOfNewLettersChanged?.Invoke(this, EventArgs.Empty);
    }

    public void IncreaseRateOfStampedLetters(float increasedRate)
    {
        stampedLetterPerSecond += increasedRate;
    }

    public void IncreaseRateOfDeliveredLetters(float increasedRate)
    {
        deliveredLetterPerSecond += increasedRate;
    }

    public void IncreaseNumberOfLettersStampedPerClick(int num)
    {
        numberOfStampedLettersPerClick += num;
    }

    public void IncreaseNumberOfNewLettersPerClick(int num)
    {
        numberOfNewLettersPerClick += num;
    }

    public void AddIncome(float num)
    {
        currentIncome += num;

        CurrentIncomeChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SubtractIncome(float num)
    {
        currentIncome -= num;

        CurrentIncomeChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetIsPortalClicked(bool isPressed)
    {
        portalBeingClicked = isPressed;
    }

    public float GetTotalIncome() => totalIncome;

    public float GetTotalNumberOfStampedLettersClickedByYou() => totalNumberOfStampClicks;

    public float GetTotalNumberOfStampedLettersByCats() => totalNumberOfStampsByCats;

    public float GetNewLettersOnClick() => numberOfNewLettersPerClick;

    public float GetNewLettersPerSecond()
    {
        if (portalBeingClicked)
        {
            var rate = (newLetterPerSecond / 10) * numberOfNewLettersPerClick;

            if(newLetterPerSecond == 0)
            {
                return numberOfNewLettersPerClick;
            }

            return (newLetterPerSecond / 10) * numberOfNewLettersPerClick;
        }
        else
        {
            return newLetterPerSecond / 10;
        }
    }
    public float GetStampedLettersPerSecond() => totalNumberOfStampingCats == 0 ? totalNumberOfStampingCats : totalNumberOfStampingCats * stampedLetterPerSecond;
    public float GetDeliveredLettersPerSecond() => totalNumberOfDeliveringCats == 0 ? deliveredLetterPerSecond : totalNumberOfDeliveringCats * deliveredLetterPerSecond;
    public float GetCurrentIncome() => currentIncome;

}
