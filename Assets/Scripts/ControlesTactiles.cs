using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlesTactiles : MonoBehaviour
{
    private MovimientoPersonaje movimiento;
    private ControlMenu menu;
    private GameObject controles;

    void Start()
    {
        movimiento = FindFirstObjectByType<MovimientoPersonaje>();
        menu = GetComponent<ControlMenu>();

        if (movimiento == null || menu == null || menu.menuPrincipal == null)
        {
            Debug.LogError("ControlesTactiles: comprueba que Canvas tiene ControlMenu y que Personaje tiene MovimientoPersonaje.");
            enabled = false;
            return;
        }

        controles = new GameObject("ControlesMoviles", typeof(RectTransform));
        controles.transform.SetParent(transform, false);
        controles.transform.SetAsFirstSibling();

        RectTransform zona = controles.GetComponent<RectTransform>();
        zona.anchorMin = Vector2.zero;
        zona.anchorMax = Vector2.one;
        zona.offsetMin = Vector2.zero;
        zona.offsetMax = Vector2.zero;

        // Flechas en la esquina inferior izquierda, relativas al Canvas.
        CrearBoton("Izquierda", "IZQ", new Vector2(0, 0), new Vector2(115, 170),
            new Vector2(150, 105), () => movimiento.MoverHorizontal(-1), () => movimiento.MoverHorizontal(0));
        CrearBoton("Derecha", "DER", new Vector2(0, 0), new Vector2(345, 170),
            new Vector2(150, 105), () => movimiento.MoverHorizontal(1), () => movimiento.MoverHorizontal(0));
        CrearBoton("Adelante", "ARRIBA", new Vector2(0, 0), new Vector2(230, 290),
            new Vector2(170, 105), () => movimiento.MoverVertical(1), () => movimiento.MoverVertical(0));
        CrearBoton("Atras", "ABAJO", new Vector2(0, 0), new Vector2(230, 50),
            new Vector2(170, 95), () => movimiento.MoverVertical(-1), () => movimiento.MoverVertical(0));

        // Salto a la derecha y regreso al menu arriba a la derecha.
        CrearBoton("Saltar", "SALTAR", new Vector2(1, 0), new Vector2(-190, 150),
            new Vector2(230, 130), () => movimiento.SaltarDesdeBoton(), null);
        CrearBoton("RegresarMenu", "MENU", new Vector2(1, 1), new Vector2(-150, -80),
            new Vector2(210, 90), RegresarAlMenu, null);

        controles.SetActive(false);
    }

    void Update()
    {
        if (controles == null || menu == null || menu.menuPrincipal == null)
            return;

        bool jugando = !menu.menuPrincipal.activeSelf;
        if (controles.activeSelf != jugando)
        {
            controles.SetActive(jugando);
            if (!jugando)
            {
                movimiento.MoverHorizontal(0);
                movimiento.MoverVertical(0);
            }
        }
    }

    void RegresarAlMenu()
    {
        movimiento.MoverHorizontal(0);
        movimiento.MoverVertical(0);
        menu.VolverAlMenu();
    }

    void CrearBoton(string nombre, string etiqueta, Vector2 ancla, Vector2 posicion,
        Vector2 tamano, Action alPresionar, Action alSoltar)
    {
        GameObject objeto = new GameObject(nombre,
            typeof(RectTransform), typeof(Image), typeof(Button), typeof(EventTrigger));
        objeto.transform.SetParent(controles.transform, false);

        RectTransform rect = objeto.GetComponent<RectTransform>();
        rect.anchorMin = ancla;
        rect.anchorMax = ancla;
        rect.pivot = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = posicion;
        rect.sizeDelta = tamano;

        Image imagen = objeto.GetComponent<Image>();
        imagen.color = new Color(0.08f, 0.15f, 0.25f, 0.84f);
        objeto.GetComponent<Button>().targetGraphic = imagen;

        GameObject texto = new GameObject("Texto", typeof(RectTransform), typeof(TextMeshProUGUI));
        texto.transform.SetParent(objeto.transform, false);
        RectTransform textoRect = texto.GetComponent<RectTransform>();
        textoRect.anchorMin = Vector2.zero;
        textoRect.anchorMax = Vector2.one;
        textoRect.offsetMin = Vector2.zero;
        textoRect.offsetMax = Vector2.zero;

        TextMeshProUGUI tmp = texto.GetComponent<TextMeshProUGUI>();
        tmp.text = etiqueta;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.fontSize = 30f;
        tmp.color = Color.white;
        tmp.raycastTarget = false;

        EventTrigger evento = objeto.GetComponent<EventTrigger>();
        AgregarEvento(evento, EventTriggerType.PointerDown, alPresionar);
        AgregarEvento(evento, EventTriggerType.PointerUp, alSoltar);
        AgregarEvento(evento, EventTriggerType.PointerExit, alSoltar);
    }

    void AgregarEvento(EventTrigger evento, EventTriggerType tipo, Action accion)
    {
        if (accion == null) return;
        EventTrigger.Entry entrada = new EventTrigger.Entry();
        entrada.eventID = tipo;
        entrada.callback.AddListener((datos) => accion());
        evento.triggers.Add(entrada);
    }
}
