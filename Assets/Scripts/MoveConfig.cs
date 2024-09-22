using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveConfig", menuName = "Data/MoveConfig")]
public class MoveConfig : ScriptableObject
{
    [SerializeField] private float _Speed;
    public float Speed { get => _Speed; }
    
    [SerializeField] private List<MovingButtonData> _DataOfMovingButtons = new();
    public MovingButtonData this[int index]
    {
        get => _DataOfMovingButtons[index];
    }
    public int MovingButtonsAmount { get => _DataOfMovingButtons.Count; }
}