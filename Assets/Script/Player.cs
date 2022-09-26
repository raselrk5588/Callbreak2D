using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public List<Card> playerCard = new List<Card>();
    public int playerIndex;

    public void setCard(Card _card)
    {
        playerCard.Add(_card);
        _card.cardindex = playerIndex;
        _card.transform.parent = transform;
    }

}
