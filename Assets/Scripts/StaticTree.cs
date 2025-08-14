using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class StaticTree
{
    public static Action OnTreeInitialized;


    public static void Example()
    {
        OnTreeInitialized += InitializeTree;
        OnTreeInitialized += InitializeTree;
        OnTreeInitialized += InitializeTree2;
        
        OnTreeInitialized -= InitializeTree;
        
        OnTreeInitialized = InitializeTree;
        OnTreeInitialized?.Invoke();
    }
    
    public static void InitializeTree()
    {
        // Initialization logic for the tree
        Debug.Log("Tree has been initialized.");
        
        // Notify subscribers that the tree has been initialized
        OnTreeInitialized?.Invoke();
    }
    
    public static void InitializeTree2()
    {

    }
}
