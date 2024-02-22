using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    private Color originColor;
    public Color onColor;

    private Image image;

    public bool IsOn { get; private set; }

    private void Awake()
    {
        image = GetComponent<Image>();
        originColor = image.color;
    }

    public void SetOn(bool value)
    {
        image.color = (IsOn = value) ? onColor : originColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Input.GetMouseButton(0))
        {
            return;
        }

        SetOn(!IsOn);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SetOn(!IsOn);
    }
}
