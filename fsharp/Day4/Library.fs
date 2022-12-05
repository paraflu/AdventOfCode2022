namespace Day4

module Part1 =
    /// data una stringa in format start1-stop1,start2-stop2
    /// ottengo due set con i corrispondenti range
    let makeRange (content:string): Set<int>*Set<int> =
        let elements = content.Split(',') 
                        |> Seq.map (fun (range:string) -> 
                                        let rangeArr = range.Split('-')  |> Seq.map int
                                        let start = rangeArr |> Seq.head 
                                        let stop = rangeArr |> Seq.tail |> Seq.exactlyOne
                                        seq {start .. stop}
                                    )
        (elements |> Seq.head |> Set 
        , elements |> Seq.rev |> Seq.head |> Set)

    let overlap (task1:Set<int>) (task2:Set<int>) : bool =
        match Set.intersect task1 task2 |> Set.count with
        | 0 -> false
        | _ -> true

    let headAndTail (task:Set<int>): int*int=
        (task |> Set.toSeq |> Seq.head, task |> Set.toSeq |> Seq.rev |> Seq.head)

    /// Controllo se il task1 contiene task2
    ///
    /// Se task1 e' piu' piccolo o uguale a task2 non lo contiene
    /// altrimenti verifico se si sovrappone
    let fullyContain (task1:Set<int>) (task2:Set<int>) : bool =
        let (h2, t2) = headAndTail task2

        match headAndTail task1 with
        | (bottom,top) when bottom <= h2 && top >= t2 -> true
        | (bottom,top) when bottom >= h2 && top <= t2 -> true
        | _ -> false
        

    let solve (content:string): int = 
        content.Split("\r\n") 
                |> Seq.map makeRange 
                |> Seq.filter (fun t -> t ||> overlap)
                |> Seq.filter (fun t -> t ||> fullyContain)
                |> Seq.length

module Part2 =
    let solve (content:string): int =
        content.Split("\r\n") 
                |> Seq.map Part1.makeRange 
                |> Seq.filter (fun t -> t ||> Part1.overlap)
                |> Seq.length