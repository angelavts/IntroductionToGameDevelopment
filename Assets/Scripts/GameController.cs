using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        // Crear instancias de Dog y Cat
        Animal myDog = new Dog();
        myDog.Name = "Rex";
        myDog.ServeFood();  // Llama al método de la clase Dog
        myDog.ShowFood();   // Muestra la comida

        Animal myCat = new Cat();
        myCat.Name = "Miau";
        myCat.ServeFood();  // Llama al método de la clase Cat
        myCat.ShowFood();   // Muestra la comida

        // Hacer que los animales hablen
        myDog.Talk();
        myCat.Talk();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
