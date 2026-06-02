using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float velocidadeMovimento = 5f;
    [SerializeField] private float forcaPulo = 5f;

    [Header("Pulo")]
    [SerializeField] private float distanciaDeteccao = 0.1f;

    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D collider2D;
    private float inputMovimento;
    private bool estaNoChao;
    private Vector2 direcao = Vector2.right;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Entrada de movimento
        inputMovimento = Input.GetAxis("Horizontal");

        // Detectar se está no chão usando raycasts
        DetectarSolo();

        // Pulo - só permite se estiver tocando o chão
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

    private void DetectarSolo()
    {
        // Obter os limites do collider
        Bounds bounds = collider2D.bounds;

        // Posição de detecção na base do personagem
        Vector2 posicaoDeteccao = new Vector2(bounds.center.x, bounds.min.y - 0.05f);

        // Fazer raycast para baixo com distância maior
        RaycastHit2D raycast = Physics2D.Raycast(posicaoDeteccao, Vector2.down, distanciaDeteccao);

        // Debugar o raycast
        Debug.DrawRay(posicaoDeteccao, Vector2.down * distanciaDeteccao, raycast.collider != null ? Color.green : Color.red);

        // Verificar se acertou algo com a tag "chao"
        if (raycast.collider != null && raycast.collider.CompareTag("chao"))
        {
            estaNoChao = true;
        }
        else
        {
            estaNoChao = false;
        }
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

        // Jump = true enquanto não está tocando o chão (está no ar)
        animator.SetBool("jump", !estaNoChao);

        // Fall = true quando está caindo e não está no chão
        bool estaCaindo = rb.linearVelocity.y < -0.1f;
        animator.SetBool("fall", estaCaindo && !estaNoChao);
    }
}
