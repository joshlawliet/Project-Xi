using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CuantityButtons : MonoBehaviour
{
  [SerializeField] InputField cajaCantidad;
  [SerializeField] Text cajaPago;
  [SerializeField] int contador = 0;

  TraderUIBehaviour traderUIBehaviour;
    // Start is called before the first frame update
    void Start()
    {
    cajaCantidad.text = contador.ToString();
  }

    // Update is called once per frame
    void Update()
    {
        
    }

  public void incrementarUnidad() {
    contador++;
    cajaCantidad.text = contador.ToString();

  }

  public void decrementarUnidad()
  {
    if (contador > 0)
    {
      contador--;
      cajaCantidad.text = contador.ToString();
    }
    else { 
      Debug.LogError("No se puede reducir más la cantidad.");
    }
  }

  public void dynamicSelectedItemPay() {
    cajaPago.text = (Convert.ToInt32(cajaCantidad.text) * 2).ToString();
  }
}
