using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineExample : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private List<Transform> points;
    [SerializeField] private bool useCoroutine = true;
    // Start is called before the first frame update
    private Coroutine _movementCoroutine;
    void Start()
    {
        MethodOne();
        _movementCoroutine = StartCoroutine(MoveTarget());
        MethodTwo();
    }

    private void Update()
    {
        if (!useCoroutine)
        {
            useCoroutine = true;
            if (_movementCoroutine != null)
                StopCoroutine(_movementCoroutine);
        }
    }

    private void MethodOne()
    {
        Debug.Log("Method One executed");
    }
    
    private void MethodTwo()
    {
        Debug.Log("Method Two executed");
    }
    
    private IEnumerator MoveTarget()
    {
        while (true)
        {
            foreach (Transform point in points)
            {
                while (Vector3.Distance(target.position, point.position) > 0.1f)
                {
                    target.position = Vector3.MoveTowards(target.position, point.position, 2f * Time.deltaTime);
                    yield return null; // Espera al siguiente frame
                }
                yield return new WaitForSeconds(1f); // Espera 1 segundo en el punto
            }
        }
    }


}
