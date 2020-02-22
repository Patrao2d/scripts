using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    // Sprites
    public Sprite cardBack;
    public Sprite cardFront;
    public Sprite cardLocked;

    // Button
    private Button _button;

    // Bools
    private bool _locked = false;
    private bool _flipped;

    // Ints
    public int cardID;

    private static Card _instance;

    public static Card instance
    {
        get { return _instance; }
    }

    private void Start()
    {
        _instance = this;
        _button = GetComponent<Button>();
        _button.image.overrideSprite = cardBack;
        _flipped = false;
    }


    public void Click()
    {
        if (_flipped || _locked) return;
        _button.image.overrideSprite = cardFront;
        _button.transform.rotation = Quaternion.Euler(0, 0, 0);
        _flipped = true;
        GameManager.instance.nOfClicks++;
        GameManager.instance.flippedCardsList.Add(this.gameObject);
        GameManager.instance.CompareCards();
    }

    public void RotateCard()
    {
        if (_flipped || _locked) return;
        _button.transform.rotation = Quaternion.Euler(0, 30, 0);
    }

    public void UndoRotate()
    {
        if (_flipped || _locked) return;
        _button.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void FailComparation()
    {
        _button.image.overrideSprite = cardBack;
        _flipped = false;
    }

    public void YayComparation()
    {
        _button.image.overrideSprite = cardLocked;
        _flipped = true;
        _locked = true;
    }
    
   /* public void ResetCard()
    {
        _button.image.overrideSprite = cardBack;
        _flipped = false;
        _locked = false;
    }*/
}
