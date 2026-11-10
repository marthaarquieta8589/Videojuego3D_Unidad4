
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReinicioPartida : MonoBehaviour
{
    public float alturaLimite = -4f;

    private bool perdio = false;

    void Update()
    {
        // Detectar si el personaje cayó
        if (transform.position.y < alturaLimite && !perdio)
        {
            perdio = true;

            Debug.Log("El jugador perdió. Reiniciando...");

            Invoke(nameof(ReiniciarNivel), 1f);
        }
    }

    void ReiniciarNivel()
    {
        // Volver a cargar la escena actual
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }
}

