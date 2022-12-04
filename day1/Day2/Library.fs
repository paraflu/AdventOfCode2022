namespace Day2

module Main =

    type MorraCinese =
        | Rock = 1
        | Paper = 2
        | Scissor = 3

    type Result =
        | Won = 6
        | Lose = 0
        | Draw = 3

    type Match = (MorraCinese * MorraCinese)

    /// Decodifica l'azione
    let charToAction (c: char) : MorraCinese =
        match c with
        | 'A'
        | 'Y' -> MorraCinese.Rock
        | 'B'
        | 'X' -> MorraCinese.Paper
        | 'C'
        | 'Z' -> MorraCinese.Scissor
        | _ -> failwith "Impossibile decodificare l'azione"

    let getScore (m: Match) : Result =
        match m with
        | (MorraCinese.Rock, MorraCinese.Rock)
        | (MorraCinese.Scissor, MorraCinese.Scissor)
        | (MorraCinese.Paper, MorraCinese.Paper) -> Result.Draw

        | (MorraCinese.Rock, MorraCinese.Scissor)
        | (MorraCinese.Paper, MorraCinese.Rock)
        | (MorraCinese.Scissor, MorraCinese.Paper) -> Result.Won

        | (MorraCinese.Rock, MorraCinese.Paper)
        | (MorraCinese.Paper, MorraCinese.Scissor)
        | (MorraCinese.Scissor, MorraCinese.Rock) -> Result.Lose

        | other -> failwith "Impossibile decodificare l'azione"

    let makeScore (c: MorraCinese) (v: Result) : int = int (c) + int (v)

    /// Faccio il gioco 
    let game (content: string) : seq<int> =
        content.Split("\r\n")
        |> Seq.map (fun c -> c.Split(' '))
        |> Seq.map (fun p -> (p.[0].[0] |> charToAction, p.[1].[0] |> charToAction))
        |> Seq.map (fun game ->
            let r = getScore (game)
            let (p1, p2) = game
            makeScore p2 r)
