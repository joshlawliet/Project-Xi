using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public struct Items {
  public string nombre;
  public int cantidad;
  public string descrip;
  public int costo;
  public int pagoPorItem;
}

//SingleTon: Jugador: 
    //***UML de los atributos del jugador/singleton.***
//ToDo: Variable temporal que muestre, previo a la compra, cuanto quedará en el dinero del jugador.


public class TraderUIBehaviour : MonoBehaviour
{

  #region Variables para añadir botones automatico
  [SerializeField] string m_Path;
  [SerializeField] public GameObject elemento;
  [SerializeField] public GameObject contenido;
  [SerializeField] public GameObject[] traderItemsList;
  //[SerializeField] RectTransform elementoFiter;
  //[SerializeField] Quaternion elementoRot;
  #endregion


  public Items[] listaMercader = new Items[2];

  [SerializeField] Text[] txtObject;
  [SerializeField] Text[] txtItemCost;

  [SerializeField] int total;

  [SerializeField] int contador = 0;

  [SerializeField] InputField [] camposCantidad;
  [SerializeField] Input[] txtItemCuantity;

  int pagoTotal;

  //[SerializeField] Button[] buttons;

  //ToDo: Función para los botones más y menos.
  //Más: Sumar una unidad al texto cantidad del item correspondiente.
  //Sumar una unidad al texto cantidad del item correspondiente.
  private void Awake()
  {

    listaMercader[0].nombre = "Roca";
    listaMercader[1].nombre = "Manzana";
    listaMercader[0].costo = 2;
    listaMercader[1].costo = 5;

    //Muestra error: IndexOutOfRange
    //for (int i = 0; i < txtItNms.Length-1; i++) {
    //  txtObject[i] = txtItNms[i].GetComponent<Text>();
    //}

    //Agrega al array todo objeto de Clase 'Button' que haya en la jerarquía (Hierarchy) de la escena.

    //Agrega al array todo objeto de Calse'Text' que haya en la jerarquía (Hierarchy) de la escena.

    txtObject = Text.FindObjectsOfType<Text>();
    camposCantidad = InputField.FindObjectsOfType<InputField>();
    //buttons = Button.FindObjectsOfType<Button>();

    int contarNoms = 0;
    int contarCost = 0;
    for (int i = 0; i < txtObject.Length;i++) {
      if (txtObject[i].tag == "itemName") {
        contarNoms++;
        switch (contarNoms) {
          case 1:
            txtObject[i].text = listaMercader[0].nombre;
            break;
          case 2:
            txtObject[i].text = listaMercader[1].nombre;
            break;
        }
      }

      if (txtObject[i].tag == "itemCost") {
        //Debug.Log(i+" :" + txtObject[i].text + "-----"+ txtObject[i].tag);
        contarCost++;
        switch (contarCost)
        {
          case 1:
            txtObject[i].text = "$" + listaMercader[0].costo.ToString();
            break;
          case 2:
            txtObject[i].text = "$" + listaMercader[1].costo.ToString();
            break;
        }
      }
    }

  }

  // Start is called before the first frame update
  void Start()
  {
    //elemento = GameObject.Find("Elemento");
    //contenido = GameObject.Find("Content");
    //m_Path = Application.dataPath;
    //elementoFiter = elemento.GetComponent<RectTransform>();
    //elementoRot = elemento.GetComponent<Quaternion>();

    //FillListItems();
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void BuyButton() {
    Debug.Log("Caja de texto de Manzana: " + camposCantidad[0].text +
      "\n Caja de texto de Roca: "+ camposCantidad[1].text);

    listaMercader[0].pagoPorItem = listaMercader[0].costo * Convert.ToInt32(camposCantidad[1].text);
    listaMercader[1].pagoPorItem = listaMercader[1].costo * Convert.ToInt32(camposCantidad[0].text);

    Debug.Log("Pago por Manzana: " + listaMercader[1].pagoPorItem +
      "\n Pago por Roca: " + listaMercader[0].pagoPorItem);

    for (int i = 0; i < listaMercader.Length; i++) {

      total = total + listaMercader[i].pagoPorItem;
    }

    Debug.Log("El Total a Pagar: $" + total);

    txtObject[16].text = total.ToString();
    total = 0;

  }

  //ToDo: hacer dinamico el cambio de numeros en el pago por elemento
  //Todo hacer dinamica la visualización de pago total de items en texto "Total"
  public void dynamicSelectedItemPay() {
    int contartxts = 0;
    for (int i = 0; i <txtObject.Length; i++) {
      if (txtObject[i].tag=="itemPay") {
        contartxts++;
        switch (contartxts)
        {
          case 1:
            txtObject[i].text = (Convert.ToInt32(camposCantidad[1].text) * listaMercader[0].costo).ToString();
            break;
          case 2:
            txtObject[i].text = (Convert.ToInt32(camposCantidad[0].text) * listaMercader[1].costo).ToString();
            break;
        }
        Debug.Log("Encontrados: "+contartxts);
      }
    }
  }

  //ToDo: Modificar el espaciado entre cada elemento de la lista para que se acomoden bien.
  public void FillListItems()
  {
    /* for (int reng = 0; reng < 6; reng++)
    {
    //Crea Objetos con las mismas características que el original.
      //GameObject.Instantiate(elemento, elementoFiter.position , elementoRot, contenido.transform); ;

      //GameObject.Instantiate(elemento, elementoFiter.position
  //= new Vector2(elementoFiter.position.x, elementoFiter.position.y*-1), elementoRot, contenido.transform);
    
    }*/
  }
}
