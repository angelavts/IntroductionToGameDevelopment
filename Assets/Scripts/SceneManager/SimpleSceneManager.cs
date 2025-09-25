using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneManager : MonoBehaviour
{
    // Instancia estática accesible desde cualquier script
    public static SimpleSceneManager Instance { get; private set; }
    
    private void Awake()
    {
        // Si ya existe una instancia, destruye la nueva para que sea único
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Se mantiene al cambiar de escena
    }
    
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void ReloadCurrentScene()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }

}
