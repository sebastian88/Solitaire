﻿using Solitaire.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solitaire.Lib.Services
{
  public class CardService
  {
    private Random _random;

    private readonly int NUMBER_OF_TIMES_TO_SHUFFLE = 4;

    public CardService()
    {
      _random = new Random();
    }

    public List<Card> GenerateShuffledCards()
    {
      List<Card> deck = GenerateDeck();
      ShuffleCardsMultipleTimes(deck);
      return deck;
    }

    private List<Card> GenerateDeck()
    {
      List<Card> cards = new List<Card>();
      for (int i = 1; i <= 4; i++)
        for (int j = 1; j <= 13; j++)
          cards.Add(new Card((Enums.Values)j, (Enums.Suits)i, false));

      return cards;
    }

    private void ShuffleCardsMultipleTimes(List<Card> cards)
    {
      for(int i = 0; i < NUMBER_OF_TIMES_TO_SHUFFLE; i++)
        ShuffleCards(cards);
    }

    private void ShuffleCards(List<Card> cards)
    {
      List<Card> shuffledCards = new List<Card>();

      for (int i = 0; i < cards.Count; i++)
        shuffledCards.Add(RemoveRandomCard(cards));
    }

    private Card RemoveRandomCard(List<Card> cards)
    {
      int randomNumber = _random.Next(cards.Count - 1);
      Card randomCard = cards.ElementAt(randomNumber);
      cards.RemoveAt(randomNumber);
      return randomCard;
    }
  }
}
