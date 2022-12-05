namespace Day2

module Part2 =

    type MorraCinese =
        | Rock = 1
        | Paper = 2
        | Scissor = 3

    type Result =
        | Won = 6
        | Lost = 0
        | Draw = 3

    type Match = (MorraCinese * MorraCinese)

    /// Decodifica l'azione
    let charToAction (c: char) : MorraCinese =
        match c with
        | 'A'
        | 'X' -> MorraCinese.Rock
        | 'B'
        | 'Y' -> MorraCinese.Paper
        | 'C'
        | 'Z' -> MorraCinese.Scissor
        | _ -> failwith "Impossibile decodificare l'azione"

    let play (m: Match) : Result =
        match m with
        | (MorraCinese.Rock, MorraCinese.Rock)
        | (MorraCinese.Scissor, MorraCinese.Scissor)
        | (MorraCinese.Paper, MorraCinese.Paper) -> Result.Draw

        | (MorraCinese.Rock, MorraCinese.Scissor)
        | (MorraCinese.Paper, MorraCinese.Rock)
        | (MorraCinese.Scissor, MorraCinese.Paper) -> Result.Lost

        | (MorraCinese.Rock, MorraCinese.Paper)
        | (MorraCinese.Paper, MorraCinese.Scissor)
        | (MorraCinese.Scissor, MorraCinese.Rock) -> Result.Won

        | other -> failwith "Impossibile decodificare l'azione"

    let makeScore (c: MorraCinese) (v: Result) : int = int (c) + int (v)

    
    let haveToPlay (c:char) (otherAction:MorraCinese): MorraCinese =
        match c with
        | 'X' -> match otherAction with
                 | MorraCinese.Rock -> MorraCinese.Scissor
                 | MorraCinese.Paper -> MorraCinese.Rock
                 | MorraCinese.Scissor -> MorraCinese.Paper
                 | _ -> failwith "Azione non riconosciuta"
        | 'Y' -> otherAction
        | 'Z' -> match otherAction with    
                 | MorraCinese.Rock -> MorraCinese.Paper
                 | MorraCinese.Paper -> MorraCinese.Scissor
                 | MorraCinese.Scissor -> MorraCinese.Rock
                 | _ -> failwith "Azione non riconosciuta"
        | _ -> failwith "Azione non riconosciuta"


    /// Faccio il gioco 
    let game (content: string) : seq<int> =
        content.Split("\r\n")
        |> Seq.map (fun c -> c.Trim().Split(' '))
        |> Seq.map (fun p -> (p.[0].[0] |> charToAction, p.[0].[0] |> charToAction |> haveToPlay p.[1].[0] ))
        |> Seq.map (fun game ->
            let (other, mine) = game
            let result = play game
            makeScore mine result
        )
