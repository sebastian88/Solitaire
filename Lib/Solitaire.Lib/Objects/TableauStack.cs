using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solitaire.Lib.Objects.Interfaces;
using Solitaire.Lib.Utils;
using Solitaire.Lib.Enums;

namespace Solitaire.Lib.Objects
{
  public class TableauStack : BaseStack, IPushableStack, IPopableStack
  {

    public TableauStack(int stackPosition) 
      : base(stackPosition)
    {
      _headOfStack = new TableauStackHead(this);
    }

    public TableauStack(List<ICard> cards, int stackPosition)
      : this(stackPosition)
    {
      SetCards(cards);
    }

    public void TurnoverTopCardIfNecessary()
    {
      if (!IsEmpty())
      {
        ICard card = ViewTopCard() as ICard;
        card.TurnFaceUp();
      }
    }

    public IStackable ViewFirstFaceUpCard()
    {
      IStackable firstFaceUpCard = _headOfStack.GetNext();
      while(!firstFaceUpCard.IsHead() && !firstFaceUpCard.IsFaceUp())
        firstFaceUpCard = firstFaceUpCard.GetNext();
      
      return firstFaceUpCard;
    }

    public ICard RemoveCardAndCardsOnTop(ICard cardToFind)
    {
      ICard foundCard = null;

      int index = GetCardIndex(cardToFind);
      if (index > 0)
        foundCard = RemoveCardAtIndex(index);

      return foundCard;
    }

    public override BaseStack GetClonedStack()
    {
      return new TableauStack(_stackPosition);
    }
  }
}