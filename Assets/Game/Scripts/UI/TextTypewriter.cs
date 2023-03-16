using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Febucci.UI;

public class TextTypewriter : MonoBehaviour
{
    TextAnimatorPlayer textAnimatorPlayer;

    private void Awake()
    {
        textAnimatorPlayer = GetComponent<TextAnimatorPlayer>();
    }

    private void OnEnable()
    {
        ShowTypewriter();
    }

    public void ShowTypewriter()
    {
        textAnimatorPlayer.ShowText("...");
    }
}
