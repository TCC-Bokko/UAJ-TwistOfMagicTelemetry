using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ISerializer : MonoBehaviour {
    abstract public string serialize(TrackerEvent t_event);
    abstract public bool persistance(string eventTrace, TrackerEvent tE);
}
