using System;

namespace CSharpExamples.PaymentMethod
{
    class CardPaymentMethod : IPaymentMethod
    {
        public CardPaymentMethod(CardType cardType, CardNumber cardNumber)
        {
            this.CardNumber = cardNumber;
            this.CardType = cardType;
        }

        public CardType CardType { get; private set; }
        public CardNumber CardNumber { get; private set; }

        /// <summary>
        /// C# equivalent of F# code that only allows three payment types
        /// </summary>
        public TResult ProcessPayment<TResult>(
            Func<CashPaymentMethod, TResult> processCash,
            Func<ChequePaymentMethod, TResult> processCheque,
            Func<CardPaymentMethod, TResult> processCard)
        {
            return processCard(this);
        }
    }
}
