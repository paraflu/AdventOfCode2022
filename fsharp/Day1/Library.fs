namespace Day1

open System

module Main =

    let stringToSeq (content: string) : seq<int> =
        content.Split("\r\n")
        |> Seq.map (fun s ->
            match s with
            | "" -> 0
            | c -> c |> int)

    let getMaxCalories (items: seq<int>) : seq<int> =
        Seq.scan
            (fun acc it ->
                match it with
                | 0 -> 0
                | i -> acc + i)
            0
            items

    let caloriesList content =
        content |> stringToSeq |> getMaxCalories
