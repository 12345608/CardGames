﻿using CardGames.Core.French.Dealers;
using System;
using System.Linq;
using CardGames.Core.Dealer;
using CardGames.Core.French.Cards;
using System.Collections.Generic;

namespace CardGames.Playground.Simulations.Dummy
{
    public class HeadsUpDummySimulation
    {
        private readonly Dealer<Card> _dealer;

        private HeadsUpDummySimulation(Dealer<Card> dealer)
        {
            _dealer = dealer;
        }

        public static HeadsUpDummySimulation CreateWithFullDeckDealer()
            => new HeadsUpDummySimulation(FrenchDeckDealer.WithFullDeck());

        public static HeadsUpDummySimulation Create(Dealer<Card> dealer)
            => new HeadsUpDummySimulation(dealer ?? throw new ArgumentNullException(nameof(dealer)));

        public HeadsUpResult Simulate(
            int numberOfHands,
            int numberOfCardsForPlayerOne,
            int numberOfCardsForPlayerTwo,
            Func<IReadOnlyCollection<Card>, IReadOnlyCollection<Card>, bool?> playerOneWins)
        {
            var results = Enumerable
                .Repeat(false, numberOfHands)
                .Select(_ => PlaySingleHAnd(numberOfCardsForPlayerOne, numberOfCardsForPlayerTwo, playerOneWins))
                .ToList();

            return new HeadsUpResult(results.Count(x => x == true), results.Count(x => x == false));
        }

        private bool? PlaySingleHAnd(
            int numberOfCardsForPlayerOne,
            int numberOfCardsForPlayerTwo,
            Func<IReadOnlyCollection<Card>, IReadOnlyCollection<Card>, bool?> playerOneWins)
        {
            _dealer.Shuffle();
            var cardsOfPlayerOne = _dealer.DealCards(numberOfCardsForPlayerOne);
            var cardsOfPlayerTwo = _dealer.DealCards(numberOfCardsForPlayerTwo);

            return playerOneWins(cardsOfPlayerOne, cardsOfPlayerTwo);
        }
    }
}
