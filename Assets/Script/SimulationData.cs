using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using System;

public class SimulationData : IInitializable, IDisposable
{
    public enum SimulationSystem
    {
        Unity,
        Calculated,
        Manual
    }

    private Dictionary<Bee.BeeLable, Bee> _bees = new Dictionary<Bee.BeeLable, Bee>();
    private double _maxTime = 0;
    private float _currentTime = 0;
    private bool _simulate;
    private SimulationSystem _simulationType;
    private SignalBus _signalBus;
    private CompositeDisposable _disposables = new CompositeDisposable();

    public Dictionary<Bee.BeeLable, Bee> Bees { get => _bees; set => _bees = value; }
    public double MaxTime {
        get
        {
            foreach (var item in Bees)
                if (item.Value.CalculatedArrivalTime > _maxTime)
                    _maxTime = item.Value.CalculatedArrivalTime;
            return _maxTime;
        }
    }

    public float CurrentTime { get => _currentTime; set => _currentTime = value; }
    public bool Simulate { get => _simulate; set => _simulate = value; }
    public SimulationSystem SimulationType { get => _simulationType; set => _simulationType = value; }

    public SimulationData(Bee[] bees, SignalBus signalBus)
    {
        foreach (Bee bee in bees)
            _bees.Add(bee.Lable, bee);

        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _signalBus.GetStream<CarleyCorpSignal>()
            .Where(x => x.CarleyCorpEvent == CarleyCorpEvent.Play)
            .Subscribe(x => StartSimulation())
            .AddTo(_disposables);

        _signalBus.GetStream<CarleyCorpSignal>()
            .Where(x => x.CarleyCorpEvent == CarleyCorpEvent.Reset)
            .Subscribe(x => ResetSimulation())
            .AddTo(_disposables);

        _signalBus.GetStream<CarleyCorpSignal>()
            .Where(x => x.CarleyCorpEvent == CarleyCorpEvent.UnitySimulation)
            .Subscribe(x =>
            {
                SimulationType = SimulationSystem.Unity;
                ResetSimulation();
            })
            .AddTo(_disposables);

        _signalBus.GetStream<CarleyCorpSignal>()
            .Where(x => x.CarleyCorpEvent == CarleyCorpEvent.MySimulation)
            .Subscribe(x =>
            {
                SimulationType = SimulationSystem.Calculated;
                ResetSimulation();
            })
            .AddTo(_disposables);

        _signalBus.GetStream<CarleyCorpSignal>()
            .Where(x => x.CarleyCorpEvent == CarleyCorpEvent.Manual)
            .Subscribe(x =>
            {
                SimulationType = SimulationSystem.Manual;
                ResetSimulation();
            })
            .AddTo(_disposables);
    }

    public string AnswerQuestionTwo()
    {
        double minTime = MaxTime;
        Bee fastestBee = Bees[Bee.BeeLable.a];
        Bee bee1 = Bees[Bee.BeeLable.a];
        Bee bee2 = Bees[Bee.BeeLable.b];
        float distance = 0;

        //Hold fastest Bee
        foreach (var item in Bees)
        {
            if (item.Value.CalculatedArrivalTime < minTime)
            {
                minTime = item.Value.CalculatedArrivalTime;
                fastestBee = item.Value;
            }
        }

        //Simulate bees at 2 seconds before first Bee arrives
        foreach (var item in Bees)
            item.Value.ManualPosition(minTime - 2);


        foreach(var i in Bees)
        {
            foreach(var j in Bees)
            {
                //if the bees are in the exact same position the data is discarded
                if (Vector3.Distance(i.Value.transform.position, j.Value.transform.position) == 0)
                    continue;

                //if the distance is lower than the previous set of bees overrite the bees, will provide answer at end of loop
                if (Vector3.Distance(i.Value.transform.position, j.Value.transform.position) < Vector3.Distance(bee1.transform.position, bee2.transform.position))
                {
                    bee1 = i.Value;
                    bee2 = j.Value;
                    distance = Vector3.Distance(bee1.transform.position, bee2.transform.position);
                }
            }
        }

        foreach(var item in Bees)
        {
            item.Value.Reset();
        }

        return("Bee " + bee1.Lable.ToString().ToUpper() + " and Bee " + bee2.Lable.ToString().ToUpper() + 
            " are closest to each other 2 seconds before the fastest Bee " + fastestBee.Lable.ToString().ToUpper() + 
            " reaches the flower. The distance between them is " + distance + " Meters");
    }

    public void StartSimulation()
    {
        Simulate = true;
    }

    public void ResetSimulation()
    {
        Simulate = false;
        foreach (var item in Bees)
            item.Value.Reset();
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }
}