
using UnityEngine;
using TMPro;

public class SistemaPuntuacion : MonoBehaviour
{
    public Transform personaje;
    public TextMeshProUGUI textoPuntuacion;
    public TextMeshProUGUI textoRecord;

    // Referencia al movimiento del personaje
    public MovimientoPersonaje movimientoPersonaje;

    public float distanciaPorPunto = 1f;

    private Vector3 posicionAnterior;
    private float distanciaRecorrida = 0f;
    private int puntos = 0;
    private int record = 0;

    void Start()
    {
        posicionAnterior = personaje.position;

        // Recuperar el record guardado
        record = PlayerPrefs.GetInt("RecordMaximo", 0);

        textoPuntuacion.text = "PUNTOS: 0";
        textoRecord.text = "RÉCORD: " + record;

        // Velocidad inicial
        movimientoPersonaje.velocidad = 5f;
    }

    void Update()
    {
        Vector3 actual = personaje.position;

        Vector2 anteriorXZ = new Vector2(
            posicionAnterior.x,
            posicionAnterior.z
        );

        Vector2 actualXZ = new Vector2(
            actual.x,
            actual.z
        );

        distanciaRecorrida += Vector2.Distance(
            anteriorXZ,
            actualXZ
        );

        posicionAnterior = actual;

        int nuevosPuntos = Mathf.FloorToInt(
            distanciaRecorrida / distanciaPorPunto
        );

        if (nuevosPuntos > puntos)
        {
            puntos = nuevosPuntos;

            textoPuntuacion.text = "PUNTOS: " + puntos;

            // Aumentar dificultad cada 10 puntos
            int nivel = puntos / 10;

            movimientoPersonaje.velocidad = Mathf.Min(
                5f + nivel,
                10f
            );

            // Actualizar y guardar el record
            if (puntos > record)
            {
                record = puntos;

                PlayerPrefs.SetInt("RecordMaximo", record);
                PlayerPrefs.Save();

                textoRecord.text = "RÉCORD: " + record;
            }
        }
    }
}


