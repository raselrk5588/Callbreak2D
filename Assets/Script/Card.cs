using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class Card : MonoBehaviour, IPointerDownHandler
{
    public Image image;
    public int cardValue;
    public int clickTime;
    public Color color;
    public int cardindex;
    public CardName cardName;
    public GameManager gameManager;

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        gameManager.PlayingCard(this);
    }
   
}

public enum CardName
{
    Daimond,
    Cube,
    Seade,
    Heart
}
