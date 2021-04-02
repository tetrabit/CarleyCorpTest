using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;

public class ManualSlider : MonoBehaviour
{
    private Slider _slider;
    private SignalBus _signalBus;
    private SimulationData _simulationData;

    [Inject]
    private void Construct(SignalBus signalBus, SimulationData simulationData)
    {
        _signalBus = signalBus;
        _simulationData = simulationData;
    }

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.interactable = false;
    }

    void Start()
    {
        _slider.maxValue = (float)_simulationData.MaxTime;
        _slider.minValue = 0;

        _signalBus.GetStream<CarleyCorpSignal>()
            .Where(x => x.CarleyCorpEvent == CarleyCorpEvent.Manual)
            .Subscribe(x =>
            {
                _slider.interactable = true;
                Reset();
            })
            .AddTo(gameObject);

        _signalBus.GetStream<CarleyCorpSignal>()
            .Where(x => x.CarleyCorpEvent != CarleyCorpEvent.Manual && x.CarleyCorpEvent != CarleyCorpEvent.Reset && x.CarleyCorpEvent != CarleyCorpEvent.Play)
            .Subscribe(x =>
            {
                _slider.interactable = false;
                Reset();
            })
            .AddTo(gameObject);

        _signalBus.GetStream<CarleyCorpSignal>()
            .Where(x => x.CarleyCorpEvent == CarleyCorpEvent.Reset && _simulationData.SimulationType == SimulationData.SimulationSystem.Manual && x.CarleyCorpEvent != CarleyCorpEvent.Play)
            .Subscribe(x =>
            {
                Reset();
            })
            .AddTo(gameObject);
    }

    public void SetTime(float time)
    {
        _simulationData.CurrentTime = time;
    }

    private void Reset()
    {
        _slider.value = 0;
    }
}
