using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class CarleyCorpButton : MonoBehaviour, IPointerClickHandler
{
    private SignalBus _signalBus;
    [SerializeField]
    CarleyCorpEvent _event;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _signalBus.Fire(new CarleyCorpSignal(_event));
    }
}
