using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<InitializableBehaviour> Initializables = new List<InitializableBehaviour>();

    void Awake()
    {
        foreach(var behaviour in Initializables)
        {
            behaviour.Init();
        }
    }
}
