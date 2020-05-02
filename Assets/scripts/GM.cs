﻿using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public static GM instance;
    public GameObject player;           //Definimos que GO es el jugador.
    public GameObject gemas;
    public GameObject migImagen;
    private GameObject nivel;
    private Rigidbody2D rb;
    private Animator animGemas;
    public Vector2 respawn;             //Lugar donde respawneara el jugador tras morir, normalmente el último portal atravesado.
    public Vector2 inicioNivel;         //Lugar donde el jugador debe empezar el nivel (Para recargar), quiza innecesario si usamos reload
    private int[] actglif;              //Array que guarda en cada posición la cantidad de antorchas necesarias a activar para activar el glifo correspondiente de Glifos[]
    public int antorchasActivadas;      //Número de antorchas que activa el jugador en cada nivel (importante para glifos) importante resetear a 0 al cambiar de escena.
    public int manamax = 200;           //Definimos el máximo de mana disponible para el jugador.
    public int manamin = 0;             //Definimos el mana mínimo que el jugador puede tener. 
    public int mana = 200;              //Cantidad de mana disponible.
    public int SegXManaTel = 1;         //Consumo de Telequinesis por tick de tiempo (para variar cada cuantos segundos se resta esta cantidad ver UPDATE->GESTION DE MANA: Telequinesis)
    public int SegXManaAgua = 1;        //Consumo de andar sobre agua por tick de tiempo (para variar cada cuantos segundos se resta esta cantidad ver UPDATE->GESTION DE MANA: Andar Sobre Agua)
    public int consumBFuego = 1;        //Consumo de bola de fuego
    public int segXRecMana = 1;         //Velocidad a la que se recupera mana en segundos.
    public int segXBajoagua;            //cantidad de segundos (bajo el agua) en quitar una unidad.
    public bool enAire;                 //Comprueba si estamos en el aire
    public bool enAgua = false;                 //Comprueba si estamos en el agua
    public bool herido;                 //Comprueba si estamos heridos (-Sugerencia: Cambiar a invulnerable)
    public bool mirDerecha = false;     //Define hacia donde esta mirando el jugador.
    public bool mostrarCrono;           // Mostrar el crono o no segun la escena.
    public bool andando;                //Controla si el jugador se esta moviemdo
    public bool cajaHundida;            // controla que las cajas no tienen impulso hacia arriba al desactivar telequinesis
    public bool mig = false;            // Menu In Game
    private bool waterWalkActivo;       //Comprueba si el poder de andar sobre el agua esta activo.
    private bool telequinesisActivo;    //Comprueba si el poder Telequinesis esta activo.
    private bool curando = false;
    private bool restando = false;
    private bool mute = false;
    public float recuperado;            //Valor que debe alcanzar cura para considerar al jugador "curado" (Fin invulnerabilidad y permite movimiento). Ver GESTION HERIDO.
    public float velcura = 1;           //Velocidad a la que se cura el personaje, más velocidad con valores >1, menor velocidad con valores <1. Ver GESTION HERIDO.
    public float cura;                  //Valor que se acumula con el tiempo y velcura. Ver GESTION HERIDO.
    public float tiempo;                // Cronometro del nivel
    public UnityEngine.UI.Text temporizador;            //Para controlar el crono.
    public static bool firsttime = true;
    public static float time;
    string textotiempo;
    public int numeroNivel;
    private static int sessionID = 0;
    //-----------TRACKER---------------
    bool startPlay = false;
    public static Tracker TrackerInstance;
    private string sessionFileExt;  
    private float idleManaTime;     //Tiempo que lleva regenerando mana.
    private float lastCureTime;     //Tiempo desde la última vez que se curo mana
    public enum serializacion { CSV, JSON, Binario }
    public serializacion Tipo_Serialización;
    //Opcional, track de posición de jugador.
    public float playerXtrack;
    public float playerYtrack;
    public float timeXplayerTrack = 5f;
    private float lastPlayerTrack;

    public int getSession()
    {
        return sessionID;
    }

    public void setSession(int s)
    {
        sessionID = s;
    }

    public void LoadSessionID() { 
        string fullpath = Application.dataPath + "/Sessionlocator.txt";
        if (!File.Exists(fullpath))
        {
            File.Create(fullpath).Close();
        }
        else
        {
            StreamReader r = new StreamReader(fullpath);
            sessionID = int.Parse(r.ReadLine());
            instance.setSession(sessionID);
            r.Close();
        }
        File.WriteAllText(fullpath, "");
        StreamWriter w = new StreamWriter(fullpath);
        int aux = sessionID + 1;
        w.Write(aux);
        w.Close();
    }

    //--------------------------

    //Array de glifos, mover aqui todos los que existan en escena para que sean manejados.
    private void Awake()
    { 
    }
    void Start()
    {
        //Inicialización del tracker
        TrackerInstance = Tracker.getInstance();
        idleManaTime = 0.0f;
        lastPlayerTrack = 0.0f;

        //Variables de Juego
        cajaHundida = false;
        animGemas = gemas.GetComponent<Animator>();
        mostrarCrono = false;
        tiempo = 0;
        rb = player.GetComponent<Rigidbody2D>();
        mana = manamax;
        waterWalkActivo = false;
        telequinesisActivo = false;
        herido = false;
        cura = 0;
        nivel = GameObject.FindGameObjectWithTag("Nivel");

        //AL INICIO SITUAR PUNTO DE SPAWN DE INICIO DEL NIVEL
        if (SceneManager.GetActiveScene().name == "Biblioteca")
        {
            respawn.x = 8.5f;
            respawn.y = -10f;
        }
        else if (SceneManager.GetActiveScene().name == "NivelTutorial")
        {
            respawn.x = 5.5f;
            respawn.y = -18f;
        }
        else if (SceneManager.GetActiveScene().name == "Nivel1")
        {
            respawn.x = 5.59f;
            respawn.y = -37.01f;
        }
        else if (SceneManager.GetActiveScene().name == "Nivel2")
        {
            respawn.x = 7.23f;
            respawn.y = -6f;
        }
        else if (SceneManager.GetActiveScene().name == "Nivel3")
        {
            respawn.x = 3f;
            respawn.y = -42f;
        }
        else if (SceneManager.GetActiveScene().name == "Nivel4")
        {
            respawn.x = 4f;
            respawn.y = -22f;
        }
        else if (SceneManager.GetActiveScene().name == "Flashback1")
        {
            respawn.x = 1.5f;
            respawn.y = -22f;
        }

        instance = this;
    }

    void Update()
    {
        //gestion mana gui
        animGemas.SetFloat("mana", mana);

        //GESTION DE MANA: ANDAR SOBRE EL AGUA
        if (waterWalkActivo == true)
        {
            if (!restando)
            {
                restando = true;
                Invoke("restaManaTiempoAqu", SegXManaAgua);                     //resta la cantidad de mana definida por consumAgua cada 1 segundos.
                Debug.Log("EstaRestando");
            }
            if (mana <= 0)
            {
                waterWalkActivo = false;
            }
        }
        //GESTION DE MANA: BAJO AGUA
        if (enAgua)
        {
            if (!restando)
            {
                restando = true;
                Invoke("restaManaTiempoAqu", segXBajoagua);                     //resta la cantidad de mana definida por consumAgua cada 1 segundos.
            }
            if (mana <= 0)
            {
                enAgua = false;
            }
        }
        //GESTION DE MANA: TELEQUINESIS
        if (telequinesisActivo == true)
        {
            if (!restando)
            {
                restando = true;
                Invoke("restaManaTiempoTel", SegXManaTel);
                Debug.Log("EstaRestando");
            }
        }

        //GESTION DEL CRONO
        if (!mig)
        {
            tiempo += Time.deltaTime;
            transformaTexto(out textotiempo);
            temporizador.text = textotiempo;
        }

        // GESTION DEL MENU PAUSA
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!mig)
            {
                player.GetComponent<Movimiento>().enabled = false;
                migImagen.SetActive(true);
                mig = true;
            }
            else
            {
                player.GetComponent<Movimiento>().enabled = true;
                migImagen.SetActive(false);
                mig = false;
            }

        }

        // GESTION DE ORIENTACION DE SPRITES
        if (Movimiento.instance.mHorizontal > 0)
            mirDerecha = true;
        if (Movimiento.instance.mHorizontal < 0)
            mirDerecha = false;

        /*// GESTION DE ACTIVACION DE GLIFOS
        for (int i = 0; i < Glifos.Length; i++){
            if (antorchasActivadas >= 4)
                Glifos [0].GetComponent<Glifo> ().activado = true;
            if (antorchasActivadas >= 12)
                Glifos [1].GetComponent<Glifo> ().activado = true;
        }*/

        //GESTION DE MANA: MUERTE (LIMITE INFERIOR)
        if (mana == 0)
        {
            Muerte();
        }

        if (herido)
        {
            cura += Time.deltaTime * velcura;                       //Cura empieza en 0, acumula valor segun el tiempo.
            if (cura >= recuperado)
            {                               //Cuando cura sea mayor que el de Recuperado (tiempo que tarde)...
                cura = 0;                                           //resetea el valor de cura.
                herido = false;                                     //pone el estado herido a false.
                player.GetComponent<Movimiento>().enabled = true;  //Devolver el movimiento al jugador.
                player.GetComponent<Animation>().Stop("FlasheoDaño");
                devuelveColor();
            }
        }

        // GESTION DE MANA: Recuperación.
        //Si el mana es mayor que el minimo y menor que el máximo
        if (mana > manamin && mana < manamax)
        {
            //Si no esta activado "curando" mana
            if (!curando && !restando)
            {
                //Tracker
                idleManaTime = 0.0f;
                curando = true;
            }

            if (curando) { 
                //Actualizar idleManatime
                idleManaTime += Time.deltaTime;

                // Si ha pasado el tiempo minimo de curación, aumenta en 1 el mana.
                lastCureTime += Time.deltaTime;
                if (lastCureTime > segXRecMana)
                {
                    lastCureTime = 0.0f;
                    mana++;
                    if (mana == manamax) curandoFin();
                }
            }
        }

        if (mana > manamax) {
            mana = manamax;
            if (curando) curandoFin(); //Pone curando a false
        }
        //--------------------------------TRACKER---------------------------
        lastPlayerTrack += Time.deltaTime;
        if (lastPlayerTrack > timeXplayerTrack) {
            //Pillamos posición de jugador
            playerXtrack = player.transform.position.x;
            playerYtrack = player.transform.position.y;
            if (startPlay) {
                //Mandamos evento
                TrackerEvent playerPosTrack = new EventPosition(playerXtrack, playerYtrack);
                TrackerInstance.TrackEvent(playerPosTrack);
            }
            lastPlayerTrack = 0.0f;
        }

        TrackerInstance.Update();

        //------------------------------------------------------------------
    }   // FIN UPDATE


    //METODOS LLAMADOS DESDE EL UPDATE Y EXTERNOS

    //transforma el tiempo a minutos y segundos.
    void transformaTexto(out string texto)
    {
        int min = (int)tiempo / 60;
        int seg = (int)tiempo % 60;
        texto = min.ToString() + ":" + seg.ToString();
    }

    //GESTION HERIDO
    public void Herido(int dirX, int dirY)
    {
        player.GetComponent<Animation>().Play("FlasheoDaño");
        knockback(rb, dirX, dirY);        //Llamamos al knockback para el jugador
        player.GetComponent<Movimiento>().enabled = true;
    }

    //KNOCKBACK
    public void knockback(Rigidbody2D jug, int dirX, int dirY)
    {
        jug.velocity = new Vector2(8f * dirX, 7f * dirY);
    }

    //DEVOLVER COLOR BÁSICO AL JUGADOR
    public void devuelveColor()
    {
        SpriteRenderer renderer = player.GetComponent<SpriteRenderer>();
        renderer.color = new Color(1f, 1f, 1f, 1f);
    }

    //METODO QUE MODIFICA EL MANA (ENEMIGOS)
    public void restarMana(int cantidad)
    {
        mana -= cantidad;
        if (curando) curandoFin();
    }

    public void curandoFin()
    {
        if (idleManaTime > 1.5f) { 
            TrackerEvent idleMana = new EventIdleMana1(idleManaTime);
            TrackerInstance.TrackEvent(idleMana);
        }
        idleManaTime = 0.0f;
        curando = false;
    }

    public void restandoFin()
    {
        restando = false;
    }

    //METODO QUE MODIFICA EL MANA (CURARSE 1 PUNTO)
    /*
    public void curaManaXTiempo()
    {
        //Cura 1 por frame.
        if (!waterWalkActivo && !telequinesisActivo && !enAgua) {
            mana++;
        }
    }
    */

    /*	//METODO QUE MODIFICA EL MANA (CURARSE 1 PUNTO)
        public void manaXtiempo (){
            mana++;
        }*/

    //METODO QUE MODIFICA EL MANA (ANDARSOBREAGUA)
    public void restaManaTiempoAqu()
    {
        mana -= 1;
        restando = false;
        if (curando) curandoFin();
    }

    //METODO QUE MODIFICA EL MANA (TELEQUINESIS)
    public void restaManaTiempoTel()
    {
        mana -= 1;
        restando = false;
        if (curando) curandoFin();
    }

    //METODO QUE MODIFICA EL MANA (BOLA FUEGO)
    public void Disparo()
    {
        mana -= consumBFuego;
        if (curando) curandoFin();
    }

    //GESTION DE TELEQUINESIS: ACTIVA HABILIDAD
    public void TelequinesisActiva()
    {
        telequinesisActivo = true;
        player.GetComponent<Disparo>().enabled = false;
    }

    //GESTION DE TELEQUINESIS: DESACTIVA HABILIDAD
    public void TelequinesisDesactivada()
    {
        telequinesisActivo = false;
        if (herido)
            herido = false;
        player.GetComponent<Disparo>().enabled = true;

    }

    //GESTION DE ANDAR POR AGUA: ACTIVA HABILIDAD
    public void WaterWalkActivo()
    {
        waterWalkActivo = true;
        player.GetComponent<ActivaTelequinesis>().enabled = false;
        player.GetComponent<Disparo>().enabled = false;
    }

    //GESTION DE ANDAR POR AGUA: DESACTIVA HABILIDAD
    public void WaterWalkDesactivado()
    {
        waterWalkActivo = false;
        player.GetComponent<ActivaTelequinesis>().enabled = true;
        player.GetComponent<Disparo>().enabled = true;
    }

    //GESTION DE MUERTE DEL PERSONAJE (MANA = 0)
    public void Muerte()
    {
        TrackerEvent playerDeath = new EventPlayerDead(player.GetComponent<Transform>().position.x, player.GetComponent<Transform>().position.y);
        TrackerInstance.TrackEvent(playerDeath);
        mana = -1;
        player.GetComponent<Movimiento>().enabled = false;
        player.GetComponent<Disparo>().enabled = false;
        player.GetComponent<Animator>().SetBool("muerto", true);                    //Activar animación desmayarse.
        SM.instance.muerte();
        Invoke("Respawn", 2);
     
    }

    //GESTION DEL RESPAWN DEL PERSONAJE
    void Respawn()
    {
        player.transform.position = respawn;                                    //transportarlo a la posición del último respawn (portal) que ha cruzado
        Invoke("Prepararse", 1);

    }

    public void Prepararse()
    {
        player.GetComponent<Animator>().SetBool("muerto", false);
        player.GetComponent<Movimiento>().enabled = true;
        player.GetComponent<Disparo>().enabled = true;
        mana = manamax;
        tiempo += 30;                                                           //Penalizar al jugador con tiempo agregado al cronometro
    }

    //AJUSTAR RESPAWN AL CARGAR NUEVO NIVE
    void OnLevelWasLoaded(int numLvl)
    {  //Cuando se acaba de cargar un nivel, recolocar el respawn
        numeroNivel = numLvl;
        if (!startPlay)
        {
            LoadSessionID();
            TrackerEvent sesionStart = new EventSesionStart();
            TrackerInstance.TrackEvent(sesionStart);
            startPlay = true;
        }
        if (numLvl == 1)
        { //biblioteca
            TrackerEvent levelStart = new EventLevelStart(1);
            TrackerInstance.TrackEvent(levelStart);
            respawn.x = 8.5f;
            respawn.y = -10f;
        }
        else if (numLvl == 2)
        { //tutorial
            TrackerEvent levelStart = new EventLevelStart(2);
            TrackerInstance.TrackEvent(levelStart);
            respawn.x = 5.5f;
            respawn.y = -18f;
        }
        else if (numLvl == 3)
        { //Nivel 1
            TrackerEvent levelStart = new EventLevelStart(3);
            TrackerInstance.TrackEvent(levelStart);
            respawn.x = 5.59f;
            respawn.y = -37.01f;
        }
        else if (numLvl == 4)
        { //Nivel 2
            TrackerEvent levelStart = new EventLevelStart(4);
            TrackerInstance.TrackEvent(levelStart);
            respawn.x = 7.23f;
            respawn.y = -6f;
        }
        else if (numLvl == 5)
        { //Nivel 3
            TrackerEvent levelStart = new EventLevelStart(5);
            TrackerInstance.TrackEvent(levelStart);
            respawn.x = 3f;
            respawn.y = -42f;
        }
        else if (numLvl == 6)
        { //Nivel 4
            TrackerEvent levelStart = new EventLevelStart(6);
            TrackerInstance.TrackEvent(levelStart);
            respawn.x = 4f;
            respawn.y = -22f;
        }
    }

    /// <summary>
    /// GUI-MENUS: INTERACCION CON RATON
    /// </summary>
    
    public void OnJugarClick()
    {
        Debug.Log("GM.OnJugarClick()");
        //LoadSessionID();
        Invoke("empiezaJuego", 1f);
        SM.instance.clickOpcion();
    }

    //MENU ESC-> SALIR
    public void OnSalirClick()
    {
        Debug.Log("GM.OnSalirClick()");
        TrackerEvent sesionEnd = new EventSesionEnd();
        TrackerInstance.TrackEvent(sesionEnd);

        //Salir del juego
        #if UNITY_EDITOR
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void OnCreditosClick()
    {
        SceneManager.LoadScene(7); // Cargar creditos
    }

    //Reinicia el nivel
    public void OnReiniciarClick()
    {
        string nombre = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(nombre);
        mig = false;

    }

    //Sale del nivel y devuelve el jugador al selector de niveles (biblioteca)
    public void OnBibliotecaClick()
    {
        TrackerEvent levelEnd = new EventLevelEnd(GM.instance.numeroNivel);
        TrackerInstance.TrackEvent(levelEnd);
        SceneManager.LoadScene(1);
    }

    public void OnMuteClick()
    {
        nivel = GameObject.FindGameObjectWithTag("Nivel");
        if (!mute)
        {
            nivel.GetComponent<AudioSource>().enabled = false;
            SM.instance.volume = 0;
            mute = true;
        }
        else
        {
            nivel.GetComponent<AudioSource>().enabled = true;
            SM.instance.volume = 5;
            mute = false;
        }

    }

    //Devuelve a la pantalla de título o portada
    public void OnMenuClick()
    {
        TrackerEvent levelEnd = new EventLevelEnd(GM.instance.numeroNivel);
        TrackerInstance.TrackEvent(levelEnd);
        SceneManager.LoadScene(0);
    }

    private void empiezaJuego()
    {
        player.GetComponent<SpriteRenderer>().enabled = true;
        player.GetComponent<Rigidbody2D>().WakeUp();
        SceneManager.LoadScene(1); //Carga el selector de nivel (Biblioteca)
    }

}
