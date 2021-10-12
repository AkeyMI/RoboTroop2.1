using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MinionUiBox : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] Image image = default;

    public MinionContainer MContainer => mContainer;

    private MinionContainer mContainer;

    private MinionData minionData;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = FindObjectOfType<BoxMInion>().GetCanvas;
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void SetMinionData(MinionData data)
    {
        //Modificar UI con la data sprite
        minionData = data;
        image.sprite = data.sprite;
    }

    public void SetMinionContainer(MinionContainer minionContainer)
    {
        mContainer = minionContainer;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = .6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(transform.parent.GetComponent<MinionContainer>() == null)
        {
            transform.SetParent(mContainer.transform, false);
            rectTransform.anchoredPosition = Vector3.zero;
        }
    }
}
