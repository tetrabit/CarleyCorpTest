using UnityEngine;
using Zenject;

public class Bee : MonoBehaviour
{
    public enum BeeLable
    {
        a,
        b,
        c,
        d
    }

    [SerializeField]
    private BeeLable _lable;
    [SerializeField]
    private float _speed = 1f;
    private Vector3 _initialPosition;
    private Flower _flower;
    private SimulationData _simulationData;
    private float _initialDistance;
    private double _calculatedArrivalTime;
    private float _distanceToFlower;
    private double _simulatedArrivalTime;
    private bool _arrived;
    private float _currentTime = 0;

    public BeeLable Lable { get => _lable; set => _lable = value; }
    public float DistanceToFlower { get => _distanceToFlower; set => _distanceToFlower = value; }
    public double SimulatedArrivalTime { get => _simulatedArrivalTime; set => _simulatedArrivalTime = value; }
    public double CalculatedArrivalTime { get => _calculatedArrivalTime; set => _calculatedArrivalTime = value; }
    public float CurrentTime { get => _currentTime; set => _currentTime = value; }

    [Inject]
    private void Construct(Flower flower, SimulationData simulationData)
    {
        _flower = flower;
        _simulationData = simulationData;
    }

    private void Awake()
    {
        transform.LookAt(_flower.transform);
        _initialPosition = transform.position;
        _initialDistance = Vector3.Distance(transform.position, _flower.transform.position);
        _calculatedArrivalTime = _initialDistance / _speed;
    }

    private void Update()
    {
        _distanceToFlower = Vector3.Distance(transform.position, _flower.transform.position);
        if(_simulationData.Simulate)
        {
            _currentTime += Time.deltaTime;
            _simulationData.CurrentTime = _currentTime;

            switch (_simulationData.SimulationType)
            {
                case SimulationData.SimulationSystem.Unity:
                    SimulatedPositionUnityFunctions();
                    break;
                case SimulationData.SimulationSystem.Calculated:
                    SimulatedPosition();
                    break;
                default:
                    SimulatedPosition();
                    break;
            }

            if (!_arrived && _distanceToFlower == 0)
            {
                _simulatedArrivalTime = _currentTime;
                _arrived = true;
            }
        }

        if (_simulationData.SimulationType == SimulationData.SimulationSystem.Manual)
            ManualPosition();
    }

    private void SimulatedPositionUnityFunctions()
    {
        transform.position = Vector3.MoveTowards(transform.position, _flower.transform.position, _speed * Time.deltaTime);
    }

    private void SimulatedPosition()
    {
        transform.position = RatioBasedPosition(_initialPosition, _flower.transform.position, (float)(_currentTime / CalculatedArrivalTime));
        if (_currentTime > CalculatedArrivalTime)
        {
            transform.position = RatioBasedPosition(_initialPosition, _flower.transform.position, 1);
        }
        else if (_currentTime < 0)
            transform.position = RatioBasedPosition(_initialPosition, _flower.transform.position, 0);
    }

    private void ManualPosition()
    {
        transform.position = RatioBasedPosition(_initialPosition, _flower.transform.position, (float)(_simulationData.CurrentTime / CalculatedArrivalTime));
        if (_simulationData.CurrentTime > CalculatedArrivalTime)
            transform.position = RatioBasedPosition(_initialPosition, _flower.transform.position, 1);
        else if (_simulationData.CurrentTime < 0)
            transform.position = RatioBasedPosition(_initialPosition, _flower.transform.position, 0);
    }

    public void ManualPosition(double time)
    {
        transform.position = RatioBasedPosition(_initialPosition, _flower.transform.position, (float)(time / CalculatedArrivalTime));
        if (time > CalculatedArrivalTime)
            transform.position = RatioBasedPosition(_initialPosition, _flower.transform.position, 1);
        else if (time < 0)
            transform.position = RatioBasedPosition(_initialPosition, _flower.transform.position, 0);
    }

    private Vector3 RatioBasedPosition(Vector3 point1, Vector3 point2, float t)
    {
        float x = (point2.x - point1.x) * t + point1.x;
        float y = (point2.y - point1.y) * t + point1.y;
        float z = (point2.z - point1.z) * t + point1.z;
        return new Vector3(x, y, z);
    }

    public void Reset()
    {
        transform.position = _initialPosition;
        _currentTime = 0;
        _arrived = false;
        _simulatedArrivalTime = 0;
        _simulationData.CurrentTime = 0;
    }
}
