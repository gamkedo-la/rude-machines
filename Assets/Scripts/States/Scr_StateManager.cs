using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_StateManager : MonoBehaviour
{
    public Scr_AIController controller = null;

    [System.Serializable]
    public struct StateDetectorPair
    {
        public Scr_State state;
        public Scr_Detect detector;
    }
    public List<StateDetectorPair> stateDetectorPairs = new List<StateDetectorPair>();
    public Scr_State defaultState = null;

    private Scr_State currentState = null;

    void Start()
    {
        if(controller == null)
            controller = GetComponent<Scr_AIController>();

        foreach(var pair in stateDetectorPairs) pair.state.StateTerminate();
        defaultState.StateInitialize();
        currentState = defaultState;
    }

    void SetCurrentState(Scr_State state)
    {
        currentState.StateTerminate();
        state.StateInitialize();
        currentState = state;
    }

    void Update()
    {
        bool detectionFound = false;
        foreach(var pair in stateDetectorPairs)
        {
            if(pair.detector != null && pair.detector.detectedTarget != null)
            {
                SetCurrentState(pair.state);
                detectionFound = true;
                break;
            }
        }

        if(!detectionFound)
            SetCurrentState(defaultState);
    }
}
