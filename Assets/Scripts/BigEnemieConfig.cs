using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BigEnemieConfig", menuName = "Data/BigEnemieConfig")]
public class BigEnemieConfig : ScriptableObject
{
    [SerializeField] private List<OneSpawnedObjectData> _DataAboutObjectsSpawnedOnDeath = new();

    public int SpawnedObjectsAmount { get => _DataAboutObjectsSpawnedOnDeath.Count; }

    public OneSpawnedObjectData this[int index]
    {
        get => _DataAboutObjectsSpawnedOnDeath[index];
    }
}