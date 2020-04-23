using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    // VARIABLES ///////////////////////////////////////////////////////////////////////
    // Proceso del singleton
    public static Tracker instance;

    // Objetos de persistencia
    private IPersistence persistenceObject; // IPersistence se comunica con ISerializer

    // Lista de trackers activos (progression, resource, etc.)
    private List<ITrackerAsset> activeTrackers;
    //Aqui dentro irán:
    //Tracker trackerNivel
    //Tracker trackerJugador
    //Tracker trackerGeneral

    // Lista de eventos
    // en esta implementación pasamos todos los eventos validos a la misma lista
    // sin embargo sería muy facil dividirlos por cada tracker (general, jugador y nivel) 
    // en el momento de validarlos.
    private List<TrackerEvent> eventList;
    int eventsInQueue = 0;


    // METODOS ///////////////////////////////////////////////////////////////////////
    //Para que no se destruya entre escenas
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Init con instanciado
    void Start()
    {
        // Genera el singleton.
        instance = this;

        // Enlazado de otras clases segun tipo
        persistenceObject = new IPersistence();
        // Llenar lista de trackers
        ITrackerAsset trackerNivel = new LevelTracker();
        activeTrackers.Add(trackerNivel);
        ITrackerAsset trackerJugador = new PlayerTracker();
        activeTrackers.Add(trackerJugador);
        ITrackerAsset trackerGeneral = new GeneralTracker();
        activeTrackers.Add(trackerGeneral);
    }

    void TrackEvent(TrackerEvent t_event) {
        bool accepted = false;
        foreach (var tracker in activeTrackers)
        {
            if (tracker.accept(t_event))
                accepted = true;
        }

        if (accepted) {
            eventList.Add(t_event);
            eventsInQueue++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Cada X tiempo o cantidad de eventos...
        if (eventsInQueue >= 10) {
            persistenceObject.send(eventList);
            eventList.Clear();
        }
    }
}
