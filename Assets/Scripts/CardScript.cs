using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour
{
    public Image[] card = new Image[5];
    public Image[] p1 = new Image[2];
    public Image[] p2 = new Image[2];

    public Text Player1CheckHand;
    public Text Player2CheckHand;
    public Text Result;

    //public ValueHolder valueHolder;

    // Start is called before the first frame update
    void Start()
    {
        FaceDownCard();
    }

    /// <summary>
    /// Facedown Card
    /// </summary>
    private void FaceDownCard()
    {
        //Set open cards sprite to back
        foreach (Image opencard in card)
        {
            opencard.sprite = Resources.Load<Sprite>("cards/back");
        }
        //Set player1 cards sprite to back
        foreach (Image openplayercard1 in p1)
        {
            openplayercard1.sprite = Resources.Load<Sprite>("cards/back");
        }
        //Set player2 cards sprite to back
        foreach (Image openplayercard2 in p2)
        {
            openplayercard2.sprite = Resources.Load<Sprite>("cards/back");
        }
    }

    private void GameCheckHand()
    {
        GameRules.CheckHandResult();
        GameRules.DisplayCheckHandPlayer1(Player1CheckHand);
        GameRules.DisplayCheckHandPlayer2(Player2CheckHand);
        GameRules.WinnerEvaluator(Result);
        ResetAllValues();
    }


    /// <summary>
    /// Method for generating and displaying
    /// the shuffled cards
    /// </summary>
    public void GenerateCard()
    {
        System.Random rnd = new System.Random();
        ValueHolder.shuffleDeck = ValueHolder.cardSprite.OrderBy(x => rnd.Next()).ToArray();

        //Debugging purposes
        //SCENARIO
        //ValueHolder.shuffleDeck = new string[] { "5h", "8s", "9c", "10h", "2d", "13s", "6d", "6s", "11d" };
        //ValueHolder.shuffleDeck = new string[] { "5s", "6s", "4s", "12s", "13d", "7s", "4d", "3s", "2s" };
        int tempCounter = 0;
        for (int i = 0; i < 9; i++)
        {
            tempCounter++;
            var resultCard = Resources.Load<Sprite>("cards/" + ValueHolder.shuffleDeck[i]);
            //0-1-2-3-4
            if (i <= 4)
            {
                card[i].sprite = resultCard;
            }
            //5-6
            else if (i <= 6)
            {
                p1[i % 2].sprite = resultCard;
            }
            //7-8
            else
            {
                p2[i % 2].sprite = resultCard;
            }
        }
        GameCheckHand();
    }

    private void ResetAllValues()
    {
        ValueHolder.Player1OnePair = false;
        ValueHolder.Player1TwoPairs = false;
        ValueHolder.Player1ThreeOfAKind = false;
        ValueHolder.Player1Straight = false;
        ValueHolder.Player1FullHouse = false;
        ValueHolder.Player1Flush = false;
        ValueHolder.Player1FourOfAKind = false;
        ValueHolder.Player1StraightFlush = false;
        ValueHolder.Player1RoyalFlush = false;
        ValueHolder.Player2OnePair = false;
        ValueHolder.Player2TwoPairs = false;
        ValueHolder.Player2ThreeOfAKind = false;
        ValueHolder.Player2Straight = false;
        ValueHolder.Player2FullHouse = false;
        ValueHolder.Player2Flush = false;
        ValueHolder.Player2FourOfAKind = false;
        ValueHolder.Player2StraightFlush = false;
        ValueHolder.Player2RoyalFlush = false;
        
        ValueHolder.duplicatedCounterResultPlayer1 = 0;
        ValueHolder.duplicatedCounterResultPlayer2 = 0;
        ValueHolder.Player1 = 0;
        ValueHolder.Player2 = 0;

        //Array.Clear(ValueHolder.shuffleDeck,0,ValueHolder.shuffleDeck.Length);
        //Array.Clear(ValueHolder.reservedValuePlayer1, 0, ValueHolder.reservedValuePlayer1.Length);
        //Array.Clear(ValueHolder.reservedValuePlayer2, 0, ValueHolder.reservedValuePlayer2.Length);
    }
}
