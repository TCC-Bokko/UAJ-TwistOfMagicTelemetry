using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour
{
    // VARIABLES ///////////////////////////////////////////////////////////////////////
    // Proceso del singleton
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static MonoBehaviour trackerInstance;

    // Objetos de persistencia
    private IPersistence persistenceObject;

    // Lista de trackers activos (progression, resource, etc.)
    private List<ITrackerAsset> activeTrackers;

    // METODOS ///////////////////////////////////////////////////////////////////////
    //Para que no se destruya entre escenas
    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Init
    void Start()
    {
        // Genera el singleton.
        this.Instance();

        // TO DO
        // Enlazado de otras clases segun tipo (ItrackerAsset, IPersistence) ?
    }

    // Instanciado
    MonoBehaviour Instance() {
        if(m_ShuttingDown){
            Debug.LogWarning("[Singleton] Instancia '" + typeof(MonoBehaviour) +
                "' destruida. Devolviendo null.");
            return null;
        }

        lock (m_Lock)
        {
            if (trackerInstance == null)
            {
                // Search for existing instance.
                trackerInstance = (MonoBehaviour)FindObjectOfType(typeof(MonoBehaviour));

                // Create new instance if one doesn't already exist.
                if (trackerInstance == null)
                {
                    // Need to create a new GameObject to attach the singleton to.
                    var singletonObject = new GameObject();
                    trackerInstance = singletonObject.AddComponent<MonoBehaviour>();
                    singletonObject.name = typeof(MonoBehaviour).ToString() + " (Singleton)";

                    // Make instance persistent.
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return trackerInstance;
        }
    }

    void TrackEvent() {
        // WORK TO DO
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
