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
    //Tracker trackerNivel
    //Tracker trackerJugador
    //Tracker trackerGeneral

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

        // Enviar eventos de inicio de sesión junto con parámetros
        // que pueden aportar especificaciones adicionales: plataforma, SO, país, información
        // demográfica(fecha de nacimiento, sexo, id de alguna red social. . . ).

        // TO DO
        // Enlazado de otras clases segun tipo (ItrackerAsset, IPersistence) ?
        
        // Llenar lista de trackers

    }

    void TrackEvent() {
        // WORK TO DO
        // Pasar el evento a persistencia
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
