using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class BeeTelemetry : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _beeA;
    [SerializeField]
    private TextMeshProUGUI _beeB;
    [SerializeField]
    private TextMeshProUGUI _beeC;
    [SerializeField]
    private TextMeshProUGUI _beeD;
    [SerializeField]
    private TextMeshProUGUI _simulationTime;
    private SimulationData _simulationData;

    [Inject]
    private void Construct(SimulationData simulationData)
    {
        _simulationData = simulationData;
    }

    void Update()
    {
        _beeA.text = "Bee A\nPosition: " + _simulationData.Bees[Bee.BeeLable.a].transform.position.ToString("N4") + "    " +
            "Distance To Flower: " + _simulationData.Bees[Bee.BeeLable.a].DistanceToFlower.ToString("N4") + " Meters" + "\n" +
            "Simulated Arrival Time: " + _simulationData.Bees[Bee.BeeLable.a].SimulatedArrivalTime.ToString("N4") + "    " +
            "Calculated Arrival Time: " + _simulationData.Bees[Bee.BeeLable.a].CalculatedArrivalTime.ToString("N4");

        _beeB.text = "Bee B\nPosition: " + _simulationData.Bees[Bee.BeeLable.b].transform.position.ToString("N4") + "    " +
            "Distance To Flower: " + _simulationData.Bees[Bee.BeeLable.b].DistanceToFlower.ToString("N4") + " Meters" + "\n" +
            "Simulated Arrival Time: " + _simulationData.Bees[Bee.BeeLable.b].SimulatedArrivalTime.ToString("N4") + "    " +
            "Calculated Arrival Time: " + _simulationData.Bees[Bee.BeeLable.b].CalculatedArrivalTime.ToString("N4");

        _beeC.text = "Bee C\nPosition: " + _simulationData.Bees[Bee.BeeLable.c].transform.position.ToString("N4") + "    " +
            "Distance To Flower: " + _simulationData.Bees[Bee.BeeLable.c].DistanceToFlower.ToString("N4") + " Meters" + "\n" +
            "Simulated Arrival Time: " + _simulationData.Bees[Bee.BeeLable.c].SimulatedArrivalTime.ToString("N4") + "    " +
            "Calculated Arrival Time: " + _simulationData.Bees[Bee.BeeLable.c].CalculatedArrivalTime.ToString("N4");

        _beeD.text = "Bee D\nPosition: " + _simulationData.Bees[Bee.BeeLable.d].transform.position.ToString("N4") + "    " +
            "Distance To Flower: " + _simulationData.Bees[Bee.BeeLable.d].DistanceToFlower.ToString("N4") + " Meters" + "\n" +
            "Simulated Arrival Time: " + _simulationData.Bees[Bee.BeeLable.d].SimulatedArrivalTime.ToString("N4") + "    " +
            "Calculated Arrival Time: " + _simulationData.Bees[Bee.BeeLable.d].CalculatedArrivalTime.ToString("N4");

        _simulationTime.text = "Simulation Time: " + _simulationData.CurrentTime.ToString("N4");
    }
}
