using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTracker : ITrackerAsset
{
    // Tracker que gestiona eventos del nivel (Ver documento diseño)
    // Diseñado para ser colocado como script del objeto a trackear y que lance eventos del tipo propio cuando sea necesario

    // Start is called before the first frame update

    // Se podría separar las colas para trabajar con ellas independientemente, aunque trabajaremos con todos los eventos en la misma


    public override bool checkValidity(TrackerEvent t_event)
    {
        bool isValid = false;
        TrackerEvent.EventType t_eventType = t_event.getType();

        if (t_eventType == TrackerEvent.EventType.LEVEL_START)
            isValid = true;
        if (t_eventType == TrackerEvent.EventType.LEVEL_END)
            isValid = true;
        if (t_eventType == TrackerEvent.EventType.LEVEL_COMPLETED)
            isValid = true;
        if (t_eventType == TrackerEvent.EventType.CHECKPOINT)
            isValid = true;

        return isValid;
    }
}
