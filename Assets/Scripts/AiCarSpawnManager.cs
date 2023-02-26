using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiCarSpawnManager : MonoBehaviour
{
    public Transform[] spawnTransforms;
    public static Transform[] spawnPoints;

    private void Start()
    {
        spawnPoints = spawnTransforms;
    }
}
