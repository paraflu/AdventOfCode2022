namespace Day8

module Utility =
    let dec x = x - 1
    let inc x = x + 1


module Part1 =
    open Utility

    type Direction = 
        | Top
        | Right
        | Bottom
        | Left

    type Trees = int list

    let getRow (trees:Trees list) direction (x,y) =
        match direction with
        | Top -> 
                let row = trees |> List.transpose 
                row.[x-1]
        | Bottom -> 
                let row = trees |> List.transpose  
                row.[x-1] |> List.rev
        | Left -> 
                trees.[y-1]
        | Right -> 
                let row = trees 
                row.[y-1] |> List.rev 

    let checkVisibleByDirection (trees:Trees list) direction (x,y) =
            let isVisible (treeRow: Trees) x =
                let tree = treeRow.[x]
                treeRow
                    |> List.take x
                    |> List.exists (fun t -> t >= tree)
                    |> not
                
            let row = getRow trees direction (x,y)


            match direction with
            | Top -> y-1 |> isVisible row 
            | Left -> x-1 |> isVisible row 
            | Bottom -> 
                let nx = (row |> List.length, y) ||> (-) 
                isVisible row nx
            | Right -> 
                let nx = (row |> List.length, x) ||> (-) 
                isVisible row nx

    let visibleTrees (trees:Trees list): int =
        let h = trees |> List.length |> dec
        let w = trees |> List.transpose |> List.length |> dec
        trees 
        |> List.mapi (fun y r -> 
                List.mapi (fun x el -> 
                    match (x,y) with
                    | (_,0) -> true
                    | (0,_) -> true
                    | (_,y) when y = h -> true
                    | (x,_) when x = w -> true
                    | _ -> 
                            checkVisibleByDirection trees Top (x |> inc,y |> inc) ||
                            checkVisibleByDirection trees Bottom (x |> inc,y |> inc) ||
                            checkVisibleByDirection trees Left (x |> inc,y |> inc) ||
                            checkVisibleByDirection trees Right (x |> inc,y |> inc) 
                ) r
            )
        |> List.map (fun r -> List.filter id r |> List.length)
        |> List.sum

    let getScore (maze:Trees list) direction (x,y):int =
        let tree = maze.[y-1].[x-1]

        let score row len =
            row 
            |> List.take len 
            |> List.rev
            |> List.takeWhile (fun x -> x < tree)
            |> List.length 
            |> (fun score -> match score with 
                             | x when x < len -> score |> inc
                             | _ -> score)

        let row = getRow maze direction (x,y)
        let h = maze |> List.length 
        let w = maze |> List.transpose |> List.length 
        match (x,y) with
                | (_,1) -> 0
                | (1,_) -> 0
                | (_,y) when y = h -> 0
                | (x,_) when x = w -> 0
                | _ -> 
                    match direction with
                    | Top -> y-1 |> score row 
                    | Left -> x-1 |> score row 
                    | Bottom -> 
                        let nx = (row |> List.length, y) ||> (-) 
                        score row nx
                    | Right -> 
                        let nx = (row |> List.length, x) ||> (-) 
                        score row nx

    let getScoreTree maze (x,y) =
        getScore maze Top (x,y) *
        getScore maze Bottom (x,y) *
        getScore maze Left (x,y) *
        getScore maze Right (x,y) 

    let getMaxScenicScore (maze:Trees list): int = 
        let h = maze |> List.length 
        let w = maze |> List.transpose |> List.length 
        maze
        |> List.mapi (fun y r -> 
                List.mapi (fun x el -> 
                    match (x,y) with
                    | (_,0) -> 0
                    | (0,_) -> 0
                    | (_,y) when y = h -> 0
                    | (x,_) when x = w -> 0
                    | _ -> 
                            getScore maze Top (x |> inc,y |> inc) *
                            getScore maze Bottom (x |> inc,y |> inc) *
                            getScore maze Left (x |> inc,y |> inc) *
                            getScore maze Right (x |> inc,y |> inc) 
                ) r |> List.max
            ) |> List.max