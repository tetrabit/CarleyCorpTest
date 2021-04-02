using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class QuestionAnswers : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _answer1;
    [SerializeField]
    private TextMeshProUGUI _answer2;
    private SimulationData _simulationData;

    [Inject]
    private void Construct(SimulationData simulationData)
    {
        _simulationData = simulationData;
    }

    void Start()
    {
        _answer1.text = "Bee A: " + _simulationData.Bees[Bee.BeeLable.a].CalculatedArrivalTime.ToString("N4") + " Seconds\t" +
                        "Bee B: " + _simulationData.Bees[Bee.BeeLable.b].CalculatedArrivalTime.ToString("N4") + " Seconds\n" +
                        "Bee C: " + _simulationData.Bees[Bee.BeeLable.c].CalculatedArrivalTime.ToString("N4") + " Seconds\t" +
                        "Bee D: " + _simulationData.Bees[Bee.BeeLable.d].CalculatedArrivalTime.ToString("N4") + " Seconds";

        _answer2.text = _simulationData.AnswerQuestionTwo();
    }
}
