using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField] private Sprite XSprite;
    [SerializeField] private Sprite OSprite;
    [SerializeField] private Sprite DefaultSprite;
    [SerializeField] private AudioClip NyaX;
    [SerializeField] private AudioClip NyaY;

    private char ButtonContent='e';
    private AudioSource buttonClickAudio;
    void Start()
    {
        buttonClickAudio = this.GetComponent<AudioSource>();
    }
    public void OnButtonClick()
    {
        if (BoardGame.instance.getIsXTurn())
        {
            this.GetComponent<Image>().sprite = XSprite;
            buttonClickAudio.clip = NyaX;
            
        }
        else
        {
            this.GetComponent<Image>().sprite = OSprite;
            buttonClickAudio.clip = NyaY;
        }

        SetCharacter();
        buttonClickAudio.Play();
        this.GetComponent<Button>().interactable = false;

        if (BoardGame.instance.isThereAWin())
        {
            BoardGame.instance.TriggerWinning();
        }
        else
        {
            BoardGame.instance.changeTurn();
        }
        
    }

    private void SetCharacter()
    {
        if (BoardGame.instance.getIsXTurn())
        {
            ButtonContent = 'x';
        }
        else
        {
            ButtonContent = 'o';
        }
    }

    public char GetCharacter()
    {
        return ButtonContent;
    }
}
