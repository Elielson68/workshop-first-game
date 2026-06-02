using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    private GameObject playerAtual;

    void Start()
    {
        // Encontrar o player na cena ao iniciar
        playerAtual = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        // Verificar se o player foi destruído
        if (playerAtual == null)
        {
            // Criar um novo player na posição deste checkpoint
            CriarNovoPlayer();
        }
    }

    private void CriarNovoPlayer()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("PlayerPrefab não foi atribuído no Inspector!");
            return;
        }

        // Instanciar o novo player na posição do checkpoint
        playerAtual = Instantiate(playerPrefab, transform.position, Quaternion.identity);
        Debug.Log("Novo Player criado em: " + transform.position);
    }
}
