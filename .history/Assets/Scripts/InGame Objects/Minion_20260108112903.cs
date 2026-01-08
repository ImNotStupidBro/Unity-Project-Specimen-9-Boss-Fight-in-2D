using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum wellnessState
{
    Vulnerable = 0,
    Invulnerable = 1,
    Hurt = 2,
    Dead = 3
}

public class Minion : MonoBehaviour
{
    private wellnessState currentState = wellnessState.Vulnerable;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
