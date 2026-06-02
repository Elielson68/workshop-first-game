using UnityEngine;

public class PigEnemie : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float velocidade = 2f;
    [SerializeField] private float distanciaPatrulha = 5f;

    private Rigidbody2D rb;
    private float posicaoEsquerda;
    private float posicaoDireita;
    private float direcao = 1f; // 1 = direita, -1 = esquerda

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        posicaoDireita = transform.position.x + distanciaPatrulha;
        posicaoEsquerda = transform.position.x - distanciaPatrulha;
    }

    void FixedUpdate()
    {
        // Aplicar movimento
        rb.linearVelocity = new Vector2(direcao * velocidade, rb.linearVelocity.y);

        // Verificar limites e inverter direção
        if (direcao > 0 && transform.position.x >= posicaoDireita)
        {
            direcao = -1;
            VirarPersonagem();
        }
        else if (direcao < 0 && transform.position.x <= posicaoEsquerda)
        {
            direcao = 1;
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
        // Debug para verificar o que colidiu
        Debug.Log("Colidiu com: " + collision.gameObject.name + " | Tag: " + collision.tag);

        // Se colidir com o Player, destruir ele
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player foi destruído pelo inimigo!");
            Destroy(collision.gameObject);
        }
    }
}

