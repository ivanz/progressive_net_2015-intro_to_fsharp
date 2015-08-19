// ================================================
// Composition 
// ================================================

let add1 x = x + 1
let double x = x * 2
let square x = x * x



let add1_double = 
  add1 >> double

add1_double 5 



let add1_double_square = 
   add1 >> double >> square

add1_double_square 5 
