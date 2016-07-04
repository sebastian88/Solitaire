using Solitaire.Lib.Config.Interfaces;
using Solitaire.Lib.Context.Interfaces;
using Solitaire.Lib.Objects.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.Enums;

namespace Solitaire.Lib.Objects
{
  public abstract class BaseStack : IPushableStack, IPopableStack, ISearchableStack, ICloneable
  {
    protected int _stackPosition;
    protected IStackable _headOfStack;

    public BaseStack(int stackPosition)
    {
      _stackPosition = stackPosition;
    }

    public int GetStackPosition()
    {
      return _stackPosition;
    }

    public IStackable GetHeadOfStack()
    {
      return _headOfStack;
    }

    public void SetCards(List<ICard> cards)
    {
      List<IStackable> stack = new List<IStackable>();
      foreach (ICard card in cards)
        stack.Add(card);
      SetCards(stack);
    }

    public void SetCards(List<IStackable> cards)
    {
      if (cards == null || cards.Count == 0)
        return;

      IStackable previousItem = _headOfStack;
      foreach(IStackable card in cards)
      {
        previousItem.SetNext(card);
        previousItem = card;
      }
      cards.Last().SetNext(_headOfStack);
    }

    public void PushTopCard(ICard card)
    {
      GetLastStackable().SetNext(card);
      GetLastStackable().SetNext(_headOfStack);
    }

    private IStackable GetLastStackable()
    {
      IStackable previousCard = _headOfStack;
      IStackable card = previousCard.GetNext();
      while (card != null && !card.IsHead())
      {
        previousCard = card;
        card = card.GetNext();
      }
      return previousCard;
    }

    private ICard GetLastCard()
    {
      return GetLastStackable() as ICard; 
    }

    public bool IsEmpty()
    {
      return Count() == 1;
    }

    public int Count()
    {
      int count = 1;
      IStackable card = _headOfStack.GetNext();
      while (!card.IsHead())
      {
        count++;
        card = card.GetNext();
      }
      return count;
    }

    public int CountCards()
    {
      return Count() - 1;
    }

    public int GetCardIndex(IStackable cardToFind)
    {
      int index = -1;
      int count = 0;
      IStackable stackCard = _headOfStack;
      while(count <= Count())
      {
        if (cardToFind.Equals(stackCard))
        {
          index = count;
          break;
        }
        count++;
        stackCard = stackCard.GetNext();
      }
      return index;
    }

    public IStackable ViewTopCard()
    {
      IStackable lastCard = GetHeadOfStack();
      if (!IsEmpty())
        lastCard = GetLastCard();
      return lastCard;
    }

    public IStackable PopTopCard()
    {
      IStackable secondToLastCard = GetSecondToLastCard();
      IStackable lastCard = secondToLastCard.GetNext();

      secondToLastCard.SetNext(_headOfStack);
      if(!lastCard.IsHead())
        lastCard.SetNext(null);

      return lastCard;
    }

    public IStackable GetStackable(IStackable card)
    {
      IStackable foundCard = null;
      int indexOfCard = GetCardIndex(card);
      if (indexOfCard >= 0)
        foundCard = GetCardAtIndexWithIndexCheck(indexOfCard);
      return foundCard;
    }

    public ICard RemoveCard(ICard cardToRemove)
    {
      int indexOfCard = GetCardIndex(cardToRemove);
      return RemoveCardAtIndex(indexOfCard);
    }

    protected ICard RemoveCardAtIndex(int index)
    {
      ThrowExceptionIfIndexIsOutOfRange(index);

      IStackable cardBeforeIndex = GetCardAtIndex(index - 1);
      IStackable cardAtindex = cardBeforeIndex.GetNext();
      ViewTopCard().SetNext(null);

      cardBeforeIndex.SetNext(_headOfStack);

      return cardAtindex as ICard;
    }

    private void ThrowExceptionIfIndexIsOutOfRange(int index)
    {
      if (IsIndexOutOfRange(index))
        throw new IndexOutOfRangeException();
    }

    private IStackable GetSecondToLastCard()
    {
      return GetCardAtIndexWithIndexCheck(Math.Max(0, Count() - 2));
    }

    private IStackable GetCardAtIndexWithIndexCheck(int index)
    {
      ThrowExceptionIfIndexIsOutOfRange(index);

      return GetCardAtIndex(index);
    }

    private IStackable GetCardAtIndex(int index)
    {
      IStackable currentCard = _headOfStack;
      int i = 0;
      while (i != index)
      {
        currentCard = currentCard.GetNext();
        i++;
      }

      return currentCard;
    }

    private bool IsIndexOutOfRange(int index)
    {
      return index > Count() || index < 0;
    }

    public virtual object Clone()
    {
      BaseStack clonedStack = GetClonedStack();
      CloneCards(clonedStack.GetHeadOfStack());
      return clonedStack;
    }

    public abstract BaseStack GetClonedStack();

    private void CloneCards(IStackable cloneHeadOfStack)
    {
      if (!_headOfStack.GetNext().IsHead())
      {
        cloneHeadOfStack.SetNext((IStackable)_headOfStack.GetNext().Clone());
        IStackable next = cloneHeadOfStack.GetNext();
        while (next.GetNext() != null)
          next = next.GetNext();
        next.SetNext(cloneHeadOfStack);
      }
    }

    public override string ToString()
    {
      StringBuilder sb = new StringBuilder();

      sb.Append(this.GetType().ToString() + " ");
      sb.Append(this._stackPosition + ": ");

      IStackable currentCard = _headOfStack.GetNext();
      while (!currentCard.IsHead())
      {
        sb.Append(currentCard.ToString() + ", ");
        currentCard = currentCard.GetNext();
      }

      return sb.ToString();
    }
  }
}
