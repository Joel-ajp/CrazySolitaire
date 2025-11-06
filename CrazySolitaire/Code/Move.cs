using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazySolitaire.Code
{
    // this represents a single move that the player
    // has made, and contains information about
    // what that moves was
    internal class Move{
        // the card that was moved
        public Card card { get; set; }

        // the place that the card was taken from
        public IDragFrom from { get; set; }

        // the place that the card was sent to
        public IDropTarget to { get; set; }

        // true if they drew to the talon, false if not
        public bool DrewToTalon { get; set; } = false;

        // true if they refreshed the draw deck, false if not
        public bool RefreshedDeck { get; set; } = false;

        // simple constructor
        public Move(Card card, IDragFrom from, IDropTarget to){
            this.card = card;
            this.from = from;
            this.to = to;
        }

        // overload for the constructor for when you draw from 
        // the draw deck
        public Move (Deck from){
            DrewToTalon = true;
        }

        // overload for the constructor for when you refresh the draw
        // deck
        public Move(Talon from){
            RefreshedDeck = true;
        }



    }
}
