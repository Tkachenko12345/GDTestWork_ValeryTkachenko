using System;
using UnityEngine;

[Serializable]
public class OneSpawnedObjectData
{
    [SerializeField] private GameObject _ObjectTemplate;
    public GameObject ObjectTemplate { get => _ObjectTemplate; }

    [SerializeField] private Vector3 _StartOffset;
    public Vector3 StartOffset { get => _StartOffset; }

    [SerializeField] private Vector3 _MotionDisplacement;
    public Vector3 MotionDisplacement { get => _MotionDisplacement; }

    [SerializeField] private float _DisplacementTime;
    public float DisplacementTime { get => _DisplacementTime; }
}