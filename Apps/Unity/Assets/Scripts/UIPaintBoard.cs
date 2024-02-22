using System.Collections.Generic;
using UnityEngine;

public class UIPaintBoard : MonoBehaviour
{
    public Texture2D targetTexture;
    public ToggleButton toggleButtonPrefab;
    private List<ToggleButton> toggleButtonList;

    private void Awake()
    {
        toggleButtonList = new List<ToggleButton>(targetTexture.width * targetTexture.height);

        for (int i = 0; i < targetTexture.width * targetTexture.height; i++)
        {
            toggleButtonList.Add(Instantiate(toggleButtonPrefab, transform));
        }
    }

    private void Update()
    {
        Color[] colors = new Color[toggleButtonList.Count];

        for (int h = 0; h < targetTexture.height; h++)
        {
            for (int w = 0; w < targetTexture.width; w++)
            {
                Color color = toggleButtonList[w + h * targetTexture.width].IsOn ? Color.white : Color.black;

                colors[w + h * targetTexture.width] = color;
            }
        }

        targetTexture.SetPixels(colors);
        targetTexture.Apply();
    }

    public void ClearBoard()
    {
        for (int i = 0; i < targetTexture.width * targetTexture.height; i++)
        {
            toggleButtonList[i].SetOn(false);
        }
    }
}
