using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manageButton : MonoBehaviour

    ///<summary>
    /// Esta classe gerencia o score e carrega a cena inicial.
    ///</summary>

{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("SCORE", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMundoGame()
        /*Carrega a cena do jogo*/
    {
        SceneManager.LoadScene("Forca");
    }
}
