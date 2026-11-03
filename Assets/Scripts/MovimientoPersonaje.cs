using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class MovimientoPersonaje : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 5f;

    private Rigidbody cuerpo;
    private float horizontal;
    private float vertical;
    private float tactilHorizontal;
    private float tactilVertical;
    private bool solicitarSalto;

    void Start()
    {
        cuerpo = GetComponent<Rigidbody>();
    }

    void Update()
    {
#if ENABLE_INPUT_SYSTEM
        Keyboard teclado = Keyboard.current;
        horizontal = 0f;
        vertical = 0f;

        if (teclado != null)
        {
            if (teclado.aKey.isPressed || teclado.leftArrowKey.isPressed)
                horizontal = -1f;
            if (teclado.dKey.isPressed || teclado.rightArrowKey.isPressed)
                horizontal = 1f;
            if (teclado.wKey.isPressed || teclado.upArrowKey.isPressed)
                vertical = 1f;
            if (teclado.sKey.isPressed || teclado.downArrowKey.isPressed)
                vertical = -1f;
            if (teclado.spaceKey.wasPressedThisFrame)
                solicitarSalto = true;
        }
#else
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
            solicitarSalto = true;
#endif
    }

    void FixedUpdate()
    {
        if (cuerpo == null)
            return;

        Vector3 direccion = new Vector3(
            horizontal + tactilHorizontal,
            0f,
            vertical + tactilVertical
        ).normalized;

        Vector3 movimiento = cuerpo.linearVelocity;
        movimiento.x = direccion.x * velocidad;
        movimiento.z = direccion.z * velocidad;
        cuerpo.linearVelocity = movimiento;

        bool enSuelo = Physics.Raycast(transform.position, Vector3.down, 1.15f);
        if (solicitarSalto && enSuelo)
        {
            cuerpo.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }
        solicitarSalto = false;
    }

    public void MoverHorizontal(float valor)
    {
        tactilHorizontal = valor;
    }

    public void MoverVertical(float valor)
    {
        tactilVertical = valor;
    }

    public void SaltarDesdeBoton()
    {
        solicitarSalto = true;
    }
}
