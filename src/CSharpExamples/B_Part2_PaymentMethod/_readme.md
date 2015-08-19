// This set of classes shows the C# equivalent of these 11 lines of F# code:

type CardType = Visa | Mastercard
type CardNumber = CardNumber of string

type PaymentMethod = 
  | Cash
  | Cheque of int
  | Card of CardType * CardNumber

let printPayment paymentMethod = 
    match paymentMethod with
    | Cash ->  printfn "Paid in cash"
    | Cheque checkNo -> printfn "Paid by cheque: %i" checkNo
    | Card (cardType,cardNo) -> printfn "Paid with %A %A" cardType cardNo

// This following F# examples have been converted to the C# class "Examples.cs"

// example
let paymentMethod1 = Card(Visa, CardNumber "1234")
let paymentMethod2 = Cheque 42
let paymentMethod3 = Cash

// highlight and run
printPayment paymentMethod1
printPayment paymentMethod2
printPayment paymentMethod3
