using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuantityButtons : MonoBehaviour
{
  [SerializeField] Text textoPrueba;
  [SerializeField] int contador = 0;
    // Start is called before the first frame update
    void Start()
    {
    textoPrueba.text = contador.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  public void incrementarUnidad() {
    contador++;
    textoPrueba.text = contador.ToString();
  }

  public void decrementarUnidad()
  {
    if (contador > 0)
    {
      contador--;
      textoPrueba.text = contador.ToString();
    }
    else { 
      Debug.LogError("No se puede reducir más la cantidad.");
    }
  }
}
