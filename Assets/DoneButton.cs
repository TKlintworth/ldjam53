using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneButton : MonoBehaviour
{
    public OrderManager orderManager;
    public MenuPage menuPage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("DoneButton.OnMouseDown");
        //orderManager.AddIngredientToPizza(ingredientSprite);
        // Get the name of the gameObject but without the strijng "button"
        menuPage.DoneButtonPressed();
    }   
}
