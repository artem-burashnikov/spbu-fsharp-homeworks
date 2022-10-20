module Task3Tests

open CLists
open HomeWork3.NTrees
open Expecto
open FsCheck
open Microsoft.FSharp.Core

module TestCases =

    let config = { Config.Default with MaxTest = 10000 }

    [<Tests>]
    let tests =

        testList "samples" [

            testCase "A single source Node with no children"
                <| fun _ ->
                    let input = Node("abba", Empty)
                    let expectedResult = Cons("abba", Empty), 1
                    let actualResult = makeCList input
                    Expect.equal expectedResult actualResult "Failed to produce a correct list \
                        on a single source node."

            testCase "A single source Leaf with no children"
                <| fun _ ->
                    let input = Node(true, Empty)
                    let expectedResult = Cons(true, Empty), 1
                    let actualResult = makeCList input
                    Expect.equal expectedResult actualResult "Failed to produce a correct list \
                        on a single source node."

            testCase "A source Node which has a single child (values of both match)"
                <| fun _ ->
                    let input = Node("abba", Cons(Leaf("abba"), Empty))
                    let expectedResult = Cons("abba", Cons("abba", Empty)), 1
                    let actualResult = makeCList input
                    Expect.equal expectedResult actualResult "Failed to produce a correct list \
                        on a source node with a single child (values of both match)."

            testCase "A source Node which has a single child (values of both differ)"
                <| fun _ ->
                    let input = Node(1, Cons(Leaf(2), Empty))
                    let expectedResult = Cons(1, Cons(2, Empty)), 2
                    let actualResult = makeCList input
                    Expect.equal expectedResult actualResult "Failed to produce a correct list \
                        on a source node with a single child (values of both differ)."

            testCase "A source node with two children and a child's child (all values differ)"
                <| fun _ ->
                    let input = Node(1, Cons(Node(2, Cons(Leaf(3), Empty)), Cons(Leaf(4), Empty)))
                    let expectedResult = Cons(1, Cons(2, Cons(3, Cons(4, Empty)))), 4
                    let actualResult = makeCList input
                    Expect.equal expectedResult actualResult "Failed to produce a correct list \
                        on a source node with two children (all values differ)"

            testCase "A source node with two children and a child's child (all values match)"
                <| fun _ ->
                    let input = Node("abba", Cons(Node("abba", Cons(Leaf("abba"), Empty)), Cons(Leaf("abba"), Empty)))
                    let expectedResult = Cons("abba", Cons("abba", Cons("abba", Cons("abba", Empty)))), 1
                    let actualResult = makeCList input
                    Expect.equal expectedResult actualResult "Failed to produce a correct list \
                        on a source node with two children and a child's child (all values match)"

            testProperty "Number of unique elements in a tree must be no more than number of all elements."
               <| fun tree ->
                    let lst = makeCList tree
                    let length = getLength (fst lst)
                    snd lst <= length

]