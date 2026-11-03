
using UnityEngine;

public class ControlMenu : MonoBehaviour
{
    public GameObject menuPrincipal;

    public void Jugar()
    {
        menuPrincipal.SetActive(false);
    }

    public void VolverAlMenu()
    {
        menuPrincipal.SetActive(true);
    }
}


