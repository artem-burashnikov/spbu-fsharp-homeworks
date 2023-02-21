namespace Helpers

open CLists
open Microsoft.FSharp.Collections
open OOPLists
open System


module ListConverters =

    /// Convert List type to OOPList type.
    let rec listToOOP list =
        match list with
        | Empty -> EmptyList() :> IList<'Value>
        | Cons(head, tail) -> List(head, listToOOP tail)

    /// Convert OOPList type to List type.
    let rec oopToList (oopList: IList<'Value>) =
        match oopList with
        | :? EmptyList<'Value> -> Empty
        | :? List<'Value> as oopList -> Cons(oopList.Head, oopToList oopList.Tail)
        | _ ->
            failwith
                $"Helpers.ListConverters.oopToList: \
                          Unexpected type was given: %A{oopList.GetType()}"

    /// Convert System type to OOPList type.
    let rec lstToOOP lst =
        match lst with
        | [] -> EmptyList() :> IList<'Value>
        | head :: tail -> List(head, lstToOOP tail)

    /// Convert OOPList type to System list type.
    let rec oopToLst (lst: IList<'Value>) =
        match lst with
        | :? EmptyList<'Value> -> []
        | :? List<'Value> as lst -> lst.Head :: oopToLst lst.Tail
        | _ ->
            failwith
                $"Lists.myOOPListToList caused an exception in matching. \
                          The input was given %A{lst}"

    /// Convert System list type to List type.
    let rec lstToList lst =
        match lst with
        | [] -> Empty
        | head :: tail -> Cons(head, lstToList tail)

    /// Convert List type to System list type.
    let rec listToLst lst =
        match lst with
        | Empty -> []
        | Cons(head, tail) -> head :: listToLst tail

module Numbers =

    /// Function calculates the smallest power of two which is greater than or equal to the given integer.
    let ceilPowTwo x =

        let rec looper x acc =
            if acc >= x then acc else looper x (acc * 2u)

        if x = 0u then 1u
        elif x = 1u then 2u
        else looper x 1u

    /// Function calculates the exponent needed to get the smallest power of two which is greater than or equal to the given integer.
    let powTwo x =

        let rec looper x acc power =
            if acc >= x then power else looper x (acc * 2u) (power + 1u)

        if x = 1u then 0u else looper x 1u 0u

    let toIntConv (unsignedInt: uint) =
        try
            Convert.ToInt32(unsignedInt)
        with :? OverflowException ->
            failwith $"%A{unsignedInt} is outside the range of the Int32 type."

    let parseFloat (input: string) =
        try
            float input
        with _ ->
            failwith $"parseFloat: Invalid input: {input}"

    let parseInt (input: string) =
        try
            int input
        with _ ->
            failwith $"parseInt: Invalid input: {input}"

module GeneralFunction =

    let takeFirst (a, _, _) = a
    let takeSecond (_, a, _) = a
    let takeThird (_, _, a) = a

module Initializers =

    // initArrayWithDensity and init2DArrayWithDensity accept density parameter which values have to range from 0 to 100
    // Won't work otherwise
    let initArrayWithDensity (density: int) length =
        let arr = Array.create length Option.None

        for i in 0 .. length - 1 do
            let cellDensity = (float (i + 1) / float length) * 100.0

            if cellDensity <= density then
                arr[i] <- Some(i + 1)

        arr

    let init2DArrayWithDensity (density: int) rows columns =
        let table = Array2D.create rows columns Option.None

        for i in 0 .. rows - 1 do
            for j in 0 .. columns - 1 do
                let cellDensity = (float (i * columns + j + 1) / float (rows * columns)) * 100.0

                if cellDensity <= density then
                    table[i, j] <- Some(i + j + 1)

        table
