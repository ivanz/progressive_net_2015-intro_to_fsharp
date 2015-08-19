using System;

namespace CSharpExamples.PaymentMethod
{
    class CashPaymentMethod : IPaymentMethod
    {
        // no properties


        /// <summary>
        /// C# equivalent of F# code that only allows three payment types
        /// </summary>
        public TResult ProcessPayment<TResult>(
            Func<CashPaymentMethod, TResult> processCash,
            Func<ChequePaymentMethod, TResult> processCheque,
            Func<CardPaymentMethod, TResult> processCard)
        {
            return processCash(this);
        }

    }
}
