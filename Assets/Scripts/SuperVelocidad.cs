using UnityEngine;
using System.Collections;

public class SuperVelocidad : MonoBehaviour
{
    public float velocidadExtra = 9f;

    private MovimientoPersonaje movimiento;
    private TrailRenderer estela;
    private float velocidadNormal;
    private bool activo = false;

    void Start()
    {
        movimiento = GetComponent<MovimientoPersonaje>();
        estela = GetComponentInChildren<TrailRenderer>();

        if (movimiento != null)
            velocidadNormal = movimiento.velocidad;

        if (estela != null)
            estela.emitting = false;
    }

    public void Activar()
    {
        if (!activo && movimiento != null)
            StartCoroutine(EfectoVelocidad());
    }

    IEnumerator EfectoVelocidad()
    {
        activo = true;

        // Aumentar la velocidad
        movimiento.velocidad = velocidadExtra;

        // Activar la estela
        if (estela != null)
        {
            estela.Clear();
            estela.emitting = true;
        }

        // Esperar 2.5 segundos
        yield return new WaitForSeconds(2.5f);

        // Recuperar la velocidad normal
        movimiento.velocidad = velocidadNormal;

        // Apagar la estela
        if (estela != null)
        {
            estela.emitting = false;
            estela.Clear();
        }

        activo = false;
    }
}

