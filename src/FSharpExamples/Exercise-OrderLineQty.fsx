
type OrderLineQty = private {value : int}
    with
    /// Public constructor
    static member Create qty = 
        if qty < 1 then
            None
        else if qty > 99 then
            None
        else
            Some {value=qty}

    /// Property 
    member this.Value = this.value

    override this.ToString() = this.value.ToString()


// Write a function that adds two OrderLineQtys