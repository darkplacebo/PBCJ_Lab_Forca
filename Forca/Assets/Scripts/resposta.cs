using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class resposta : MonoBehaviour

    ///<summary>
    /// Esta classe define a palavra que será usada.
    ///</summary>

{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = PlayerPrefs.GetString("ultimaPalavraOculta");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
