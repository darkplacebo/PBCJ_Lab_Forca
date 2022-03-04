using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour

    ///<summary>
    /// Esta classe é o principal controlador do jogo, responsável pelos parâmetros.
    ///</summary>

{
    private int numTentativas;                      // Armazenas quantas tentaivas foram usadas
    private int maxNumTentativas;                   // Limite de tentativas ou "Vidas"
    int score = 0;

    public GameObject letra;                        // Prefab da letra no Game
    public GameObject centro;                       // objeto de texto que indica o centro da tela

    private string palavraOculta = "";              // palavra a ser descoberta
    private int tamanhoPalavraOculta;               // tamanho da palavra
    char[] letrasOcultas;                           // letras da palavra
    bool[] letrasDescobertas;                       // indicador das letras que foram descobertas

    // Start is called before the first frame update
    void Start()
    {
        centro = GameObject.Find("centroTela");
        IniGame();
        IniLetra();
        numTentativas = 0;
        maxNumTentativas = 10;
        PlayerPrefs.SetInt("score", 0);
        UpdateNumTentativas();
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        checkTeclado();
        CheckPalavraDescoberta();
    }

    void IniLetra()

        /*Responsável pela inicialização das letras que irão aparecer no canvas, de forma que a palavra que é mostrada seja condizente com a palavra escolhida.*/

    {
        int num_Letras = tamanhoPalavraOculta;

        for (int i = 0; i < num_Letras; i++)
        {
            Vector3 novaPosicao;
            novaPosicao = new Vector3(centro.transform.position.x + ((i-num_Letras/2.0f)*80), centro.transform.position.y, centro.transform.position.z);
            GameObject l = (GameObject)Instantiate(letra, novaPosicao, Quaternion.identity);
            l.name = "letra" + (i + 1);
            l.transform.SetParent(GameObject.Find("Canvas").transform);
        }
    }

    void IniGame()
    
        /*Responsável pela inicialização do jogo em si, escolha da palavra oculta, inicialização do array que será utilizado para armazenar a palavra oculta e controla o array
         responsável pelo indicador de acertos.*/
    
    {
        palavraOculta = EscolhePalavra();                       // define a palavra a ser descoberta
        tamanhoPalavraOculta = palavraOculta.Length;            // contabiliza o num de letras da palavra
        palavraOculta = palavraOculta.ToUpper();                // escreve a palavra em caixa alta
        letrasOcultas = new char[tamanhoPalavraOculta];         // instancia o array char da palavra
        letrasDescobertas = new bool[tamanhoPalavraOculta];     // instancia o array bool do indicador de acerto da palavra
        letrasOcultas = palavraOculta.ToCharArray();            // copia a palavra para o array de letras
    }

    void checkTeclado()

        /*Responsável por checar as teclas pressionadas durante o jogo, assim como carregar a cena de game over quando necessário, e responsável pelo contador de pontos.*/

    {
        if(Input.anyKeyDown)
        {
            char letraTeclada = Input.inputString.ToCharArray()[0];
            int letraTecladaComoInt = System.Convert.ToInt32(letraTeclada);

            if(letraTecladaComoInt >= 97 && letraTecladaComoInt <= 122)
            {
                // CheckPalavraDescoberta();
                numTentativas++;
                UpdateNumTentativas();
                if(numTentativas == maxNumTentativas)
                {
                    SceneManager.LoadScene("Forca_GameOver");
                }
                for(int i = 0; i < tamanhoPalavraOculta; i++)
                {
                    if (!letrasDescobertas[i])
                    {
                        letraTeclada = System.Char.ToUpper(letraTeclada);
                        if(letrasOcultas[i] == letraTeclada)
                        {
                            letrasDescobertas[i] = true;
                            GameObject.Find("letra" + (i + 1)).GetComponent<Text>().text = letraTeclada.ToString();
                            
                            score = PlayerPrefs.GetInt("score");
                            score++;
                            PlayerPrefs.SetInt("score", score);
                            UpdateScore();
                        }
                    }
                }
            }
        }
    }

    void UpdateNumTentativas()

        /*Responsável por controlar o número de tentativas, atualizando conforme necessário.*/

    {
        GameObject.Find("numTentativas").GetComponent<Text>().text = "Tries: " + numTentativas + " | " + maxNumTentativas;
    }

    void UpdateScore()

        /*Responsável por controlar o score, atualizando conforme necessário.*/

    {
        GameObject.Find("scoreUI").GetComponent<Text>().text = "SCORE\n" + score;
    }

    void CheckPalavraDescoberta()

        /*Responsável por checar se a palavra oculta e a palavra descoberta são iguais e então carregar a cena de vitória.*/

    {
        bool check = true;
        for(int i = 0; i < tamanhoPalavraOculta; i++)
        {
            check = check && letrasDescobertas[i];
        }
        if (check)
        {
            PlayerPrefs.SetString("ultimaPalavraOculta", palavraOculta);
            SceneManager.LoadScene("Forca_winner");
        }
    }

    string EscolhePalavra()

        /*Responsável pela escolha da palavra oculta que será utilizada no jogo*/

    {
        TextAsset t1 = (TextAsset)Resources.Load("palavras", typeof(TextAsset));
        string s = t1.text;
        string[] palavras = s.Split(' ');
        int palavraAleatoria = Random.Range(0, palavras.Length);
        return (palavras[palavraAleatoria]);
    }
}