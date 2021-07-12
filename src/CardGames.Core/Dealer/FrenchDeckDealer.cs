﻿using EluciusFTW.CardGames.Core.Cards.French;
using EluciusFTW.CardGames.Core.Deck.French;
using EluciusFTW.CardGames.Core.Random;
using System.Linq;

namespace EluciusFTW.CardGames.Core.Dealer
{
    public class FrenchDeckDealer : Dealer<Card>
    {
        private FrenchDeck _specificDeck => Deck as FrenchDeck;

        public FrenchDeckDealer(FrenchDeck deck) 
            : base(deck)
        {
        }

        public FrenchDeckDealer(FrenchDeck deck, IRandomNumberGenerator numberGenerator) 
            : base(deck, numberGenerator)
        {
        }

        public static FrenchDeckDealer WithShortDeck() 
            => new(new ShortFrenchDeck());
        
        public static FrenchDeckDealer WithFullDeck() 
            => new(new FullFrenchDeck());

        public bool TryDealCardOfValue(int value, out Card card)
        {
            var availableCards = _specificDeck
                .CardsLeftOfValue(value)
                .ToArray();

            if (!availableCards.Any())
            {
                card = default;
                return false;
            }

            card = availableCards[NumberGenerator.Next(availableCards.Length)];
            _ = Deck.GetSpecific(card);
            return true;
        }
    }
}