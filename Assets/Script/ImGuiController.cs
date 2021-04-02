using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImGuiNET;
using Zenject;
using System.Linq;

public class ImGuiController : MonoBehaviour
{
    public GameObject BeeA;

    private Flower _flower;
    private SimulationData _simulationData;
    private float _currentTime;
    private string _question2Answer;
    private int _simulationType;

    [Inject]
    private void Construct(Flower flower, SimulationData simulationData)
    {
        _flower = flower;
        _simulationData = simulationData;
    }

    private void Start()
    {
        _question2Answer = _simulationData.AnswerQuestionTwo();
    }

    void OnEnable()
    {
        ImGuiUn.Layout += OnLayout;
    }

    void OnDisable()
    {
        ImGuiUn.Layout -= OnLayout;
    }

    void OnLayout()
    {
        ImGui.ShowDemoWindow();
        BeeInfoWindow();
        QuestionAnswersWindow();
        SimulationControlsWindow();
    }

    private void SimulationControlsWindow()
    {
        ImGui.Begin("Simulation Controls");
        {
            if(ImGui.Button("Play"))
                _simulationData.StartSimulation();
            if (ImGui.Button("Reset"))
                _simulationData.ResetSimulation();

            ImGui.RadioButton("Unity Simulation", ref _simulationType, 0);
            ImGui.SameLine();
            ImGui.RadioButton("My Simulation", ref _simulationType, 1);
            ImGui.SameLine();
            ImGui.RadioButton("Manual", ref _simulationType, 2);
            _simulationData.SimulationType = (SimulationData.SimulationSystem)_simulationType;
            if (_simulationData.SimulationType == SimulationData.SimulationSystem.Manual)
                ImGui.DragFloat("Time", ref _currentTime, 0.01f);
        }
        ImGui.End();
    }

    private void QuestionAnswersWindow()
    {
        ImGui.Begin("Question Answers");
        {
            ImGui.TextWrapped("Question 1.1: Showing your work, how many seconds does it take each bee to arrive at the flower?");
            ImGui.Text("Bee A: " + _simulationData.Bees[Bee.BeeLable.a].CalculatedArrivalTime.ToString("N4") + " Seconds");
            ImGui.Text("Bee B: " + _simulationData.Bees[Bee.BeeLable.b].CalculatedArrivalTime.ToString("N4") + " Seconds");
            ImGui.Text("Bee C: " + _simulationData.Bees[Bee.BeeLable.c].CalculatedArrivalTime.ToString("N4") + " Seconds");
            ImGui.Text("Bee D: " + _simulationData.Bees[Bee.BeeLable.d].CalculatedArrivalTime.ToString("N4") + " Seconds");
            ImGui.NewLine();
            ImGui.TextWrapped("Question 1.2: Showing your work, Which two bees come closest to each other 2 seconds before the first bee arrives at the flower and what is the distance?");
            ImGui.TextWrapped(_question2Answer);
        }
        ImGui.End();
    }    

    private void BeeInfoWindow()
    {
        ImGui.Begin("Bee Info");
        {
            ImGui.Text("Bee A");
            ImGui.Text("Position: " + _simulationData.Bees[Bee.BeeLable.a].transform.position.ToString("N4"));
            ImGui.Text("Distance: " + _simulationData.Bees[Bee.BeeLable.a].DistanceToFlower.ToString("N4") + " Meters");
            ImGui.Text("Simulated Arrival Time: " + _simulationData.Bees[Bee.BeeLable.a].SimulatedArrivalTime.ToString("N4"));
            ImGui.Text("Calculated Arrival Time: " + _simulationData.Bees[Bee.BeeLable.a].CalculatedArrivalTime.ToString("N4"));
            ImGui.Text("Bee B");
            ImGui.Text("Position: " + _simulationData.Bees[Bee.BeeLable.b].transform.position.ToString("N4"));
            ImGui.Text("Distance: " + _simulationData.Bees[Bee.BeeLable.b].DistanceToFlower.ToString("N4") + " Meters");
            ImGui.Text("Simulated Arrival Time: " + _simulationData.Bees[Bee.BeeLable.b].SimulatedArrivalTime.ToString("N4"));
            ImGui.Text("Calculated Arrival Time: " + _simulationData.Bees[Bee.BeeLable.b].CalculatedArrivalTime.ToString("N4"));
            ImGui.Text("Bee C");
            ImGui.Text("Position: " + _simulationData.Bees[Bee.BeeLable.c].transform.position.ToString("N4"));
            ImGui.Text("Distance: " + _simulationData.Bees[Bee.BeeLable.c].DistanceToFlower.ToString("N4") + " Meters");
            ImGui.Text("Simulated Arrival Time: " + _simulationData.Bees[Bee.BeeLable.c].SimulatedArrivalTime.ToString("N4"));
            ImGui.Text("Calculated Arrival Time: " + _simulationData.Bees[Bee.BeeLable.c].CalculatedArrivalTime.ToString("N4"));
            ImGui.Text("Bee D");
            ImGui.Text("Position: " + _simulationData.Bees[Bee.BeeLable.d].transform.position.ToString("N4"));
            ImGui.Text("Distance: " + _simulationData.Bees[Bee.BeeLable.d].DistanceToFlower.ToString("N4") + " Meters");
            ImGui.Text("Simulated Arrival Time: " + _simulationData.Bees[Bee.BeeLable.d].SimulatedArrivalTime.ToString("N4"));
            ImGui.Text("Calculated Arrival Time: " + _simulationData.Bees[Bee.BeeLable.d].CalculatedArrivalTime.ToString("N4"));
        }
        ImGui.End();
    }

    private void Update()
    {
        _simulationData.CurrentTime = _currentTime;
    }
}
