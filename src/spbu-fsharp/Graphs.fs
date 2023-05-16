module Graphs

open SparseMatrix.SparseMatrix
open Helpers.Numbers

type Vertex = Vertex of uint

type Edge<'Label> = Edge of Vertex * Vertex * 'Label

type Graph<'A when 'A: equality and 'A: comparison>(adjMtx: SparseMatrix<'A>) =

    let verticesOfMtx (mtx: SparseMatrix<'A>) =
        Set.ofSeq (
            seq {
                for i = 1 to (toIntConv mtx.Rows) do
                    yield Vertex(uint i)
            }
        )

    let edgesOfMtx (mtx: SparseMatrix<'A>) =

        let folder i j value set =
            Set.add (Edge(Vertex i, Vertex j, value)) set

        SparseMatrix.Fold folder Set.empty mtx

    member this.Vertices = verticesOfMtx adjMtx

    member this.Edges = edgesOfMtx adjMtx

    member this.AdjMtx = adjMtx