using System;

namespace CSharpExamples.PaymentMethod
{
    class Examples
    {
        public void ExampleOfPrintingCash()
        {
            var cashPayment = new CashPaymentMethod();
            cashPayment.ProcessPayment(
                cash =>
                {
                    Console.WriteLine("Paid in cash");
                    return 0;
                },
                cheque => 0,    //ignore
                card => 0    //ignore
            );
        }

        public void ExampleOfPrintingCheque()
        {
            var chequePayment = new ChequePaymentMethod(42);
            chequePayment.ProcessPayment(
                cash => 0,    //ignore
                cheque =>
                {
                    Console.WriteLine("Paid by cheque: {0}", cheque.ChequeNumber);
                    return 0;
                },
                card => 0    //ignore
            );
        }

        public void ExampleOfPrintingCard()
        {
            var cardType = CardType.Visa;
            var cardNumber = new CardNumber("1234");
            var cardPayment = new CardPaymentMethod(cardType, cardNumber);
            cardPayment.ProcessPayment(
                cash => 0,    //ignore
                cheque => 0,    //ignore
                card =>
                {
                    Console.WriteLine("Paid with : {0} {1}", card.CardType, card.CardNumber);
                    return 0;
                }
            );
        }

    }
}
