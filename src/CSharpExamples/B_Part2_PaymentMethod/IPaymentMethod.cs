using System;

namespace CSharpExamples.PaymentMethod
{
    interface IPaymentMethod
    {
        /// <summary>
        /// C# equivalent of F# code that only allows three payment types
        /// </summary>
        TResult ProcessPayment<TResult>(
            Func<CashPaymentMethod, TResult> processCash,
            Func<ChequePaymentMethod, TResult> processCheque,
            Func<CardPaymentMethod, TResult> processCard
            );

    }
}
