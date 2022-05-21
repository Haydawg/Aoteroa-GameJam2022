using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void AnimationEvent();

public class AnimationEvents : MonoBehaviour
{
    public delegate void AnimationEvent();
    public static event AnimationEvent RunEvent;
    

    public void PlayEvent()
    {
        RunEvent();
    }
}

