using UnityEngine;
using UnityEngine.SceneManagement;

public class NavegacionNiveles : MonoBehaviour
{
    
public void SiguienteNivel()
{
    int siguiente = SceneManager.GetActiveScene().buildIndex + 1;

    if (siguiente < SceneManager.sceneCountInBuildSettings)
    {
        SceneManager.LoadScene(siguiente);
    }
}


    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name
        );
    }
}


