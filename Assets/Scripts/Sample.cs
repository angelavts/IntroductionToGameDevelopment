using System.Collections.Generic;
using UnityEngine;

namespace Animals
{
    // Clase base Animal
    public class Animal
    {
        public string Name;                // Propiedad pública para almacenar el nombre del animal
        private int _age;                   // Campo privado para almacenar la edad del animal
        protected List<string> _food;      // Lista protegida que almacena la comida que puede consumir el animal

        // Constructor de la clase Animal, inicializa la lista de comida
        public Animal()
        {
            _food = new List<string>();
        }

        // Método virtual Talk, que puede ser sobrescrito por clases derivadas
        public virtual void Talk()
        {
            Debug.Log("El animal hace un sonido");
        }

        // Método virtual ServeFood, que agrega comida a la lista
        public virtual void ServeFood()
        {
            _food.Add("Carne");
            _food.Add("Pollo");
        }

        // Método para mostrar la comida disponible en la lista
        public void ShowFood()
        {
            string foodList = "Comida disponible: ";
            foreach (var food in _food)
            {
                foodList += food + ", ";
            }
            Debug.Log(foodList);
        }
    }

    // Clase derivada Dog, hereda de Animal
    public class Dog : Animal
    {
        // Sobrescribe el método ServeFood para agregar comida adicional para perros
        public override void ServeFood()
        {
            base.ServeFood();  // Llama al método de la clase base (Animal)
            _food.Add("Perrarina");  // Agrega un tipo de comida exclusivo para perros
        }

        // Sobrescribe el método Talk para que el perro haga su sonido característico
        public override void Talk()
        {
            Debug.Log("Wof wof");
        }
    }

    // Clase derivada Cat, hereda de Animal
    public class Cat : Animal
    {
        // Sobrescribe el método ServeFood para agregar comida adicional para gatos
        public override void ServeFood()
        {
            base.ServeFood();  // Llama al método de la clase base (Animal)
            _food.Add("Gatarina");  // Agrega un tipo de comida exclusivo para gatos
        }

        // Sobrescribe el método Talk para que el gato haga su sonido característico
        public override void Talk()
        {
            Debug.Log("Miau");
        }
    }
}





