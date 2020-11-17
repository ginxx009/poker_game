using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameRules : MonoBehaviour
{

    /// <summary>
    /// Check the hand of all the cards present on the board
    /// </summary>
    public static void CheckHandResult()
    {
        ConvertCards();
        PokerHandAlgoPlayer1(ValueHolder.cardplaceholderValue, ValueHolder.p1val);
        PokerHandAlgoPlayer2(ValueHolder.cardplaceholderValue, ValueHolder.p2val);
    }

    /// <summary>
    /// Converts string values to its original value 1h,1d,1c,1s = ACE(1) and so on...
    /// </summary>
    public static void ConvertCards()
    {
        //get all the value to store in the reserved value for the straightflush and royalflush
        ValueHolder.reservedValuePlayer1[0] = ValueHolder.shuffleDeck[0];
        ValueHolder.reservedValuePlayer1[1] = ValueHolder.shuffleDeck[1];
        ValueHolder.reservedValuePlayer1[2] = ValueHolder.shuffleDeck[2];
        ValueHolder.reservedValuePlayer1[3] = ValueHolder.shuffleDeck[3];
        ValueHolder.reservedValuePlayer1[4] = ValueHolder.shuffleDeck[4];
        ValueHolder.reservedValuePlayer1[5] = ValueHolder.shuffleDeck[5];
        ValueHolder.reservedValuePlayer1[6] = ValueHolder.shuffleDeck[6];

        ValueHolder.reservedValuePlayer2[0] = ValueHolder.shuffleDeck[0];
        ValueHolder.reservedValuePlayer2[1] = ValueHolder.shuffleDeck[1];
        ValueHolder.reservedValuePlayer2[2] = ValueHolder.shuffleDeck[2];
        ValueHolder.reservedValuePlayer2[3] = ValueHolder.shuffleDeck[3];
        ValueHolder.reservedValuePlayer2[4] = ValueHolder.shuffleDeck[4];
        ValueHolder.reservedValuePlayer2[5] = ValueHolder.shuffleDeck[7];
        ValueHolder.reservedValuePlayer2[6] = ValueHolder.shuffleDeck[8];

        for (int i = 0; i < 9; i++)
        {

            if (ValueHolder.shuffleDeck[i].Length == 2)
            {
                //Remove the 2nd character
                ValueHolder.shuffleDeck[i] = ValueHolder.shuffleDeck[i].Remove(1, 1);
                //Debug.Log(ValueHolder.shuffleDeck[i]);
            }
            else
            {
                //Remove the 3rd character
                ValueHolder.shuffleDeck[i] = ValueHolder.shuffleDeck[i].Remove(2, 1);
                //Debug.Log(ValueHolder.shuffleDeck[i]);
            }

            //Set cardplaceholder,player1 and player2 values from the new shuffle deck
            if (i <= 4)
            {
                ValueHolder.cardplaceholderValue[i] = ValueHolder.shuffleDeck[i];
            }
            else if (i <= 6)
            {
                ValueHolder.p1val[i % 2] = ValueHolder.shuffleDeck[i];
            }
            else
            {
                ValueHolder.p2val[i % 2] = ValueHolder.shuffleDeck[i];
            }
        }
    }

    /// <summary>
    /// This is for determining the type of cards
    /// Spade, Heart, Diamond, Clover
    /// </summary>
    /// <param name="cards"></param>
    public static void CardType(string[] cards)
    {
        for (int i = 0; i < 7; i++)
        {
            if (cards[i].Length == 2)
            {
                cards[i] = cards[i].Remove(0, 1);
            }
            else
            {
                cards[i] = cards[i].Remove(0, 2);
            }
        }
    }

    /// <summary>
    /// Algorithm for check the hand for player 1
    /// Note : This is only for highcards,onepair,twopairs,threeofakind,Straight,fullhouse,fourofakind
    /// Missing : Flush,StraightFlush,RoyalFlush
    /// </summary>
    public static void PokerHandAlgoPlayer1(string[] tablecards, string[] playercard)
    {
        //Combined the two array
        string[] getSevenCards = tablecards.Concat(playercard).ToArray();

        #region PAIRS
        //Look for the duplicate
        var cards = getSevenCards.GroupBy(v => v).Select(v => new { key = v.Key, val = v.Count() });
        //get the duplicate and display
        foreach (var card in cards)
        {
            if (card.val > 1)
            {
                //Debug.Log("Element : " + card.key + " has " + card.val + " duplicate(s)");
                //Count how many duplicates are there
                ValueHolder.duplicatedCounterResultPlayer1 += 1;
                //check if there's a 3 card duplicate

                if(card.val == 2)
                {
                    ValueHolder.Player1OnePair = true;
                }
                if (card.val == 3)
                {
                    //Three of a kind
                    ValueHolder.Player1ThreeOfAKind = true;
                }
                if(card.val == 4)
                {
                    //Four of a kind
                    ValueHolder.Player1FourOfAKind = true;
                }
            }
        }
        #endregion

        #region STRAIGHT
        //Convert string array to int
        var stringToIntCard = getSevenCards.Select(int.Parse).ToArray();
        //check if straight
        CheckStraightPlayer1(stringToIntCard);
        #endregion

        #region FLUSH
        isFlushPlayer1();
        #endregion

        #region ROYAL FLUSH
        isRoyalFlushPlayer1(stringToIntCard);
        #endregion
    }

    /// <summary>
    /// Method for check if the card is a straight
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool CheckStraightPlayer1(int[] cards)
   {
        //Sort first to ascending 
        var orderedCards = cards.OrderBy(n => n).ToArray();

        var resCard = orderedCards.Zip(orderedCards.Skip(1), (a, b) => b - a);
        for(int i = 0; i < cards.Length - 5; i++)
        {
            var skipped = cards.Skip(i);
            var possibleStraight = skipped.Take(5);
            var count = 0;
            foreach(var n in resCard)
            {
                if(n==1)
                {
                    count++;
                    if(count == 4)
                    {
                        ValueHolder.Player1Straight = true;
                        return true;
                    }
                }
                else
                {
                    count = 0;
                }
            }
        }
        ValueHolder.Player1Straight = false;
        return false;
    }

    /// <summary>
    /// Check if the card is Flush
    /// </summary>
    /// <returns></returns>
    public static bool isFlushPlayer1()
    {
        var getCardType = ValueHolder.reservedValuePlayer1;
        CardType(getCardType);
        var res = getCardType.GroupBy(v => v).Select(v => new { key = v.Key, val = v.Count() });
        foreach (var r in res)
        {
            if (r.val > 1)
            {
                if(r.val >= 5)
                {
                    ValueHolder.Player1Flush = true;
                    return true;
                }
            }
        }
        ValueHolder.Player1Flush = false;
        return false;
    }
   
    /// <summary>
    /// Check if the card is Royal Flush
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool isRoyalFlushPlayer1(int[] cards)
    {
        if (Array.Exists(cards, element => element == 10) && Array.Exists(cards, element => element == 11) && Array.Exists(cards, element => element == 12)
          && Array.Exists(cards, element => element == 13) && Array.Exists(cards, element => element == 1))
        {
            ValueHolder.Player1RoyalFlush = true;
            return true;
        }
        ValueHolder.Player1RoyalFlush = false;
        return false;
    }



    
    /// <summary>
    /// Algorithm for check the hand for player2
    /// Note : This is only for highcards,onepair,twopairs,threeofakind,fullhouse,fourofakind
    /// </summary>
    public static void PokerHandAlgoPlayer2(string[] tablecards, string[] playercard)
    {
        //Combined the two array
        string[] getSevenCards = tablecards.Concat(playercard).ToArray();

        #region PAIRS
        //Look for the duplicate
        var cards = getSevenCards.GroupBy(v => v).Select(v => new { key = v.Key, val = v.Count() });
        //get the duplicate and display
        foreach (var card in cards)
        {
            if (card.val > 1)
            {
                //Debug.Log("Element : " + card.key + " has " + card.val + " duplicate(s)");
                //Count how many duplicates are there
                ValueHolder.duplicatedCounterResultPlayer2 += 1;
                //check if there's a 3 card duplicate

                if (card.val == 2)
                {
                    ValueHolder.Player2OnePair = true;
                }
                if (card.val == 3)
                {
                    //Three of a kind
                    ValueHolder.Player2ThreeOfAKind = true;
                }
                if (card.val == 4)
                {
                    //Four of a kind
                    ValueHolder.Player2FourOfAKind = true;
                }
            }
        }
        #endregion

        #region STRAIGHT
        //Convert string array to int
        var stringToIntCard = getSevenCards.Select(int.Parse).ToArray();
        //check if straight
        CheckStraightPlayer2(stringToIntCard);
        #endregion

        #region FLUSH
        isFlushPlayer2();
        #endregion

        #region ROYAL FLUSH
        isRoyalFlushPlayer2(stringToIntCard);
        #endregion
    }


    /// <summary>
    /// Method for check if the card is a straight
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool CheckStraightPlayer2(int[] cards)
    {
        //Sort first to ascending 
        var orderedCards = cards.OrderBy(n => n).ToArray();

        var resCard = orderedCards.Zip(orderedCards.Skip(1), (a, b) => b - a);
        for (int i = 0; i < cards.Length - 5; i++)
        {
            var skipped = cards.Skip(i);
            var possibleStraight = skipped.Take(5);
            var count = 0;
            foreach (var n in resCard)
            {
                if (n == 1)
                {
                    count++;
                    if (count == 4)
                    {
                        ValueHolder.Player2Straight = true;
                        return true;
                    }
                }
                else
                {
                    count = 0;
                }
            }
        }
        ValueHolder.Player2Straight = false;
        return false;
    }

    /// <summary>
    /// Check if the card is Flush
    /// </summary>
    /// <returns></returns>
    public static bool isFlushPlayer2()
    {
        var getCardType = ValueHolder.reservedValuePlayer2;
        CardType(getCardType);
        var res = getCardType.GroupBy(v => v).Select(v => new { key = v.Key, val = v.Count() });
        foreach (var r in res)
        {
            if (r.val > 1)
            {
                if (r.val >= 5)
                {
                    ValueHolder.Player2Flush = true;
                    return true;
                }
            }
        }
        ValueHolder.Player2Flush = false;
        return false;
    }

    /// <summary>
    /// Check if the card is Royal Flush
    /// </summary>
    /// <param name="cards"></param>
    /// <returns></returns>
    public static bool isRoyalFlushPlayer2(int[] cards)
    {
        if (Array.Exists(cards, element => element == 10) && Array.Exists(cards, element => element == 11) && Array.Exists(cards, element => element == 12)
          && Array.Exists(cards, element => element == 13) && Array.Exists(cards, element => element == 1))
        {
            ValueHolder.Player2RoyalFlush = true;
            return true;
        }
        ValueHolder.Player2RoyalFlush = false;
        return false;
    }



    /// <summary>
    /// Displaying the check hand on the UI
    /// </summary>
    /// <param name="checkHand"></param>
    public static void DisplayCheckHandPlayer1(UnityEngine.UI.Text checkHand)
    {
        if (ValueHolder.duplicatedCounterResultPlayer1 == 1)
        {
            if (ValueHolder.Player1FourOfAKind)
            {
                checkHand.text = ValueHolder.FourOfAKind;
                ValueHolder.Player1 = 8;
            }

            if (ValueHolder.Player1ThreeOfAKind)
            {
                checkHand.text = ValueHolder.ThreeOfAKind;
                ValueHolder.Player1 = 4;
            }

            if (ValueHolder.Player1OnePair && !ValueHolder.Player1Straight && !ValueHolder.Player1Flush
                && !ValueHolder.Player1RoyalFlush)
            {
                checkHand.text = ValueHolder.OnePair;
                ValueHolder.Player1 = 2;
            }
            if(ValueHolder.Player1OnePair && ValueHolder.Player1Straight && !ValueHolder.Player1Flush
                && !ValueHolder.Player1RoyalFlush)
            {
                checkHand.text = ValueHolder.Straight;
                ValueHolder.Player1 = 5;
            }
            if (ValueHolder.Player1OnePair && !ValueHolder.Player1Straight && ValueHolder.Player1Flush
                && !ValueHolder.Player1RoyalFlush)
            {
                checkHand.text = ValueHolder.Flush;
                ValueHolder.Player1 = 7;
            }
            if (ValueHolder.Player1OnePair && ValueHolder.Player1Straight && ValueHolder.Player1Flush
                && !ValueHolder.Player1RoyalFlush)
            {
                checkHand.text = ValueHolder.StraightFlush;
                ValueHolder.Player1 = 9;
            }
            if (ValueHolder.Player1OnePair && !ValueHolder.Player1Straight && ValueHolder.Player1Flush
                && ValueHolder.Player1RoyalFlush)
            {
                checkHand.text = ValueHolder.RoyalFlush;
                ValueHolder.Player1 = 10;
            }
        }
        else if (ValueHolder.duplicatedCounterResultPlayer1 == 2)
        {
            if (ValueHolder.Player1ThreeOfAKind)
            {
                checkHand.text = ValueHolder.FullHouse;
                ValueHolder.Player1 = 6;
            }
            else
            {
                ValueHolder.Player1TwoPairs = true;
                checkHand.text = ValueHolder.TwoPairs;
                ValueHolder.Player1 = 3;
            }
        }
        else if(ValueHolder.duplicatedCounterResultPlayer1 == 3)
        {
            ValueHolder.Player1TwoPairs = true;
            checkHand.text = ValueHolder.TwoPairs;
            ValueHolder.Player1 = 3;
        }
        else
        {
            if (ValueHolder.Player1Straight && ValueHolder.Player1Flush && !ValueHolder.Player1RoyalFlush)
            {
                checkHand.text = ValueHolder.StraightFlush;
                ValueHolder.Player1 = 9;
            }
            else if (ValueHolder.Player1Straight && !ValueHolder.Player1Flush && !ValueHolder.Player1RoyalFlush)
            {
                checkHand.text = ValueHolder.Straight;
                ValueHolder.Player1 = 5;
            }
            else if(ValueHolder.Player1Flush && !ValueHolder.Player1Straight && !ValueHolder.Player1RoyalFlush)
            {
                checkHand.text = ValueHolder.Flush;
                ValueHolder.Player1 = 7;
            }
            else if(ValueHolder.Player1Flush && !ValueHolder.Player1Straight && ValueHolder.Player1RoyalFlush)
            {
                checkHand.text = ValueHolder.RoyalFlush;
                ValueHolder.Player1 = 10;
            }
            else
            {
                checkHand.text = ValueHolder.HighCard;
                ValueHolder.Player1 = 1;
            }
        }
    }


    /// <summary>
    /// Displaying the check hand on the UI
    /// </summary>
    /// <param name="checkHand"></param>
    public static void DisplayCheckHandPlayer2(UnityEngine.UI.Text checkHand)
    {
        if (ValueHolder.duplicatedCounterResultPlayer2 == 1)
        {
            if (ValueHolder.Player2FourOfAKind)
            {
                checkHand.text = ValueHolder.FourOfAKind;
                ValueHolder.Player2 = 8;
            }

            if (ValueHolder.Player2ThreeOfAKind)
            {
                checkHand.text = ValueHolder.ThreeOfAKind;
                ValueHolder.Player2 = 4;
            }

            if (ValueHolder.Player2OnePair && !ValueHolder.Player2Straight && !ValueHolder.Player2Flush
                && !ValueHolder.Player2RoyalFlush)
            {
                checkHand.text = ValueHolder.OnePair;
                ValueHolder.Player2 = 2;
            }
            if (ValueHolder.Player2OnePair && ValueHolder.Player2Straight && !ValueHolder.Player2Flush
                && !ValueHolder.Player2RoyalFlush)
            {
                checkHand.text = ValueHolder.Straight;
                ValueHolder.Player2 = 5;
            }
            if (ValueHolder.Player2OnePair && !ValueHolder.Player2Straight && ValueHolder.Player2Flush
                && !ValueHolder.Player2RoyalFlush)
            {
                checkHand.text = ValueHolder.Flush;
                ValueHolder.Player2 = 7;
            }
            if (ValueHolder.Player2OnePair && ValueHolder.Player2Straight && ValueHolder.Player2Flush
                && !ValueHolder.Player2RoyalFlush)
            {
                checkHand.text = ValueHolder.StraightFlush;
                ValueHolder.Player2 = 9;
            }
            if (ValueHolder.Player2OnePair && !ValueHolder.Player2Straight && ValueHolder.Player2Flush
                && ValueHolder.Player2RoyalFlush)
            {
                checkHand.text = ValueHolder.RoyalFlush;
                ValueHolder.Player2 = 10;
            }
        }
        else if (ValueHolder.duplicatedCounterResultPlayer2 == 2)
        {
            if (ValueHolder.Player2ThreeOfAKind)
            {
                checkHand.text = ValueHolder.FullHouse;
                ValueHolder.Player2 = 6;
            }
            else
            {
                checkHand.text = ValueHolder.TwoPairs;
                ValueHolder.Player2 = 3;
            }
        }
        else if (ValueHolder.duplicatedCounterResultPlayer2 == 3)
        {
            checkHand.text = ValueHolder.TwoPairs;
            ValueHolder.Player2 = 3;
        }
        else
        {
            if (ValueHolder.Player2Straight && ValueHolder.Player2Flush && !ValueHolder.Player2RoyalFlush)
            {
                checkHand.text = ValueHolder.StraightFlush;
                ValueHolder.Player2 = 9;
            }
            else if (ValueHolder.Player2Straight && !ValueHolder.Player2Flush && !ValueHolder.Player2RoyalFlush)
            {
                checkHand.text = ValueHolder.Straight;
                ValueHolder.Player2 = 5;
            }
            else if (ValueHolder.Player2Flush && !ValueHolder.Player2Straight && !ValueHolder.Player2RoyalFlush)
            {
                checkHand.text = ValueHolder.Flush;
                ValueHolder.Player2 = 7;
            }
            else if (ValueHolder.Player2Flush && !ValueHolder.Player2Straight && ValueHolder.Player2RoyalFlush)
            {
                checkHand.text = ValueHolder.RoyalFlush;
                ValueHolder.Player2 = 10;
            }
            else
            {
                checkHand.text = ValueHolder.HighCard;
                ValueHolder.Player2 = 1;
            }
        }
    }

    public static void WinnerEvaluator(UnityEngine.UI.Text checkHand)
    {
        if(ValueHolder.Player1 > ValueHolder.Player2)
        {
            checkHand.text = "Player 1 Wins";
        }
        else if(ValueHolder.Player1 < ValueHolder.Player2)
        {
            checkHand.text = "Player 2 Wins";
        }
        else
        {
            

            //Check if one pair
            if (ValueHolder.Player1OnePair && !ValueHolder.Player1TwoPairs)
            {
                Checker(checkHand);
            }
            //check if two pairs
            else if (ValueHolder.Player1OnePair && ValueHolder.Player1TwoPairs)
            {
                Checker(checkHand);
            }
            //Three of a kind
            else if(ValueHolder.Player1ThreeOfAKind)
            {
                Checker(checkHand);
            }
            //Straight
            else if(ValueHolder.Player1Straight)
            {
                Checker(checkHand);
            }
            else if(ValueHolder.Player1FullHouse)
            {
                Checker(checkHand);
            }
            else if(ValueHolder.Player1Flush)
            {
                Checker(checkHand);
            }
            else if(ValueHolder.Player1FourOfAKind)
            {
                Checker(checkHand);
            }
            else if(ValueHolder.Player1StraightFlush)
            {
                Checker(checkHand);
            }
            else if(ValueHolder.Player1RoyalFlush)
            {
                Checker(checkHand);
            }
            else
            {
                Checker(checkHand);
            }


        }
    }

    public static void Checker(UnityEngine.UI.Text checkHand)
    {
        //Means Tie
        //So let's check the value of each players cards

        //Combine Arrays of cards and player
        var player1Array = ValueHolder.p1val;
        var player2Array = ValueHolder.p2val;
        //Make it int
        var player1Convert = player1Array.Select(int.Parse).ToArray();
        var player2Convert = player2Array.Select(int.Parse).ToArray();

        //sort to ascending
        var player1 = player1Convert.OrderBy(n => n).ToArray();
        var player2 = player2Convert.OrderBy(n => n).ToArray();

        //Check what is the duplicated number
        var duplicatedCardValuePlayer1 = player1.GroupBy(v => v).Select(v => new { key = v.Key, val = v.Count() });
        var duplicatedCardValuePlayer2 = player2.GroupBy(v => v).Select(v => new { key = v.Key, val = v.Count() });


        int highestNumPlayer1 = 0;
        int highestNumPlayer2 = 0;
        foreach (var dc in duplicatedCardValuePlayer1)
        {
            if (dc.val >= 1)
            {
                highestNumPlayer1 = dc.key;
            }
        }
        foreach (var dc in duplicatedCardValuePlayer2)
        {
            if (dc.val >= 1)
            {
                highestNumPlayer2 = dc.key;
            }
        }
        if (highestNumPlayer1 > highestNumPlayer2)
        {
            checkHand.text = "Player 1 Win";
        }
        else if (highestNumPlayer1 < highestNumPlayer2)
        {
            checkHand.text = "Player 2 Win";
        }
        else
        {
            checkHand.text = "TIE";
        }
    }
}
