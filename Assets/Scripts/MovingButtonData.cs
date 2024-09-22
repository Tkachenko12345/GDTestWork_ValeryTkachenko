using System;
using UnityEngine;

[Serializable]
public class MovingButtonData
{
    [SerializeField] private string _ButtonName;
    public string ButtonName { get => _ButtonName; }

    [SerializeField] private Vector3 _Direction;
    public Vector3 Direction { get => _Direction; }
}