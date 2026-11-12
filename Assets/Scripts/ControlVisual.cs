using UnityEngine;
using TMPro;

public class ControlVisual : MonoBehaviour
{
    public TMP_Text textoPuntuacion;
    public int puntosPorCambio = 20;
    public float suavidad = 2f;

    public Color[] coloresFondo =
    {
        new Color32(16, 18, 30, 255),
        new Color32(43, 18, 41, 255),
        new Color32(50, 37, 19, 255)
    };

    private Camera camara;

    void Start()
    {
        camara = GetComponent<Camera>();
        camara.backgroundColor = coloresFondo[0];
    }

    void Update()
    {
        if (textoPuntuacion == null ||
            coloresFondo.Length == 0)
            return;

        string[] partes = textoPuntuacion.text.Split(':');

        if (partes.Length < 2)
            return;

        if (int.TryParse(partes[1].Trim(), out int puntos))
        {
            int indice = (puntos / Mathf.Max(1, puntosPorCambio))
                         % coloresFondo.Length;

            camara.backgroundColor = Color.Lerp(
                camara.backgroundColor,
                coloresFondo[indice],
                Time.deltaTime * suavidad
            );
        }
    }
}

