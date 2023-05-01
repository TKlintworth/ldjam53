using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton pattern for easy access to the GameManager
    public static GameManager Instance;

    //public List<Order> Orders;
    public string currentDay;
    public float currentTime = 0f; // Actual elapsed time in seconds
    public float inGameHours = 0f; // In-game hours (11 AM to midnight). Every second of real-life time, 130 seconds (2 minutes) will pass in the simulated time.
    public float realLifeDuration = 300f; // 5 minutes in seconds


    private float timeScale;
    public float baseOrderRate = 0.3f; // Orders per second
    public float orderRate; // Orders per second
    //private float orderOffsetRange; // Seconds to offset order rate
    private float orderMultiplier = 1f; // Multiplier for order rate
    public float nextOrderTime = 0f;
    private float lastOrderTime = 0f;
    private float lastTimePeriod = -1f;

    private float dinnerOrderRate = 2f;
    private float lunchOrderRate = 1.5f;
    private float totalOrdersSent = 0f;
    private float totalOrdersCompleted = 0f;
    private float totalOrdersMissed = 0f;


    public bool gameStarted = false;
    public bool dayActive = false;
    private List<string> days = new List<string> {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday"};
    private Dictionary<string, float> orderRates = new Dictionary<string, float> {
        {"Monday", 0.3f},
        {"Tuesday", 0.4f},
        {"Wednesday", 0.5f},
        {"Thursday", 0.6f},
        {"Friday", 0.7f}
    };

    private void Awake()
    {
        // Ensure there's only one instance of GameManager
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update, but after Awake()
    // This is for initialization
    private void Start()
    {
        timeScale = 13f / realLifeDuration; // 13 hours (11 AM to midnight) in-game time
        //orderOffsetRange = 0.5f * orderRate; // 50% of order rate
        Debug.Log("GameManager Start");
    }

    private void Update()
    {
        // If the game has started, update the time and check for events
        if (dayActive)
        {
            UpdateTime();
            CheckEvents();
        }
    }

    // The start button has been pressed and the game begins
    public void StartGame()
    {
        Debug.Log("Start Game");
        // Hide the button
        GameObject.Find("StartButton").SetActive(false);
        // Other start game stuff
        gameStarted = true;
        dayActive = true;
        currentDay = days[0];
        orderRate = orderRates[currentDay];
        CalculateNextOrderTime();
    }


    private void UpdateTime()
    {
        currentTime += Time.deltaTime;
        inGameHours = 11f + currentTime * timeScale; // Convert real-life seconds to in-game hours
        //Debug.Log("Current Time: " + currentTime);
        //Debug.Log("In-Game Hours: " + inGameHours);
        // Check if it's the end of the day
        if(inGameHours >= 24f)
        {
            // End the day and then transition to the next day
            EndDay();
        }
    }

    private void ResetTime()
    {
        currentTime = 0f;
        inGameHours = 0f;
    }

    private void CheckEvents()
    {
        // Change the order rate based on whether it's lunch or dinner time

        float lunchStart = 12f; // 12 PM
        float lunchEnd = 14f; // 2 PM
        float dinnerStart = 18f; // 6 PM
        float dinnerEnd = 21f; // 9 PM

        // Example: Trigger an event based on in-game hours
        float currentTimePeriod;

        if (inGameHours >= lunchStart && inGameHours <= lunchEnd)
        {
            currentTimePeriod = 1f;
            if (lastTimePeriod != currentTimePeriod)
            {
                lastTimePeriod = currentTimePeriod;
                orderRate = baseOrderRate * lunchOrderRate;
                Debug.Log("Lunch Time");
                CalculateNextOrderTime();
            }
        }
        else if (inGameHours >= dinnerStart && inGameHours <= dinnerEnd)
        {
            currentTimePeriod = 2f;
            if (lastTimePeriod != currentTimePeriod)
            {
                lastTimePeriod = currentTimePeriod;
                orderRate = baseOrderRate * dinnerOrderRate;
                Debug.Log("Dinner Time");
                CalculateNextOrderTime();
            }
        }
        else
        {
            currentTimePeriod = 0f;
            if (lastTimePeriod != currentTimePeriod)
            {
                lastTimePeriod = currentTimePeriod;
                orderRate = baseOrderRate;
                Debug.Log("Other Time");
                CalculateNextOrderTime();
            }
        }
        if(currentTime >= nextOrderTime)
        {
            // Send an order to OrderManager every orderRate seconds plus the orderOffset multiplied by the orderMultiplier
            Debug.Log("Send Order");
            OrderManager.Instance.RequestOrder();
            CalculateNextOrderTime();
        }
    }

    private void CalculateNextOrderTime()
    {
        Debug.Log("CalculateNextOrderTime");
        // Calculate the next time to send an order
        //TODO: Test this
        //float orderOffset = Random.Range(-orderOffsetRange, orderOffsetRange);
        float orderInterval = 3f / (orderRate * orderMultiplier);
        nextOrderTime = currentTime + orderInterval; //+ orderOffset;
        Debug.Log("Next Order Time: " + nextOrderTime);

    }

    private void EndDay()
    {
        // Check if this is the end of the last day
        if(days.IndexOf(currentDay) == days.Count - 1)
        {
            // End the game
            // Display the results of the game as a UI element
            // Display the button to start the game again
            Debug.Log("End Game");
        }
        else
        {
            // End the day
            // Pause everything (time, orders, etc.)
            // Display the results of the day as a UI element

            // Display the button to start the next day
            Debug.Log("End Day");
            dayActive = false;
        }
    }

    public void TransitionToNextDay()
    {
        Debug.Log("StartNextDay");
        currentDay = days[days.IndexOf(currentDay) + 1];
        orderRate = orderRates[currentDay];
        // Set dayActive to start the timer again
        ResetTime();
        dayActive = true;

        // Start the next day
        // Unpause everything
        // Hide the button to start the next day
        // Update the current day
        // Calculate the next order time
    }
}