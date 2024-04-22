using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditMenu : MonoBehaviour
{
    public Canvas CreditCanvas;

    public void ShowCredit()
    {
        CreditCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void HideCredit()
    {
        CreditCanvas.GetComponent<Canvas>().enabled = false;
    }
}
