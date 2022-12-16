namespace HomeWork5

open HomeWork4
open Microsoft.FSharp.Core

open MatrixData
open SparseVector
open SparseMatrix

module BreadthFirstSearch =

    let fAdd a b =
        match a, b with
        | Some x, _
        | _, Some x -> Some x
        | _ -> Option.None


    let fMult a b =
        match a, b with
        | Some x, Some _ -> Some x
        | _ -> Option.None


    let fVisit a b =
        match a, b with
        | Some _, Some y -> Some y
        | _ -> Option.None


    let fMask a b =
        match a, b with
        | Option.None, Some y -> Some y
        | _ -> Option.None


    let fUpdateCount iter a b =
        match a, b with
        | Option.None, Option.None -> Option.None
        | Option.None, Some _ -> Some(Some iter)
        | Some x, Option.None -> Some x
        | Some x, Some _ -> Some x


    let markVertices lst value = List.map (fun x -> x, value) lst


    let BFS (startV: List<uint>) (gMtx: COOMatrix<'A>) =

        let mtx = SparseMatrix gMtx
        let length = mtx.Rows

        let frontier = SparseVector(markVertices startV (Some()), length)

        let visited = SparseVector(markVertices startV (Some 0u), length)

        let rec inner frontier visited counter =

            let newFrontier =
                MatrixAlgebra.vecByMtx fAdd fMult frontier mtx
                |> MatrixAlgebra.elementwiseVecVec fMask visited

            if newFrontier.IsEmpty then
                visited
            else
                let newVisited =
                    MatrixAlgebra.elementwiseVecVec (fUpdateCount counter) visited newFrontier

                inner newFrontier newVisited (counter + 1u)

        if frontier.IsEmpty then
            visited
        else
            inner frontier visited 1u
