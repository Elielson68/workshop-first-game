using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public GameObject TextoPressF;

    private bool playerProximo = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verificar se o que entrou tem a tag "Player"
        if (collision.CompareTag("Player"))
        {
            playerProximo = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Verificar se o que saiu tem a tag "Player"
        if (collision.CompareTag("Player"))
        {
            playerProximo = false;
        }
    }

    void Update()
    {
        if (playerProximo)
        {
            TextoPressF.SetActive(true);
        }
        else
        {
            TextoPressF.SetActive(false);
        }

        // Se o player está próximo e apertar F, muda de cena
        if (playerProximo && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("Finish");
        }
    }
}

