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
    private int HP = 3;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
