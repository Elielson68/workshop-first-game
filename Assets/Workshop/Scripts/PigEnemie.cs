using UnityEngine;

public class PigEnemie : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float velocidade = 2f;
    [SerializeField] private float distanciaPatrulha = 5f;

    private Rigidbody2D rb;
    private Vector3 posicaoInicial;
    private float direcao = 1f; // 1 = direita, -1 = esquerda

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        posicaoInicial = transform.position;
    }

    void FixedUpdate()
    {
        // Aplicar movimento
        rb.linearVelocity = new Vector2(direcao * velocidade, rb.linearVelocity.y);

        // Verificar se chegou ao limite de patrulha
        float distanciaPercorrida = Mathf.Abs(transform.position.x - posicaoInicial.x);

        if (distanciaPercorrida >= distanciaPatrulha)
        {
            // Inverter direção
            direcao *= -1;
            VirarPersonagem();
        }
    }

    private void VirarPersonagem()
    {
        // Virar o sprite
        Vector3 novaEscala = transform.localScale;
        novaEscala.x *= -1;
        transform.localScale = novaEscala;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se colidir com o Player, destruir ele
        if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Player foi destruído pelo inimigo!");
        }
    }
}

