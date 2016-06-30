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
      _availableMoves = new List<IMove>();
    }

    public Table(IUnitOfWork unitOfWork, List<ICard> cards)
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

    public List<IMove> GetAvailableMoves_TESTING_ONLY()
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
        _tableau.Add(new TableauStack(i));
    }

    private void InstanciateFoundation()
    {
      _foundation = new List<FoundationStack>();
      for (int i = 0; i < NUMBER_OF_FOUNDATION_STACKS; i++)
        _foundation.Add(new FoundationStack(i));
    }

    private void instanciateHand()
    {
      _hand = new Hand();
    }

    public void Deal(List<ICard> cards)
    {
      DealTableau(cards);
      DealHand(cards);
    }

    private void DealTableau(List<ICard> cards)
    {
      for (int currentRow = 0; currentRow < NUMBER_OF_TABLEAU_STACKS; currentRow++)
        cards = DealRow(currentRow, cards);

      UpTurnEndTableauCards();
    }

    private List<ICard> DealRow(int rowToDeal, List<ICard> cards)
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

    public void SetTableauStack(int tableauStack, List<ICard> cards)
    {
      _tableau[tableauStack] = new TableauStack(cards, tableauStack);
    }

    public FoundationStack GetFoundationStack(int foundationStack)
    {
      return _foundation[foundationStack];
    }

    public void SetHand(List<ICard> cards)
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
      List<IStackable> checkedCards = new List<IStackable>();
      checkedCards.Add(CheckCardInHandAndDealHand(clonedHand));

      while (!checkedCards.Contains(clonedHand.ViewTopCard()))
        checkedCards.Add(CheckCardInHandAndDealHand(clonedHand));
    }

    private IStackable CheckCardInHandAndDealHand(Hand hand)
    {
      IStackable checkedCard = hand.ViewTopCard();
      AvailableMovesFromHandCard(checkedCard);
      hand.Deal();
      return checkedCard;
    }

    private void AvailableMovesFromHandCard(IStackable card)
    {
      if (card != null)
      {
        AvailableMovesForCardOntoFoundation<HandToFoundationMove>(card);
        AvailableMovesForCardOntoTableau<HandToTableauMove>(card);
      }
    }

    private void AvailableMovesForCardOntoFoundation<T>(IStackable card) where T : FoundationMove
    {
      foreach (FoundationStack foundationStack in _foundation)
      {
        IMove move = (IMove)Activator.CreateInstance(typeof(T), _unitOfWork, card, foundationStack.ViewTopCard());
        if (move.IsValid())
          AddToAvailableMoves(move);
      }
    }

    private void AvailableMovesForCardOntoTableau<T>(IStackable card) where T : TableauMove
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
      ICard cardToMove = PickUpCardToMove(move.GetTopCard() as ICard);
      
      MoveCardsOnToCard(cardToMove, move.GetBottomCard());
      
      _pastMoves.Add(move);
    }

    private ICard PickUpCardToMove(ICard cardToFind)
    {
      ICard foundCard = FindAndRemoveHandCard(cardToFind);
      if (foundCard == null)
        foundCard = FindAndRemoveTableauCards(cardToFind);
      return foundCard;
    }

    private ICard FindAndRemoveTableauCards(ICard cardToFind)
    {
      ICard foundCard = null;
      IHeadOfStack headOfStack = cardToFind.GetHeadOfStack();
      if (headOfStack != null)
      {
        BaseStack stack = headOfStack.GetStack();
        foundCard = stack.RemoveCard(cardToFind);
      }
      return foundCard;
    }

    private ICard FindAndRemoveHandCard(ICard cardToRemove)
    {
      return _hand.RemoveCard(cardToRemove);
    }

    private void MoveCardsOnToCard(ICard cardToMove, IStackable bottomCard)
    {
      BaseStack stack = bottomCard.GetHeadOfStack().GetStack();
      stack.PushTopCard(cardToMove as Card);
    }

    public ICard GetCard(ICard card)
    {
      ICard foundCard = null;
      foreach(ISearchableStack stack in CombineAllCardStacks())
      {
        foundCard = stack.GetCard(card);
        if (foundCard != null)
          break;
      }
      return foundCard;
    }

    private List<ISearchableStack> CombineAllCardStacks()
    {
      List<ISearchableStack> allCardStacks = _tableau.ToList<ISearchableStack>()
        .Concat(_foundation.ToList<ISearchableStack>())
        .ToList();

      allCardStacks.Add(_hand);

      return allCardStacks;
    }

    public int CountFaceDownTableauCards()
    {
      int facedownCards = 0;
      foreach (TableauStack tableauStack in _tableau)
        if (!tableauStack.ViewTopCard().IsFaceUp())
          facedownCards++;
      return facedownCards;
    }

    public bool UpTurnEndTableauCards()
    {
      bool areCardsTurned = false;
      foreach (TableauStack tableauStack in _tableau)
      {
        if (TurnCardFaceUpIfFaceDown(tableauStack))
          areCardsTurned = true;
      }
      return areCardsTurned;
    }

    private bool TurnCardFaceUpIfFaceDown(TableauStack tableauStack)
    {
      bool isCardTurned = false;
      if (!tableauStack.ViewTopCard().IsFaceUp())
      {
        ICard card = tableauStack.ViewTopCard() as ICard;
        card.TurnFaceUp();
        isCardTurned = true;
      }
      return isCardTurned;
    }

    private void DealHand(List<ICard> cards)
    {
      foreach (ICard card in cards)
        card.TurnFaceUp();
      _hand.SetCards(cards);
    }
    
    public object Clone()
    {
      Table table = new Table(_unitOfWork);
      // TODO clone of the tableau stack needs to be deeper. 
      table.SetTableau(this._tableau.Clone<TableauStack>());
      table.SetFoundation(this._foundation.Clone<FoundationStack>());
      table.SetHand((Hand)this._hand.Clone());
      Object.ReferenceEquals(_hand.ViewTopCard(), table.GetHand().ViewTopCard());
      table.SetPastMoves(this._pastMoves.Clone<IMove>());
      table.SetAvailableMoves(this._availableMoves.Clone<IMove>());
      return table;
    }
  }
}
