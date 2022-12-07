namespace Day6

module Part1 =

    type Stream = seq<char>

    let solve (content: string) = ""

    let get_marker (stream: Stream) (markerLength: int) =
        let marker =
            stream
            |> Seq.windowed markerLength
            |> Seq.find (fun s ->
                let distinct = s |> Seq.distinct |> Seq.length

                match s |> Seq.length with
                | x when x = distinct -> true
                | _ -> false)
            |> Seq.map string
            |> String.concat ""

        let ss = stream |> string

        (marker,
         (ss.IndexOf(marker), marker |> String.length)
         ||> (+))
