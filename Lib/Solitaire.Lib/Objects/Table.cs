using Solitaire.Lib.Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.Context.Interfaces;
using Solitaire.Lib.Comparers;
using Solitaire.Lib.Ententions;

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

    public void SetTableau(List<TableauStack> tableau)
    {
      _tableau = tableau;
    }

    public void SetFoundation(List<FoundationStack> foundation) 
    {
      _foundation = foundation;
    }

    public void SetHand(Hand hand)
    {
      _hand = hand;
    }

    public void SetPastMoves(List<IMove> pastMoves)
    {
      _pastMoves = pastMoves;
    }

    public List<IMove> GetAvailableMovesForTestingOnly()
    {
      return _availableMoves;
    }

    public void SetAvailableMoves(List<IMove> availableMoves)
    {
      _availableMoves = availableMoves;
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

      PopulateAvailableMovesFromTableau();

      PopulateAvailableMovesfromHand();

      RemoveDuplicatesFromAvailableMoves();

      RemoveMovesAlreadyTaken();

      return _availableMoves;
    }

    private void PopulateAvailableMovesFromTableau()
    {
      foreach (TableauStack tableauStack in _tableau)
      {
        AvailableMovesForCardOntoFoundation<TableauToFoundationMove>(tableauStack.ViewTopCard());
        AvailableMovesForCardOntoTableau<TableauToTableauMove>(tableauStack.ViewFirstFaceUpCard());
      }
    }

    private void PopulateAvailableMovesfromHand()
    {
      Hand clonedHand = (Hand)_hand.Clone();
      List<Card> checkedCards = new List<Card>();
      checkedCards.Add(CheckCardInHandAndDealHand(clonedHand));

      while (!checkedCards.Contains(clonedHand.ViewTopCard()))
        checkedCards.Add(CheckCardInHandAndDealHand(clonedHand));
    }

    private Card CheckCardInHandAndDealHand(Hand hand)
    {
      Card checkedCard = hand.ViewTopCard();
      AvailableMovesFromHandCard(checkedCard);
      hand.Deal();
      return checkedCard;
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
      foreach (TableauStack tableauStack in _tableau)
      {
        IMove move = (IMove)Activator.CreateInstance(typeof(T), _unitOfWork, card, tableauStack.ViewTopCard());
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

    public void RemoveMovesAlreadyTaken()
    {
      for (int i = _availableMoves.Count - 1; i >= 0; i--)
        if (_pastMoves.Contains<IMove>(_availableMoves[i], new MoveComparer()))
          _availableMoves.RemoveAt(i);
    }

    public void MakeMove(IMove move)
    {
      List<Card> cardsToMove = PickUpCardToMove(move.GetTopCard());
      
      MoveCardsOnToCard(cardsToMove, move.GetBottomCard());
      
      _pastMoves.Add(move);
    }

    private List<Card> PickUpCardToMove(Card cardToFind)
    {
      List<Card> foundCards = FindAndRemoveTableauCards(cardToFind);
      if(foundCards.Count == 0)
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

    private void MoveCardsOnToCard(List<Card> cardsToMove, Card bottomCard)
    {
      FindAndMoveCardsOntoStack(_tableau.Concat<BaseStack>(_foundation), cardsToMove, bottomCard);
    }

    private void FindAndMoveCardsOntoStack(IEnumerable<BaseStack> stacks, List<Card> cardsToMove, Card bottomCard)
    {
      foreach (BaseStack stack in stacks)
      {
        if (stack.ViewTopCard().Equals(bottomCard))
        {
          MoveCardsOntoStack(stack, cardsToMove);
          break;
        }
      }
    }

    private void MoveCardsOntoStack(BaseStack stack, List<Card> cardsToAdd)
    {
      foreach (Card card in cardsToAdd)
        stack.PushTopCard(card);
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
      Table table = (Table)this.MemberwiseClone();
      table.SetTableau(this._tableau.Clone<TableauStack>());
      table.SetFoundation(this._foundation.Clone<FoundationStack>());
      table.SetHand((Hand)this._hand.Clone());
      table.SetPastMoves(this._pastMoves.Clone<IMove>());
      table.SetAvailableMoves(this._availableMoves.Clone<IMove>());
      return table;
    }
  }
}
