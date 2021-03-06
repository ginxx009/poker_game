﻿using System;
using System.Collections;
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

    public float x, y, z;

    [SerializeField]
    private Button[] buttons;

    //public ValueHolder valueHolder;

    // Start is called before the first frame update
    void Start()
    {
        FaceDownCard();
        //Again button
        buttons[1].gameObject.SetActive(false);
    }


    /// <summary>
    /// Facedown Card
    /// </summary>
    public void FaceDownCard()
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
        Player1CheckHand.text = "";
        Player2CheckHand.text = "";
        Result.text = "";
        buttons[1].gameObject.SetActive(false);
        buttons[0].gameObject.SetActive(true);
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
        
        StartCoroutine(GeneratesCards());
    }


    IEnumerator GeneratesCards()
    {
        foreach(var btn in buttons)
        {
            btn.gameObject.SetActive(false);
        }

        System.Random rnd = new System.Random();
        ValueHolder.shuffleDeck = ValueHolder.cardSprite.OrderBy(x => rnd.Next()).ToArray();

        //Debugging purposes
        //SCENARIO
        //ValueHolder.shuffleDeck = new string[] { "2d", "11s", "4h", "10d", "12c", "1d", "10h", "1h", "11s" };
        //ValueHolder.shuffleDeck = new string[] { "5s", "6s", "4s", "12s", "13d", "7s", "4d", "3s", "2s" };
        int tempCounter = 0;
        for (int i = 0; i < 9; i++)
        {
            tempCounter++;
            var resultCard = Resources.Load<Sprite>("cards/" + ValueHolder.shuffleDeck[i]);

            //0-1-2-3-4
            if (i <= 4)
            {
                for(int j = 0; j < 120; j++)
                {
                    card[i].transform.Rotate(new Vector3(x, y, z));
                    yield return new WaitForSeconds(0.01f);
                    card[i].sprite = resultCard;
                }
                
            }
            //5-6
            else if (i <= 6)
            {
                for(int j = 0; j < 120; j++)
                {
                    p1[i % 2].transform.Rotate(new Vector3(x, y, z));
                    yield return new WaitForSeconds(0.01f);
                    p1[i % 2].sprite = resultCard;
                }
            }
            //7-8
            else
            {
                for(int j = 0; j < 120; j++)
                {
                    p2[i % 2].transform.Rotate(new Vector3(x, y, z));
                    yield return new WaitForSeconds(0.01f);
                    p2[i % 2].sprite = resultCard;
                }
            }
        }
        GameCheckHand();
        yield return new WaitForSeconds(0.5f);
        buttons[1].gameObject.SetActive(true);
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
    }
}
