using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralTracker : ITrackerAsset
{
    // Tracker que gestiona eventos generales (Ver documento diseño)
    // Diseñado para ser colocado como script del objeto a trackear y que lance eventos del tipo propio cuando sea necesario

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool checkValidity(TrackerEvent t_event) {
        bool isValid = false;
        TrackerEvent.EventType t_eventType = t_event.getType();

        if (t_eventType == TrackerEvent.EventType.SESSION_START)
            isValid = true;
        if (t_eventType == TrackerEvent.EventType.SESSION_END)
            isValid = true;

        return isValid;
    }
}
