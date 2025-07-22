using UnityEngine;
using Animals;

public class GameController : MonoBehaviour
{
    private Animal myDog;
    private Animal myCat;
    private void Awake()
    {
        Debug.Log("PRIMERO SE EJECUTA AWAKE");
    }
    

    // Start is called before the first frame update
    private void Start()
    {
        // Crear instancias de Dog y Cat
        myDog = new Dog();
        myDog.Name = "Rex";
        myDog.ServeFood();  // Llama al método de la clase Dog
        myDog.ShowFood();   // Muestra la comida

        myCat = new Cat();
        myCat.Name = "Miau";
        myCat.ServeFood();  // Llama al método de la clase Cat
        myCat.ShowFood();   // Muestra la comida


    }

    // Update is called once per frame
    private void Update()
    {
        // Hacer que los animales hablen
        myDog.Talk();
        myCat.Talk();
    }
}
