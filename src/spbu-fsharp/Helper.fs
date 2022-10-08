namespace Helpers

open CLists
open Microsoft.FSharp.Collections
open OOPLists


module ListConverters =

    /// Convert List type to OOPList type.
    let rec listToOOP list =
        match list with
        | Empty -> EmptyList() :> IList<'value>
        | Cons (head, tail) -> List(head, listToOOP tail)

    /// Convert OOPList type to List type.
    let rec oopToList (oopList: IList<'value>) =
        match oopList with
        | :? EmptyList<'value> -> Empty
        | :? List<'value> as oopList -> Cons(oopList.Head, oopToList oopList.Tail)
        | _ ->
            failwith
                $"Helpers.ListConverters.oopToList: \
                          Unexpected type was given: %A{oopList.GetType()}"

    /// Convert System type to OOPList type.
    let rec lstToOOP lst =
        match lst with
        | [] -> EmptyList() :> IList<'value>
        | head :: tail -> List(head, lstToOOP tail)

    /// Convert OOPList type to System list type.
    let rec oopToLst (lst: IList<'value>) =
        match lst with
        | :? EmptyList<'value> -> []
        | :? List<'value> as lst -> lst.Head :: oopToLst lst.Tail
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
        | Cons (head, tail) -> head :: listToLst tail
