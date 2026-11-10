
using UnityEngine;
using System.Collections.Generic;

public class GeneradorNiveles : MonoBehaviour
{
    public Transform personaje;
    public GameObject[] tiposDeModulo;

    public int modulosIniciales = 5;
    public float longitudModulo = 8f;
    public float alturaModulo = -0.25f;
    public float distanciaAdelante = 32f;

    private float siguienteZ = 8f;

    private Queue<GameObject> modulosActivos =
        new Queue<GameObject>();

    void Start()
    {
        if (personaje == null || tiposDeModulo.Length == 0)
        {
            Debug.LogWarning("Falta configurar el generador.");
            enabled = false;
            return;
        }

        for (int i = 0; i < modulosIniciales; i++)
        {
            CrearModulo();
        }
    }

    void Update()
    {
        // Generar plataformas delante del personaje
        while (personaje.position.z + distanciaAdelante >= siguienteZ)
        {
            CrearModulo();
        }

        // Eliminar plataformas que quedaron atrás
        while (modulosActivos.Count > modulosIniciales &&
               modulosActivos.Peek().transform.position.z <
               personaje.position.z - 16f)
        {
            Destroy(modulosActivos.Dequeue());
        }
    }

    void CrearModulo()
    {
        int numero = Random.Range(0, tiposDeModulo.Length);

        Vector3 posicion = new Vector3(
            0,
            alturaModulo,
            siguienteZ
        );

        GameObject nuevoModulo = Instantiate(
            tiposDeModulo[numero],
            posicion,
            Quaternion.identity
        );

        modulosActivos.Enqueue(nuevoModulo);

        siguienteZ += longitudModulo;
    }
}

