using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour, IPointerClickHandler
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

    public void OnPointerClick(PointerEventData eventData)
    {
        IsOn = !IsOn;
        image.color = IsOn ? onColor : originColor;
    }
}
