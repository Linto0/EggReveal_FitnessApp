using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FitnessDataSimulator : MonoBehaviour
{
    public TextMeshProUGUI heartRateText;
    public TextMeshProUGUI stepsText;
    public TextMeshProUGUI caloriesText;

    private int heartRate;
    private int steps;
    private int calories;

    void Start()
    {
        // Start with random values
        heartRate = Random.Range(70, 90);
        steps = Random.Range(2000, 6000);
        calories = Random.Range(100, 300);

        UpdateUI();
        InvokeRepeating(nameof(SimulateData), 2f, 2f); // simulate every 2 seconds
    }

    void SimulateData()
    {
        heartRate += Random.Range(-2, 3);
        steps += Random.Range(10, 100);
        calories += Random.Range(1, 5);

        UpdateUI();
    }

    void UpdateUI()
    {
        heartRateText.text = $"Heart Rate: {heartRate} bpm";
        stepsText.text = $"Steps Today: {steps:N0}";
        caloriesText.text = $"Calories: {calories} kcal";
    }
}