namespace Day5

module Part1 =

  


    type Crate(tag: string) =
        let _tag = match tag.[0] with
                    | '[' -> tag.Trim().Substring(1,1)
                    | _ -> tag

        member this.ToString = Printf.sprintf "[%s] " _tag
        member this.Badge with get() = _tag

    type Stack = list<Crate>
    type CrateHolder = list<Stack>

    let toString (c:Crate) = c.ToString
        
  


    let private removeLast (s:CrateHolder):CrateHolder = s |> List.rev |> List.tail |> List.rev

    let private normalizeLength crateholder:CrateHolder = 
        let maxSize = crateholder 
                        |> List.map List.length 
                        |> List.max

        crateholder |> List.map (fun (l:Stack) -> match l |> List.length with
                                            | x when x = maxSize -> l
                                            | x -> [ 
                                                     l; 
                                                     [ for i in 1 .. maxSize - x -> Crate("[ ]") ]
                                                   ] |> List.concat 
                                 )
    let printCrateHolder (s:CrateHolder) = 
        let print (rowId:int) (c:list<Crate>) =
            printf "Row %d\t" rowId
            c |> List.iteri (fun i x -> match x.Badge with
                                        | " " when rowId = 0 -> printf " %s  \t" " "
                                        | _ -> printf "%s\t" x.ToString
                                        )
            printf "\n"

        let colNo = s.Length
        s 
        |> normalizeLength
        |> List.transpose
        |> (fun l -> 
                l |> List.iteri print
                printf "\t\t"
                [1..colNo] |> List.iter (fun idx -> printf " %d  \t" idx)
                printf "\n"
        )

    let parse (content: string):CrateHolder =
        content.Split("\r\n") 
        |> Seq.filter (fun s -> s.Length > 0)
        |> Seq.map
            (fun r ->
                r
                |> Seq.chunkBySize 4
                |> Seq.map (fun s -> s
                                    |> System.String 
                                    |> (fun s -> s.Trim().PadLeft(1))
                                    |> Crate) 
                |> Seq.toList
            )
        |> Seq.toList
        |> removeLast 
        |> normalizeLength
        |> List.transpose

    let solve (content: string) = 0

    let processAction (action: string) = 0


    // move 1 from 2 to 1
    // move 3 from 1 to 3
    // move 2 from 2 to 1
    // move 1 from 1 to 2
    type Command = { move: int; src: int; dest: int; }
      

    let parseCommand (cmd:string): seq<Command> =
        let dec x = x - 1
        cmd.Split("\r\n")
        |> Seq.map (fun c -> 
                            let tok = c.Split(" ")
                            {
                                move = tok.[1] |> int |> dec;
                                src = tok.[3]|> int |> dec ;
                                dest = tok.[5]|> int |> dec
                            })

    let execCommand (crateHolder: CrateHolder) (c:Command): CrateHolder =
        let toMove: Stack = crateHolder.[c.src].[0..c.move] |> List.rev
        
        crateHolder |> List.mapi (fun i s -> match i with
                                             | x when x = c.src -> s.[c.move..]
                                             | x when x = c.dest ->  seq { 
                                                toMove; 
                                                s |> List.filter (fun s -> match s.Badge with
                                                                           | " " -> false
                                                                           | _ -> true)} |> List.concat
                                             | _ -> s)
