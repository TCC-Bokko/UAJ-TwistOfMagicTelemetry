using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : ITrackerAsset
{
    // Tracker que gestiona eventos de jugador (Ver documento diseño)
    // Diseñado para ser colocado como script del objeto a trackear y que lance eventos del tipo propio cuando sea necesario

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override bool checkValidity(TrackerEvent t_event)
    {
        bool isValid = false;
        TrackerEvent.EventType t_eventType = t_event.getType();

        if (t_eventType == TrackerEvent.EventType.DEAD)
            isValid = true;
        if (t_eventType == TrackerEvent.EventType.IDLE_MANA1)
            isValid = true;
        if (t_eventType == TrackerEvent.EventType.PLAYER_POSITION)
            isValid = true;

        return isValid;
    }
}
