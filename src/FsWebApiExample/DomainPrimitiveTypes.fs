﻿module FsWebApiExample.DomainPrimitiveTypes

open System
open FsWebApiExample.Rop 

type StringError =
    | Missing
    | MustNotBeLongerThan of int
    | DoesntMatchPattern of string

type IntegerError =
    | Missing
    | MustBePositiveInteger

module String10 =
    type T = private String10 of string

    let create (s:string) =
        match s with
        | null -> fail StringError.Missing
        | _ when s.Length > 10 -> fail (MustNotBeLongerThan 10)
        | _ -> succeed (String10 s)

    let apply f (String10 s) = f s

    let value = apply id

module String20 =
    type T = private String20 of string

    let create (s:string) =
        match s with
        | null -> fail StringError.Missing
        | _ when s.Length > 20 -> fail (MustNotBeLongerThan 20)
        | _ -> succeed (String20 s)

    let apply f (String20 s) = f s

    let value = apply id

module EmailAddress =
    type T = private EmailAddress of String20.T

    let create (s:string) =
        match String20.create s with
        | Failure errs -> Failure errs
        | Success (s20,_) ->
            if s.Contains("@") then
                succeed (EmailAddress s20)
            else
                fail (DoesntMatchPattern "*@*")

    let apply f (EmailAddress s20) =
        let s = s20 |> String20.apply id
        f s

    let value = apply id

module CustomerId  =
    type T = private CustomerId of int

    // Create a CustomerId from a nullable int
    // I'm using nullable just to demonstrate how to use it as input
    let create (i: int) =
        if i < 1 then
            fail MustBePositiveInteger
        else 
            succeed (CustomerId i)

    // Create a CustomerId from a nullable int
    // I'm using nullable just to demonstrate how to use it as input
    let createFromNullable (i: Nullable<int>) =
        if not i.HasValue then
            fail IntegerError.Missing
        else if i.Value < 1 then
            fail MustBePositiveInteger
        else 
            succeed (CustomerId i.Value)

    let apply f (CustomerId i) =
        f i

    let value = apply id


