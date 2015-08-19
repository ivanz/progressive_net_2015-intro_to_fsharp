using System;

namespace CSharpExamples.PaymentMethod
{
    class ChequePaymentMethod : IPaymentMethod
    {
        public ChequePaymentMethod(int chequeNumber)
        {
            this.ChequeNumber = chequeNumber;
        }

        public int ChequeNumber { get; private set; }


        /// <summary>
        /// C# equivalent of F# code that only allows three payment types
        /// </summary>
        public TResult ProcessPayment<TResult>(
            Func<CashPaymentMethod, TResult> processCash,
            Func<ChequePaymentMethod, TResult> processCheque,
            Func<CardPaymentMethod, TResult> processCard)
        {
            return processCheque(this);
        }

    }
}
