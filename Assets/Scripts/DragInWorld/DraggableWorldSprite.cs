using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class DraggableWorldSprite : MonoBehaviour, 
    IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Camera cam;                    // Asigna la cámara, o se busca en Start
    private Vector3 offset;
    private bool dragging;

    void Start()
    {
        if (!cam) cam = Camera.main;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 mouseWorld = cam.ScreenToWorldPoint(eventData.position);
        mouseWorld.z = transform.position.z;

        offset = transform.position - mouseWorld;
        Debug.Log("OnPointerDown at " + mouseWorld + " with offset " + offset);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragging = true;
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!dragging) return;

        // Para camara ORTOGRAFICA
        // Convertimos la posición del puntero (eventData.position está en coordenadas de pantalla, en píxeles)
        // a coordenadas del mundo usando la cámara. Esto nos da la posición X e Y correctas en el mundo.
        // Sin embargo, como el puntero en pantalla no tiene "profundidad" (Z),
        // forzamos el valor de Z a ser igual al del objeto, para que permanezca en su mismo plano.
        //Vector3 mouseWorld = cam.ScreenToWorldPoint(eventData.position);
        //mouseWorld.z = transform.position.z;
        
        // Para camara PERSPECTIVA
        // Calcula la profundidad (distancia del objeto a la cámara en pantalla)
        float depth = cam.WorldToScreenPoint(transform.position).z;

        // Usa esa profundidad al convertir pantalla → mundo
        Vector3 sp = new Vector3(eventData.position.x, eventData.position.y, depth);
        Vector3 mouseWorld = cam.ScreenToWorldPoint(sp);

        transform.position = mouseWorld;
        Debug.Log("OnDrag to " + transform.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
    }
}