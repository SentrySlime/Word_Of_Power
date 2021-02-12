using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopUpTrait : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject traitCard;
    public Image traitCardImage;
    UI_Controller uiController;

    GameObject tempCard;

    void Start()
    {
        uiController = GameObject.Find("Canvas").GetComponent<UI_Controller>();
    }

    
    void Update()
    {
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        print("Over");
        tempCard = Instantiate(traitCard, gameObject.transform.position /*+ (gameObject.transform.up * 2)*/, Quaternion.identity);
        tempCard.transform.SetParent(uiController.characterMenu.transform);
        tempCard.transform.localScale = new Vector3(1, 1, 1);
        tempCard.transform.localPosition += (tempCard.transform.up * 80) + (tempCard.transform.right * 50);
        tempCard.SetActive(true);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Destroy(tempCard);
    }
}
