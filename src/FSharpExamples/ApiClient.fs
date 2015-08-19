module ApiClient

// wont compile
//let s50_bad = ApiExample.String50 "123"

// will compiler
let s50_good = ApiExample.createString50 "123"

let email1 = ApiExample.createEmailAddress "bad"
let email2 = ApiExample.createEmailAddress "abc@example.com"