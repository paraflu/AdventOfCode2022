namespace Day3

module Part1 =

    type Zaino(content:string) = 
        let full = content
          
        member this.First with get(): seq<char> = 
            let middle = content.Length / 2
            full |> Seq.take middle

        member this.Second with get() =
            let middle = content.Length / 2
            full |> Seq.skip middle

        member this.HasError with get() =
            this.Errors |> Seq.exists (fun (err, _) -> err)

        member this.Errors with get() =
            Seq.map (fun (x) -> (Seq.contains x this.Second , x)) this.First 
            |> Seq.filter (fun (err,_) -> err)

        member this.Full with get() =
            full

    let checkBackpack (content:string) =
        let zaino = Zaino(content)
        zaino.Errors

    let inline charToInt (c:char) = int c - int '0'

    let getPriority (c:char):int = 
        match c with
        | i when i >= 'a' && i <= 'z' -> charToInt i - charToInt 'a' + 1
        | i when i >= 'A' && i <= 'Z' -> charToInt i - charToInt 'A' + 27
        | _ -> failwith "Valore non valido"

    let solve (content:string):int = 
        content.Split("\r\n") 
        |> Seq.map (fun r -> Zaino(r)) 
        |> Seq.filter (fun z -> z.HasError)
        |> Seq.map (fun z -> 
                            let (_, c) = z.Errors |> Seq.head
                            c
                   )
        |> Seq.map (fun e -> getPriority e)
        |> Seq.sum


module Part2 =
    

    let parseContent (rows:seq<string>): seq<Part1.Zaino> =
        rows |> Seq.map (fun r -> Part1.Zaino(r))

    let getBadge (backpack: seq<Part1.Zaino>): char =

        match Seq.length backpack with
        | 3 -> ()
        | _ -> failwith "Numero di elementi errato"

        backpack 
        |> Array.ofSeq
        |> Seq.map (fun z -> z.Full |> Seq.toList |> Set.ofList )
        |> Seq.reduce Set.intersect
        |> Seq.head 
        

    let solve (content:string): int =
        content.Split("\r\n") |> parseContent 
        |> Seq.chunkBySize 3 
        |> Seq.map (fun chunks -> chunks |> getBadge |> Part1.getPriority)
        |> Seq.sum