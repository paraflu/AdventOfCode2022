namespace Day7

open System
open Microsoft.FSharpLu.Logging

module Utility =
    let tap action value =
        action value
        value

module Part1 =

    open Utility
    // https://github.com/bvnierop/advent-of-code-fsharp/blob/main/src/AdventOfCode.Solutions/2022/Day07.fs
    let rec updateSizes pwd size sizes =
        match pwd with
        | [] -> sizes   
        | _::xs -> 
            let updated = 
                sizes
                |> Map.change pwd (fun i -> Some (Option.defaultValue 0 i + size))
            updateSizes xs size updated

    let calculateSizes (lines:string list) =
        let handleLine (pwd, sizes) (cmd:string) =
            match cmd.Split(" ") with
            | [| "$"; "cd" ; "/"|] -> (["/"], sizes)
            | [| "$" ; "cd" ; ".."|] -> (List.tail pwd, sizes)
            | [| "$" ; "cd" ; dir |] -> (dir::pwd, sizes)
            | [| "$" ; "ls"|] -> (pwd, sizes)
            | [| "dir"; dir |] -> (pwd, sizes)
            | [| sizeStr; _ |] -> 
                let size = Int32.Parse(sizeStr)
                (pwd, updateSizes pwd size sizes)
            | _ -> failwith "Failed to parse"

        let (_, sizes) =
                lines
                |> List.fold handleLine ([], Map.empty)
            
        sizes
        //match cmd with 
        //| head :: tail -> 
        //    printf $"{path} : {head}\n"
        //    let subitems = 
        //        match head with
        //        | s when s.StartsWith("$ cd") -> 

        //            let newPath = s.Substring(5)
        //            printf "[cd] %s\n" newPath

        //            match newPath with
        //            | ".." -> 
        //                printf "[cd] ..\n"
        //                []
        //            | _ as subFolder ->
        //                let items = parseCommand subFolder tail
        //                printf "complete subfolder %s %A\n" subFolder items
        //                items

        //        | s when s.StartsWith("$ ls") ->
        //            [{
        //                name=path; 
        //                typ=Directory; 
        //                size=0; 
        //                items=parseCommand path tail
        //            }]
                    
        //        | row -> 
        //                match row with
        //                | x when x.StartsWith("dir") ->
        //                    //let subdir = x.Substring(4).Trim()
        //                    //let subitems = parse subdir tail
        //                    //{name=path; typ=Directory; size=0; items=[]} :: parse path tail
        //                    // per il momento non la considero
        //                    //parse path tail
        //                    parseCommand path tail
        //                | _ ->
        //                    let fspec = row.Split()
        //                    let size = fspec.[0] |> int
        //                    let name = fspec.[1]
        //                    let items = parseCommand path tail
        //                    let file = {name=name; typ=File; size=size; items=[]}
        //                    file :: items
        //    subitems
        //| [] -> []

    //let rec getSize (root:Item) (path:list<string>) = 
    //    match path with
    //    | head :: tail -> 
    //        let folder = root.items |> List.tryFind (fun x -> x.typ = Directory && x.name = head)
    //        match folder with
    //        | None -> Trace.failwith $"Impossibile trovare la cartella {head} come sotto cartella di {root.name}"
    //        | Some(f) -> getSize f tail
    //    | [] -> 
    //        match root.typ with
    //        | Directory -> root.items |> List.map (fun it -> getSize it []) |> List.sum
    //        | File -> root.size
        


        
    //let rec printFs lvl root  =
    //    let level s = "".PadLeft(lvl) + s

    //    match root.typ with
    //    | Directory -> 
    //        Printf.kprintf level "%A %s" root.typ root.name |> Console.WriteLine
    //        root.items |> List.iter (fun d -> (lvl + 1, d) ||> printFs)
    //    | File -> 
    //        Printf.kprintf level "%A %s %d" root.typ root.name root.size 
    //        |> Console.WriteLine

    //let makeRoot path cmd =
    //    {name="root"; typ=Directory; items=parseCommand path cmd; size = 0}
    //    |> tap (fun x -> printFs 0 x)
