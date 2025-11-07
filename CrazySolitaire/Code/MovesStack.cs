using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazySolitaire.Code
{

    // this keeps track of the moves that the player has
    // done, and contains methods for reversing them
    // when the undo button is hit
    public class MovesStack{
        // the stack where the moves are stored
        public Stack<Move> stack { get; private set; }

        public MovesStack(){
            stack = new Stack<Move>();
        }

        public void Log(Move move){
            stack.Push(move);
        }

        public void Undo(){
            // retrive the most recent move
            Move lastMove = stack.Pop();

            if (lastMove != null) { 
                // if if was drawing from the deck
                if (lastMove.DrewToTalon){
                    // put the top 3 cards back on top of the deck
                    for (int i = 0; i < 3; i++) {
                        // figure out what the card is
                        Card top = Game.Talon.Cards.Peek();
                        // remove it from the Talon
                        Game.Talon.RemCard(top);
                        Game.Talon.Panel.RemCard(top);

                        // put it on top of the deck.
                        // deck is a queue, so we'll have to cycle through
                        // the whole deck

                        // make a copy of the deck to move the current deck into
                        Queue<Card> curDeckState = new Queue<Card>();

                        // loop through the Deck, saving the cards in CurDeckState
                        while (true){
                            if (Game.Deck.IsEmpty()){
                                break;
                            }else {
                                Card nextCard = Game.Deck.Acquire();
                                curDeckState.Enqueue(nextCard);
                            }
                        }

                        // add the card to the deck
                        Game.Deck.Release(top);

                        // put all the cards from CurDeckState back in the Deck;
                        while (true){
                            if (Game.Deck.IsEmpty()){
                                break;
                            }else{
                                Card nextCard = curDeckState.Dequeue();
                                Game.Deck.Release(nextCard);
                            }

                        }
                    }
                // if it was putting the talon back in the deck
                }else if (lastMove.RefreshedDeck){
                    // loop through the whole deck, putting the cards
                    // back in the talon
                    while (true){
                        if (Game.Deck.IsEmpty()) {
                            break;
                        }else{
                            Card nextCard = Game.Deck.Acquire();
                            Game.Talon.AddCard(nextCard);
                        }
                    }
                // if it was a normal move
                }else{
                    // set up the values
                    Card card = lastMove.card;
                    IDragFrom from = lastMove.from;
                    IDropTarget to = lastMove.to;

                    // set it up so it thinks this is where to snap it back to
                    FrmGame.DragCard(card);

                    if (from is TableauStack tab1) { card.conBeforeDrag = tab1.Panel; }
                    else if (from is FoundationStack foundation1) { card.conBeforeDrag = foundation1.Panel; }
                    else if (from is Talon talon1) { card.conBeforeDrag = talon1.Panel; }

                    //relLocBeforeDrag = PicBox.Location;
                    if (from is TableauStack tab2) { card.relLocBeforeDrag =  new Point(0,((tab2.Cards.Count * 20) + 20)); }
                    else if (from is FoundationStack foundation2) { card.relLocBeforeDrag = new Point(0, 0); }
                    else if (from is Talon talon2) { card.relLocBeforeDrag = new Point(((talon2.Cards.Count % 3) * 20), 0); }

                    card.conBeforeDrag.RemCard(card);
                    FrmGame.Instance.AddCard(card);
                    var abs = new Point(card.conBeforeDrag.Left + card.relLocBeforeDrag.X, card.conBeforeDrag.Top + card.relLocBeforeDrag.Y);
                    card.PicBox.Location = abs;
                    card.PicBox.BringToFront();

                    // make it snap back
                    FrmGame.CancelRunDrag();

                }
            


                // if it's a normal card move, put it back
            }
        }


    }
}
