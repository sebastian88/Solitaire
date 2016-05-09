using Solitaire.Lib.Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.Context.Interfaces;
using Solitaire.Lib.Comparers;

namespace Solitaire.Lib.Objects
{
  public class Table : ICloneable
  {
    private readonly int NUMBER_OF_TABLEAU_STACKS = 7;
    private readonly int NUMBER_OF_FOUNDATION_STACKS = 4;

    private IUnitOfWork _unitOfWork;

    private List<TableauStack> _tableau;
    private List<FoundationStack> _foundation;
    private Hand _hand;

    private List<IMove> _pastMoves;
    private List<IMove> _availableMoves;

    public Table(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
      InstanciateTableau();
      InstanciateFoundation();
      instanciateHand();
      _pastMoves = new List<IMove>();
    }

    public Table(IUnitOfWork unitOfWork, List<Card> cards)
      :this(unitOfWork)
    {
      Deal(cards);
    }

    private void InstanciateTableau()
    {
      _tableau = new List<TableauStack>();
      for (int i = 0; i < NUMBER_OF_TABLEAU_STACKS; i++)
        _tableau.Add(new TableauStack());
    }

    private void InstanciateFoundation()
    {
      _foundation = new List<FoundationStack>();
      for (int i = 0; i < NUMBER_OF_FOUNDATION_STACKS; i++)
        _foundation.Add(new FoundationStack());
    }

    private void instanciateHand()
    {
      _hand = new Hand();
    }

    public void Deal(List<Card> cards)
    {
      DealTableau(cards);
      DealHand(cards);
    }

    private void DealTableau(List<Card> cards)
    {
      for (int currentRow = 0; currentRow < NUMBER_OF_TABLEAU_STACKS; currentRow++)
        cards = DealRow(currentRow, cards);

      UpTurnEndTableauCards();
    }

    private List<Card> DealRow(int rowToDeal, List<Card> cards)
    {
      for (int currentTableau = rowToDeal; currentTableau < NUMBER_OF_TABLEAU_STACKS; currentTableau++)
      {
        _tableau[currentTableau].PushTopCard(cards.Last());
        cards.RemoveAt(cards.Count - 1);
      }
      return cards;
    }

    public TableauStack GetTableauStack(int tableauStack)
    {
      return _tableau[tableauStack];
    }

    public void SetTableauStack(int tableauStack, List<Card> cards)
    {
      _tableau[tableauStack] = new TableauStack(cards);
    }

    public FoundationStack GetFoundationStack(int foundationStack)
    {
      return _foundation[foundationStack];
    }

    public void SetHand(List<Card> cards)
    {
      _hand = new Hand(cards);
    }

    public Hand GetHand()
    {
      return _hand;
    }

    public List<IMove> GetPastMoves()
    {
      return _pastMoves;
    }

    public List<IMove> GetAvailableMoves()
    {
      _availableMoves = new List<IMove>();

      PopulateAvailableMovesForTableau();

      PopulateAvailableMovesfromHand();

      RemoveDuplicatesFromAvailableMoves();

      return _availableMoves;
    }

    private void PopulateAvailableMovesForTableau()
    {
      foreach (TableauStack tableauStack in _tableau)
        AvailableMovesFromTableauCard(tableauStack.ViewTopCard());
    }

    private void AvailableMovesFromTableauCard(Card card)
    {
      if (card != null)
      {
        AvailableMovesForCardOntoFoundation<TableauToFoundationMove>(card);
        AvailableMovesForCardOntoTableau<TableauToTableauMove>(card);
      }
    }

    private void PopulateAvailableMovesfromHand()
    {
      Card firstHandCard = _hand.ViewTopCard();
      CheckCardInHandAndDealHand();

      while (firstHandCard != _hand.ViewTopCard())
        CheckCardInHandAndDealHand();
    }

    private void CheckCardInHandAndDealHand()
    {
      AvailableMovesFromHandCard(_hand.ViewTopCard());
      _hand.Deal();
    }

    private void AvailableMovesFromHandCard(Card card)
    {
      if (card != null)
      {
        AvailableMovesForCardOntoFoundation<HandToFoundationMove>(card);
        AvailableMovesForCardOntoTableau<HandToTableauMove>(card);
      }
    }

    private void AvailableMovesForCardOntoFoundation<T>(Card card) where T : FoundationMove
    {
      foreach (FoundationStack foundationStack in _foundation)
      {
        IMove move = (IMove)Activator.CreateInstance(typeof(T), _unitOfWork, card, foundationStack.ViewTopCard());
        if (move.IsValid())
          AddToAvailableMoves(move);
      }
    }

    private void AvailableMovesForCardOntoTableau<T>(Card card) where T : TableauMove
    {
      foreach (TableauStack foundationStack in _tableau)
      {
        IMove move = (IMove)Activator.CreateInstance(typeof(T), _unitOfWork, card, foundationStack.ViewTopCard());
        if (move.IsValid())
          AddToAvailableMoves(move);
      }
    }

    private void AddToAvailableMoves(IMove move)
    {
      if (_availableMoves == null)
        _availableMoves = new List<IMove>();
      _availableMoves.Add(move);
    }
    
    private void RemoveDuplicatesFromAvailableMoves()
    {
      _availableMoves = _availableMoves.Distinct(new MoveComparer()).ToList();
    }

    public void MakeMove(IMove move)
    {
      List<Card> cardsToMove = PickUpCardToMove(move.GetTopCard());
      
      MovesCardsOnToCard(cardsToMove, move.GetBottomCard());
      
      _pastMoves.Add(move);
    }

    private List<Card> PickUpCardToMove(Card cardToFind)
    {
      List<Card> foundCards = FindAndRemoveTableauCards(cardToFind);
      foundCards.Add(FindAndRemoveHandCard(cardToFind));
      return foundCards;
    }

    private List<Card> FindAndRemoveTableauCards(Card cardToFind)
    {
      List<Card> foundCards = null;
      int currentTableauStackindex = 0;
      do
      {
        foundCards = _tableau[currentTableauStackindex].FindCardAndCardsOnTop(cardToFind);
        currentTableauStackindex++;
      }
      while (foundCards.Count == 0 && _tableau.Count > currentTableauStackindex);

      return foundCards;
    }

    private Card FindAndRemoveHandCard(Card cardToRemove)
    {
      return _hand.RemoveCard(cardToRemove);
    }

    private void MovesCardsOnToCard(List<Card> cardsToMove, Card bottomCard)
    {
    }

    public int CountFaceDownTableauCards()
    {
      int facedownCards = 0;
      foreach (TableauStack tableauStack in _tableau)
        if (!tableauStack.ViewTopCard().IsFaceUp)
          facedownCards++;
      return facedownCards;
    }

    public void UpTurnEndTableauCards()
    {
      foreach (TableauStack tableauStack in _tableau)
        TurnCardFaceUpIfFaceDown(tableauStack);
    }

    private void TurnCardFaceUpIfFaceDown(TableauStack tableauStack)
    {
      if (!tableauStack.ViewTopCard().IsFaceUp)
        tableauStack.ViewTopCard().IsFaceUp = true;
    }

    private void DealHand(List<Card> cards)
    {
      foreach (Card card in cards)
        card.IsFaceUp = true;
      _hand.SetCards(cards);
    }
    
    public object Clone()
    {
      return this.MemberwiseClone();
    }
  }
}
