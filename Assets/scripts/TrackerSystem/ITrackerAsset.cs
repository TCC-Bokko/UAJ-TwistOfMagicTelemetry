using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITrackerAsset : MonoBehaviour
{
    //CLASE ABSTRACTA DE TRACKERS CON LA CUAL IMPLEMENTAR:
    //subTracker levelTracker
    //subTracker playerTracker
    //subTracker generalTracker

    public bool accept(TrackerEvent t_event)
    {
        bool isValid = false;
        isValid = checkValidity(t_event);
        return isValid;
    }

    public bool checkValidity(TrackerEvent t_event) {
        return true;
    }
}
