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

        member this.ToString = match _tag with 
                                | " " -> "    "
                                | "?" -> "??? "
                                | "x" -> "xxx "
                                | x -> Printf.sprintf "[%s] " x

        member this.Badge =  match _tag with
                             | "?" 
                             | "x" -> " "
                             | x -> x

    type Stack = list<Crate>

    type CrateHolder = list<Stack>

    let toString (c: Crate) = c.ToString

    let filterEmpty list = List.filter (fun (c:Crate) -> match c.Badge.Trim() with
                                                            | ""
                                                            | "?"
                                                            | "x" -> false
                                                            | _ -> true) list

    let padLeft el size li = 
        match li |> List.length with
        | len when len = size -> li
        | x -> let filler = [1..size-x] 
                          |> List.map (fun _ -> el) 
               seq {li ;filler} |> List.concat

    let padRight el size li =
        match li |> List.length with
        | len when len = size -> li
        | x -> let filler = [1..size-x] 
                          |> List.map (fun _ -> el) 
               seq {filler;li} |> List.concat

    let trimLeft (li:Stack) = 
        li |> List.skipWhile (fun it -> match it.Badge with
                                        | " " -> false
                                        | _ -> true)
    
    let trimRight (li:Stack) = 
        li 
        |> List.rev
        |> List.takeWhile (fun it -> match it.Badge with
                                        | " " -> false
                                        | _ -> true)
        |> List.rev


    let normalizeLength (crateholder: CrateHolder) : CrateHolder =
        let maxSize =
            crateholder |> List.map List.length |> List.max

        
        //(make maxSize,crateholder) ||> List.map 
        crateholder |> List.map (fun st -> (Crate(" "), maxSize, st) |||> padLeft )

    let printStack (s:Stack) = 
        s
        |> List.iteri (fun i x ->
            match x.Badge with
            | " " -> printf "... \t"
            | _ -> printf "%s\t" x.ToString)

    let printCrateHolder (s: CrateHolder) =
        let print (rowId: int) (c: list<Crate>) =
            //printf "Row %d\t" rowId
            printStack c
            printf "\n"

        let transposes = s |> normalizeLength

        let colNo = transposes.Length

        transposes        
            |> List.transpose
            |> (fun l -> 
                        l |> List.iteri print
                        printf "\t\t"
                        [ 1 .. colNo ]
                        |> List.iter (fun idx -> printf " %d  \t" idx)

                        printf "\n")


        

    let parse (content: array<string>) : CrateHolder =
        let stringToStack (spec:string): Stack =
            spec
            |> Seq.chunkBySize 4
            // convert string to Crate (padding empty token)
            |> Seq.map (fun s ->
                s
                |> System.String
                |> (fun s -> s.Trim().PadLeft(1))
                |> Crate)
            |> Seq.toList
            //|> tap (fun s -> printf "from %s\n" spec
            //                 printStack s
            //                 printf "\n")

        content
        // filter empty rows
        |> Seq.filter (fun s -> s.TrimEnd().Length > 0)
        // chunk of 4 char
        |> Seq.map stringToStack
        |> Seq.toList
        // remove last element (idx)
        |> List.removeLast<Stack> 1
        // normalize colums/row for transpose
        |> normalizeLength
        //|> tap (fun c -> printf "dopo normalize\n"; c |> List.iter (fun s -> printStack s; printf "\n"))

        |> List.transpose 
        //|> tap (fun c -> printf "parse\n"; printCrateHolder c)

 

    let processAction (action: string) = 0


    // move 1 from 2 to 1
    // move 3 from 1 to 3
    // move 2 from 2 to 1
    // move 1 from 1 to 2
    type Command = { move: int; src: int; dest: int }

    let dec x = x - 1

    let parseCommand (cmd:string): Command =
        let tok = cmd.Split(" ")

        { move = tok.[1] |> int 
          src = tok.[3] |> int |> dec
          dest = tok.[5] |> int |> dec }

    let parseCommandList (cmd: string) : seq<Command> =
        cmd.Split("\r\n")
        |> Seq.map parseCommand


    let execCommand (holder:CrateHolder) (cmd:Command) : CrateHolder =

        //printf "execCommand Start\n"; printCrateHolder holder 
        //printf "now move from %d to %d len %d\n" cmd.src cmd.dest cmd.move
        let toMove = holder.[cmd.src] 
                     //|> tap printStack
                     |> trimRight 
                     //|> tap printStack
                     |> List.take cmd.move
                     |> List.rev
                     //|> tap printStack

                    //|> tap (fun c -> printf "tomove\n"; c |> List.iteri (fun i el -> printf "%d) %s\n" i el.ToString))
        //printf "\nexecCommand tomove\n"; printStack toMove

        let result =
            holder 
            |> List.mapi (fun i cl -> 
                    match i with
                    | x when x = cmd.src -> cl |> trimRight |> List.skip cmd.move
                                            //|> tap (fun x -> 
                                            //                printf "SRC\n"
                                            //                x |> List.iteri (fun i el -> 
                                            //                    printf "%d) %s " i el.ToString)
                                            //                printf "\n")
                    | x when x = cmd.dest -> (toMove, cl |> filterEmpty) ||> List.append 
                                                //|> tap (fun x -> 
                                                //            printf "DEST\n"
                                                //            x |> List.iteri (fun i el -> 
                                                //                printf "%d) %s " i el.ToString)
                                                //            printf "\n"
                                                //            )
                    | _ -> cl |> trimRight)
            //|> tap (fun c -> printf "after exec command\n"; printCrateHolder c)
        let max = result |> List.map (fun x -> List.length x) |> List.max
        let emptyCrate = Crate(" ")
        result 
        |> List.map (fun s ->  s 
                                //|> trimRight 
                                |> padRight emptyCrate max
                                )
        //|> tap (fun s -> printf "\nResult cmd: move %d from %d to %d\n" cmd.move cmd.src cmd.dest ; printCrateHolder s)

    let getHeader (c:CrateHolder):seq<char> =
        //printf "getHeader\n"
        //printCrateHolder c
        //printf "--\n"
        c |> List.map (fun s -> 
                            let first = s |> trimRight 

                            match first with
                            | head :: tail -> 
                                            match head.Badge with
                                            | " " -> ' '
                                            | x -> x.[0]
                            | [] -> ' '
                      ) |> List.toSeq
                      //|> tap (fun sc -> sc |> Seq.map string |> String.concat "" |> printf "getHeader result %s")
      

    let rec reduce (c:list<Command>) (crates:CrateHolder): CrateHolder =
        match c with
        | head :: tail -> let newElm = execCommand crates head
                          reduce tail newElm
        | [] -> crates
        
    let getSpecFromContent (content:string): list<string> * list<string> =
        let contentList = content.Split("\r\n") |> Array.toList
        let spec = List.takeWhile (fun (row:string) -> match row.Trim() with
                                                          | "" -> false
                                                          | _ -> true) contentList 
                           
        let commandList = (List.length spec |> (+) 1, contentList) 
                            ||> List.skip 
        (spec, commandList)
        
    let solve (content: string) = 
        //let contentList = content.Split("\r\n") |> Array.toList
        //let spec = List.takeWhile (fun (row:string) -> match row.Trim() with
        //                                                  | "" -> false
        //                                                  | _ -> true) contentList 
                           
        //let initialState = spec |> List.toArray |> parse

        //let commandList = (List.length spec |> (+) 1, contentList) 
        //                    ||> List.skip 
        //                    |> List.map parseCommand
        let (spec, commandList) = getSpecFromContent content

        let initialState = spec |> List.toArray |> parse
        let commandList = commandList |> List.map parseCommand
        
        reduce commandList initialState


module Part2 =
    let solve (content:string) = 
        ""