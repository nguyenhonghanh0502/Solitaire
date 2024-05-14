using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    //card name list
    [SerializeField]
    private List<string> deckCard = new List<string>();

    //face card sprite
    public Sprite[] FaceCard;

    //spawn card
    [SerializeField]
    private GameObject cardPrefab;

    [SerializeField]
    private GameObject[] posTop;

    [SerializeField]
    private GameObject[] posBottom;

    [SerializeField]
    private List<string>[] pos;

    [SerializeField]
    private List<string>[] tops;

    private List<string> pos0 = new List<string>();
    private List<string> pos1 = new List<string>();
    private List<string> pos2 = new List<string>();
    private List<string> pos3 = new List<string>();
    private List<string> pos4 = new List<string>();
    private List<string> pos5 = new List<string>();
    private List<string> pos6 = new List<string>();

    private float _yOffset = 0.2f;
    private float _zOffset = 0.03f;
    private int _numberPosCard = 7;
    private float _delaySpawnCard = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        pos = new List<string>[] {pos0, pos1,pos2,pos3,pos4,pos5,pos6};
        DealCard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static List<string> GenerateDeckCard()
    {
        List<string> newDeck = new List<string>();

        foreach(string type in DataConfig.SetCard)
        {
            foreach(string value in DataConfig.CardValues)
            {
                newDeck.Add(type + value);
            }
        }
        return newDeck;
    }

    public void DealCard()
    {
        deckCard = GenerateDeckCard();
        ShufferCard(deckCard);

        /*foreach(string card in deckCard)
        {
            //print(card);
        }*/

        SolitaireSort();
        StartCoroutine(CreateDecks());
    }

    public void ShufferCard<T>(List<T> list)
    {
        System.Random rand = new System.Random();
        int n = list.Count;
        while(n>1)
        {
            int k = rand.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    private IEnumerator CreateDecks()
    {
        for (int i = 0; i < _numberPosCard; i++)
        {
            float ySet = 0f;
            float zSet = 0f;

            foreach (string name in pos[i])
            {
                yield return new WaitForSeconds(_delaySpawnCard);
                GameObject newCard = Instantiate(cardPrefab,
                                                 new Vector3(posBottom[i].transform.position.x, posBottom[i].transform.position.y - ySet, transform.position.z - zSet),
                                                 Quaternion.identity,
                                                 posBottom[i].transform);
                newCard.name = name;
                if (name == pos[i][pos[i].Count - 1])
                {
                    newCard.GetComponent<Selectable>().FaceUp = true;
                }

                ySet += _yOffset;
                zSet += _zOffset;
            }
        }
    }

    private void SolitaireSort()
    {
        for(int i = 0; i < _numberPosCard; i++)
        {
            for (int j = i; j < _numberPosCard; j++)
            {
                pos[j].Add(deckCard.Last<string>());
                deckCard.RemoveAt(deckCard.Count - 1);
            }
        }
    }
}
