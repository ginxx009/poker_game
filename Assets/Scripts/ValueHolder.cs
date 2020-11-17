using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueHolder : MonoBehaviour
{
    /// <summary>
    /// Deck of cards
    /// </summary>
    public static int deckOfCards = 52;
    /// <summary>
    /// Stores the player 1 value card
    /// </summary>
    public static string[] p1val { get; set; } = new string[2];
    /// <summary>
    /// Stores the player 2 value card
    /// </summary>
    public static string[] p2val { get; set; } = new string[2];
    /// <summary>
    /// Stores the shuffled card value
    /// </summary>
    public static string[] shuffleDeck { get; set; }
    /// <summary>
    /// Stores the 5 card place holder
    /// </summary>
    public static string[] cardplaceholderValue { get; set; } = new string[5];
    /// <summary>
    /// Array of card sprites base on their names 
    /// Resources/Cards
    /// </summary>
    public static string[] cardSprite = new string[]
   {
        "1c", "1s", "1d", "1h", "2c", "2s", "2d", "2h", "3c", "3s", "3d", "3h",
        "4c", "4s", "4d", "4h", "5c", "5s", "5d", "5h", "6c", "6s", "6d", "6h",
        "7c", "7s", "7d", "7h", "8c", "8s", "8d", "8h", "9c", "9s", "9d", "9h",
        "10c", "10s", "10d", "10h", "11c", "11s", "11d", "11h", "12c", "12s", "12d", "12h",
        "13c", "13s", "13d", "13h",
   };

    /// <summary>
    /// Get and Set value of player 1
    /// Highest is 10 - Royal Flush
    /// Lowerst is 1 - HighCard
    /// </summary>
    public static int Player1 { get; set; } = 0;

    /// <summary>
    /// Get and Set value of player 1
    /// Highest is 10 - Royal Flush
    /// Lowerst is 1 - HighCard
    /// </summary>
    public static int Player2 { get; set; } = 0;
    
    /// <summary>
    /// Boolean for One Pair
    /// </summary>
    public static bool Player1OnePair { get; set; }
    /// <summary>
    /// Boolean for Two Pairs
    /// </summary>
    public static bool Player1TwoPairs { get; set; }
    /// <summary>
    /// Boolean for Three of a Kind
    /// </summary>
    public static bool Player1ThreeOfAKind { get; set; }
    /// <summary>
    /// Boolean for a Straight
    /// </summary>
    public static bool Player1Straight { get; set; }
    /// <summary>
    /// Boolean for a Fullhouse
    /// </summary>
    public static bool Player1FullHouse { get; set; }
    /// <summary>
    /// Boolean for a Flush
    /// </summary>
    public static bool Player1Flush { get; set; }
    /// <summary>
    /// Boolean for a Four of a Kind
    /// </summary>
    public static bool Player1FourOfAKind { get; set; }
    /// <summary>
    /// Boolean for a Stright Flush
    /// </summary>
    public static bool Player1StraightFlush { get; set; }
    /// <summary>
    /// Boolean for a RoyalFlush
    /// </summary>
    public static bool Player1RoyalFlush { get; set; }


    /// <summary>
    /// Boolean for One Pair
    /// </summary>
    public static bool Player2OnePair { get; set; }
    /// <summary>
    /// Boolean for Two Pairs
    /// </summary>
    public static bool Player2TwoPairs { get; set; }
    /// <summary>
    /// Boolean for Three of a Kind
    /// </summary>
    public static bool Player2ThreeOfAKind { get; set; }
    /// <summary>
    /// Boolean for a Straight
    /// </summary>
    public static bool Player2Straight { get; set; }
    /// <summary>
    /// Boolean for a Fullhouse
    /// </summary>
    public static bool Player2FullHouse { get; set; }
    /// <summary>
    /// Boolean for a Flush
    /// </summary>
    public static bool Player2Flush { get; set; }
    /// <summary>
    /// Boolean for a Four of a Kind
    /// </summary>
    public static bool Player2FourOfAKind { get; set; }
    /// <summary>
    /// Boolean for a Stright Flush
    /// </summary>
    public static bool Player2StraightFlush { get; set; }
    /// <summary>
    /// Boolean for a RoyalFlush
    /// </summary>
    public static bool Player2RoyalFlush { get; set; }

    /// <summary>
    /// This is where store the value of the shuffled card
    /// for the RoyalFlush and Straight Flush
    /// </summary>
    public static string[] reservedValuePlayer1 { get; set; } = new string[7];

    /// <summary>
    /// This is where store the value of the shuffled card
    /// for the RoyalFlush and Straight Flush
    /// </summary>
    public static string[] reservedValuePlayer2 { get; set; } = new string[7];

    /// <summary>
    /// Gets only the card types
    /// </summary>
    public static string[] cardType { get; set; }

    /// <summary>
    /// Duplicated items that turns out to be the result of the card value 
    /// </summary>
    public static int duplicatedCounterResultPlayer1 { get; set; } = 0;

    /// <summary>
    /// Duplicated items that turns out to be the result of the card value 
    /// </summary>
    public static int duplicatedCounterResultPlayer2 { get; set; } = 0;

    /// <summary>
    /// Pre texts
    /// </summary>
    public static string RoyalFlush = "Royal Flush";
    public static string StraightFlush = "Straight Flush";
    public static string FourOfAKind = "Four of a kind";
    public static string Flush = "Flush";
    public static string FullHouse = "Full house";
    public static string Straight = "Straight";
    public static string ThreeOfAKind = "Three of a kind";
    public static string TwoPairs = "Two Pairs";
    public static string OnePair = "One Pair";
    public static string HighCard = "High Card";


}
