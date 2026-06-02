using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float velocidadeMovimento = 5f;
    [SerializeField] private float forcaPulo = 5f;

    [Header("Pulo")]
    [SerializeField] private float raioDeteccao = 0.2f;
    [SerializeField] private LayerMask camadaChao;

    private Rigidbody2D rb;
    private Animator animator;
    private float inputMovimento;
    private bool estaNoChao;
    private Vector2 direcao = Vector2.right;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Entrada de movimento
        inputMovimento = Input.GetAxis("Horizontal");

        // Detectar se está no chão
        estaNoChao = Physics2D.OverlapCircle(transform.position, raioDeteccao, camadaChao);

        // Pulo
        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao)
        {
            Pular();
        }

        // Atualizar animações
        AtualizarAnimador();
    }

    void FixedUpdate()
    {
        // Aplicar movimento
        Mover();
    }

    private void Mover()
    {
        // Preservar velocidade vertical
        float velocidadeY = rb.linearVelocity.y;

        // Aplicar movimento horizontal
        float novaVelocidadeX = inputMovimento * velocidadeMovimento;
        rb.linearVelocity = new Vector2(novaVelocidadeX, velocidadeY);

        // Virar o personagem para a direção do movimento
        if (inputMovimento > 0.1f)
        {
            direcao = Vector2.right;
            transform.localScale = new Vector3(5, 5, 5);
        }
        else if (inputMovimento < -0.1f)
        {
            direcao = Vector2.left;
            transform.localScale = new Vector3(-5, 5, 5);
        }
    }

    private void Pular()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * forcaPulo, ForceMode2D.Impulse);
    }

    private void AtualizarAnimador()
    {
        // Calcular velocidade de movimento (valor absoluto para animação)
        float velocidadeAbsoluta = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("velocity", velocidadeAbsoluta);

        // Verificar se está pulando (velocidade Y positiva)
        bool estaPulando = rb.linearVelocity.y > 0.1f;
        animator.SetBool("jump", estaPulando && !estaNoChao);

        // Verificar se está caindo (velocidade Y negativa)
        bool estaCaindo = rb.linearVelocity.y < -0.1f;
        animator.SetBool("fall", estaCaindo && !estaNoChao);
    }
}
