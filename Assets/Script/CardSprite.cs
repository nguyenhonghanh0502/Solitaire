using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSprite : MonoBehaviour
{
    [SerializeField]
    private Sprite faceCard;

    [SerializeField]
    private Sprite backCard;

    private SpriteRenderer _displayCard;

    private CardManager _cardManager;

    private Selectable _selectable;

    // Start is called before the first frame update
    void Start()
    {
        SetDeck();
    }

    // Update is called once per frame
    void Update()
    {
        if (_selectable.FaceUp)
        {
            _displayCard.sprite = faceCard;
        } else
        {
            _displayCard.sprite = backCard;
        }
    }

    private void SetDeck()
    {
        List<string> deck = CardManager.GenerateDeckCard();

        _cardManager = FindObjectOfType<CardManager>();

        int index = 0;

        foreach(string card in deck)
        {
            if (this.name == card)
            {
                faceCard = _cardManager.FaceCard[index];
                break;
            }
            index++;
        }
        _displayCard = GetComponent<SpriteRenderer>();
        _selectable = GetComponent<Selectable>();
    }
}
