open System.IO

let day1_result =
    File.ReadAllText "../../../day1.realdata.txt"
    |> Day1.Main.caloriesList
    |> Seq.max

printf "Day1 : %d" day1_result

let day2_result =
    File.ReadAllText "../../../day2.realdata.txt"
    |> Day2.Main.game
    |> Seq.sum

printf "Day2 : %d" day2_result

printf "Premere invio per terminare"
System.Console.ReadLine |> ignore
