using UnityEngine;

public class PotenciadorVelocidad : MonoBehaviour
{
    private void OnTriggerEnter(Collider otro)
    {
        SuperVelocidad superVelocidad =
            otro.GetComponentInParent<SuperVelocidad>();

        if (superVelocidad != null)
        {
            superVelocidad.Activar();

            Destroy(gameObject);
        }
    }
}

