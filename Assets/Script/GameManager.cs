using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    public List<Sprite> daimond_sprite = new List<Sprite>();
    public List<Sprite> heart_sprite = new List<Sprite>();
    public List<Sprite> cube_sprite = new List<Sprite>();
    public List<Sprite> spade_sprite = new List<Sprite>();

    public List<Player> players = new List<Player>();

    public List<Transform> boardCards = new List<Transform>();


    public List<Card> cards = new List<Card>();
    public List<Card> allCard = new List<Card>();

    public GameObject cardPrefab;
    public Transform cardsHolder;
    void Start()
    {
        CardCreate(daimond_sprite, CardName.Daimond);
        CardCreate(heart_sprite, CardName.Heart);
        CardCreate(spade_sprite, CardName.Seade);
        CardCreate(cube_sprite, CardName.Cube);
        CardSendToPlayer();
    }

    public void CardCreate(List<Sprite> _sprite, CardName _cardName)
    {
        for (int i = 0; i < _sprite.Count; i++)
        {
            GameObject tempcard = Instantiate(cardPrefab);
            Card card = tempcard.GetComponent<Card>();
            card.transform.parent = cardsHolder;
            card.transform.localScale = new Vector3(1, 1, 1);
            card.transform.position = new Vector3(0, 0, 0);
            card.cardName = _cardName;
            card.image.sprite = _sprite[i];
            card.cardValue = i + 2;
            card.gameObject.name = _cardName.ToString() + " : " + card.cardValue;
            card.gameManager = this;
            cards.Add(card);
            allCard.Add(card);

        }
    }
    public void CardSendToPlayer()
    {

        for (int i = 0; i < players.Count; i++)
        {
            players[i].playerIndex = i;
            for (int j = 0; j < 13; j++)
            {
                Card randomCard = cards[Random.Range(0, cards.Count)];
                players[i].setCard(randomCard);
                cards.Remove(randomCard);

            }
        }

    }

    public void PlayingCard(Card _card)
    {

        cards.Add(_card);
        _card.transform.parent = cardsHolder;
        if (players[0].playerIndex == _card.cardindex)
        {
            _card.transform.DOMove(boardCards[0].transform.position, 1f);
            _card.clickTime++;

        }

        if (players[1].playerIndex == _card.cardindex)
        {
            _card.transform.DOMove(boardCards[1].transform.position, 1f);

        }

        if (players[2].playerIndex == _card.cardindex)
        {
            _card.transform.DOMove(boardCards[2].transform.position, 1f);

        }
        if (players[3].playerIndex == _card.cardindex)
        {
            _card.transform.DOMove(boardCards[3].transform.position, 1f);

        }


        for (int i = 0; i < allCard.Count; i++)
        {

            if (cards[0].cardName == allCard[i].cardName)
            {
                if (players[1].playerIndex == allCard[i].cardindex)
                {
                    Debug.Log("Active Card");
                }
            }
            else
            {
                allCard[i].image.color = Color.cyan;
                Debug.Log("InActive Card");
            }
        }

        if (cards.Count == 4)
        {
            List<Card> mixCard = new List<Card>();
            List<Card> trumpCard = new List<Card>();
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[0].cardName == cards[i].cardName && i != 0)
                {
                    mixCard.Add(cards[i]);
                    Debug.Log(mixCard.Count);
                }
                if (cards[i].cardName == CardName.Seade)
                {
                    trumpCard.Add(cards[i]);
                    Debug.Log(trumpCard.Count);
                }
            }
            if (trumpCard.Count > 0)
            {
                Card bigCard = trumpCard[0];

                for (int i = 1; i < trumpCard.Count; i++)
                {
                    if (trumpCard[i].cardValue > bigCard.cardValue)
                    {
                        bigCard = trumpCard[i];

                    }

                }
                RoundWinner(bigCard);
            }
            else
            {
                Card bigCard = mixCard[0];

                for (int i = 1; i < mixCard.Count; i++)
                {
                    if (mixCard[i].cardValue > bigCard.cardValue)
                    {
                        bigCard = mixCard[i];
                    }


                }
                RoundWinner(bigCard);
            }

        }
    }

    public void RoundWinner(Card _card)
    {

        Debug.Log(_card.cardValue);
        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].transform.DOMove(players[_card.cardindex].transform.position, 1f).SetDelay(1f);
        }
        cards.Clear();

        for (int i = 0; i < allCard.Count; i++)
        {
            allCard[i].image.color = Color.white;
        }
    }






}
