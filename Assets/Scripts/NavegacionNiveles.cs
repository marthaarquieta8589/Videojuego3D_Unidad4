using UnityEngine;
using UnityEngine.SceneManagement;

public class NavegacionNiveles : MonoBehaviour
{
    public void SiguienteNivel()
    {
        SceneManager.LoadScene("Nivel2_Unidad4_5");
    }

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }
}


