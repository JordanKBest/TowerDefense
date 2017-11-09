using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    public bool dragOnSurfaces = true;
    private GameObject _Dragging_Icon;
    private GameObject _Object_Radius;
    private RectTransform _DraggingPlane;
    public GameObject newObject;
    private float radius;
    public Sprite radiusImage;

    public void OnBeginDrag(PointerEventData eventData)
    {
        var canvas = FindInParents<Canvas>(gameObject);
        if (canvas == null)
            return;
        _Dragging_Icon = new GameObject(newObject.name);
        _Dragging_Icon.transform.SetParent(canvas.transform, false);
        _Dragging_Icon.transform.SetAsLastSibling();
        Image image = _Dragging_Icon.AddComponent<Image>();
        image.sprite = newObject.GetComponent<SpriteRenderer>().sprite;
        image.SetNativeSize();
        //radius = newObject.GetComponent<CircleCollider2D>().radius;
        //_Object_Radius = new GameObject("Radius");
        //_Object_Radius.transform.SetParent(_Dragging_Icon.transform, false);
        //SpriteRenderer radiusSprite = _Object_Radius.AddComponent<SpriteRenderer>();
        //radiusSprite.sprite = radiusImage;
        //radiusSprite.transform.localScale = new Vector3(radius * 2, radius * 2);
        if (dragOnSurfaces)
        {
            _DraggingPlane = transform as RectTransform;
        }
        else
        {
            _DraggingPlane = canvas.transform as RectTransform;
        }

        SetDraggedPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(_Dragging_Icon != null)
        {
            SetDraggedPosition(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(_Dragging_Icon != null)
        {
            Destroy(_Dragging_Icon);
        }
        Vector3 dropPos = Camera.main.ScreenToWorldPoint(eventData.position);
        Instantiate(newObject, new Vector3(dropPos.x, dropPos.y, 0), _DraggingPlane.rotation);
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        if (dragOnSurfaces && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
        {
            _DraggingPlane = data.pointerEnter.transform as RectTransform;
        }
        var rt = _Dragging_Icon.GetComponent<RectTransform>();
        Vector3 globalMousePos;
        if(RectTransformUtility.ScreenPointToWorldPointInRectangle(_DraggingPlane, data.position, data.pressEventCamera, out globalMousePos))
        {
            rt.position = globalMousePos;
            rt.rotation = _DraggingPlane.rotation;
        }
    }

    static public T FindInParents<T>(GameObject go) where T:Component
    {
        if (go == null) return null;
        var comp = go.GetComponent<T>();
        if(comp != null)
        {
            return comp;
        }

        Transform t = go.transform.parent;
        while(t != null && comp == null)
        {
            comp = t.gameObject.GetComponent<T>();
            t = t.parent;
        }
        return comp;
    }
}
