namespace Day5

module List =
    let removeLast<'T> (size: int) (s:list<'T>) : list<'T> =
        let newSize = List.length s - size
        List.take newSize s

    let cut s (p: int) =
        let first = List.take p s
        let secondSize = List.length s - p

        let second =
            List.rev s |> List.take secondSize |> List.rev

        (first, second)

    let make (len:int) action =
         [ for i in 1 .. len -> action ]

module Part1 =

    let tap action value =
        action value
        value

    type Crate(tag: string) =
        let _tag =
            match tag.[0] with
            | '[' -> tag.Trim().Substring(1, 1)
            | _ -> tag

        member this.ToString = Printf.sprintf "[%s] " _tag
        member this.Badge = _tag

    type Stack = list<Crate>

    type CrateHolder = list<Stack>

    let toString (c: Crate) = c.ToString

    let normalizeLength (crateholder: CrateHolder) : CrateHolder =
        let maxSize =
            crateholder |> List.map List.length |> List.max

        let make (maxSize: int) (l: Stack) : Stack =
            match l |> List.length with
            | x when x = maxSize -> l
            | x ->
                let filler =
                    [ for i in 1 .. maxSize - x -> Crate("[ ]") ]

                seq {
                    l
                    filler
                }
                |> List.concat
                //|> (fun x ->
                //    x
                //    |> List.map toString
                //    //|> List.iter (fun s -> printf "%s" s)

                //    x)

        let fn = make maxSize
        crateholder |> List.map fn

    let printCrateHolder (s: CrateHolder) =
        let print (rowId: int) (c: list<Crate>) =
            printf "Row %d\t" rowId

            c
            |> List.iteri (fun i x ->
                match x.Badge with
                | " " when rowId = 0 -> printf " %s  \t" " "
                | _ -> printf "%s\t" x.ToString)

            printf "\n"

        let colNo = s.Length

        s
        |> normalizeLength
        |> List.transpose
        |> (fun l ->
            l |> List.iteri print
            printf "\t\t"

            [ 1 .. colNo ]
            |> List.iter (fun idx -> printf " %d  \t" idx)

            printf "\n")

    let parse (content: string) : CrateHolder =
        content.Split "\n"
        // filter empty rows
        |> Seq.filter (fun s -> s.TrimEnd().Length > 0)
        // chunk of 4 char
        |> Seq.map (fun r ->
            r.TrimEnd()
            |> Seq.chunkBySize 4
            // convert string to Crate (padding empty token)
            |> Seq.map (fun s ->
                s
                |> System.String
                |> (fun s -> s.Trim().PadLeft(1))
                |> Crate)
            |> Seq.toList)
        |> Seq.toList
        // remove last element (idx)
        |> List.removeLast<Stack> 1
        // normalize colums/row for transpose
        |> normalizeLength
        |> List.transpose 
        |> tap printCrateHolder

    let solve (content: string) = 0

    let processAction (action: string) = 0


    // move 1 from 2 to 1
    // move 3 from 1 to 3
    // move 2 from 2 to 1
    // move 1 from 1 to 2
    type Command = { move: int; src: int; dest: int }

    let dec x = x - 1

    let parseCommand (cmd: string) : seq<Command> =

        cmd.Split("\r\n")
        |> Seq.map (fun c ->
            let tok = c.Split(" ")

            { move = tok.[1] |> int 
              src = tok.[3] |> int |> dec
              dest = tok.[5] |> int |> dec })

    let filterEmpty list = List.filter (fun (c:Crate) -> match c.Badge.Trim() with
                                                            | "" -> false
                                                            | _ -> true) list

    let execCommand (crateHolder: CrateHolder) (c: Command) : CrateHolder =
        let toMove: Stack =
            crateHolder.[c.src].[0..c.move |> dec] |> List.rev

        let empty:Stack = 
            let emptyCrate = Crate(" ")
            List.make c.move emptyCrate

        crateHolder
        |> List.mapi (fun i s ->
            match i with
            | x when x = c.src -> 
                let rest = s.[c.move..];
                seq { empty; rest } |> List.concat
            | x when x = c.dest ->
                seq {
                    toMove; s |> filterEmpty
                }
                |> List.concat
            | _ -> s)

   
    let getFirst list = filterEmpty list |> List.head

    let getHeader (c:CrateHolder):seq<char> =
        c |> List.map getFirst |> List.map (fun x -> x.Badge.[0]) |> List.toSeq
        