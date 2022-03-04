using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class creditsButton : MonoBehaviour
    ///<summary>
    /// Esta classe controla o botão de créditos.
    ///</summary>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToCredits()
    {
        SceneManager.LoadScene("Forca_credits");
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Forca_start");
    }
}
