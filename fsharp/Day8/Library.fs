namespace Day8

module Part1 =
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
                row.[y-1]
        | Bottom -> 
                let row = trees |> List.transpose |> List.rev 
                row.[y-1] 
        | Left -> 
                trees.[x-1]
        | Right -> 
                let row = trees 
                row.[x-1] |> List.rev 

    let checkVisibleByDirection (trees:Trees list) direction (x,y) =
            let isVisible (treeRow: Trees) x =
                let tree = treeRow.[x]
                treeRow
                    |> List.take x
                    |> List.exists (fun t -> t >= tree)
                    |> not
                
            let row = getRow trees direction (x,y)


            match direction with
            | Top
            | Left -> x-1 |> isVisible row 
            | Bottom
            | Right -> 
                let nx = (row |> List.length, x) ||> (-) 
                isVisible row nx

    let visibleTrees (trees:Trees list): int =
        trees 
        |> List.mapi (fun y r -> 
                List.mapi (fun x el -> 
                    checkVisibleByDirection trees Top (x+1,y+1) &&
                    checkVisibleByDirection trees Bottom (x+1,y+1) &&
                    checkVisibleByDirection trees Left (x+1,y+1) &&
                    checkVisibleByDirection trees Right (x+1,y+1) 
                ) r
            )
        |> List.map (fun r -> List.filter id r |> List.length)
        |> List.sum