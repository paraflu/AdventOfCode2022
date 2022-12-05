namespace Day5

module Part1 =

    type Crate(tag: string) =
        let _tag = tag

        member this.ToString = Printf.sprintf "[%s] " _tag

    let parse (content: string) =
        content.Split("\r\n")
        |> Seq.map
            (fun r ->
                r
                |> Seq.chunkBySize 4
                |> Seq.map
                    (fun s ->
                        s
                        |> string
                        |> (fun s -> s.Trim())
                        |> (fun x -> Crate(x))))
        |> Seq.transpose

    let solve (content: string) = 0

    let processAction (action: string) = 0
