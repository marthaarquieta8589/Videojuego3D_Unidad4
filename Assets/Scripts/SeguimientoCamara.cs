
using UnityEngine;

public class SeguimientoCamara : MonoBehaviour
{
    public Transform personaje;

    public float alturaCamara = 6f;
    public float distanciaZ = -9f;

    void LateUpdate()
    {
        if (personaje == null)
            return;

        transform.position = new Vector3(
            personaje.position.x,
            alturaCamara,
            personaje.position.z + distanciaZ
        );
    }
}
