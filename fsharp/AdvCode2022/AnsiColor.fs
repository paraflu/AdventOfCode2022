module AnsiColor

type Colors =
    | Red = 31
    | Green = 32
    | Yellow = 33

let private esc = string (char 0x1B)
let private csi = esc + "["
let private printsequencef f = Printf.kprintf (fun s -> System.Console.Write(esc + s)) f
let private printcsif f = Printf.kprintf (fun s -> System.Console.Write(csi + s)) f
let private selectGraphicRendition (gr: int list) =
    printcsif "%sm" (System.String.Join(";", gr |> Seq.map string))

let resetColor() = selectGraphicRendition [0]
let setForeground (i:Colors) = selectGraphicRendition([30 + int i])
let setBackground (i:Colors) = selectGraphicRendition([40 + int i])

let setExtendedForeground i = selectGraphicRendition [38; 5; i]
let setExtendedBackground i = selectGraphicRendition [48; 5; i]
let setForegroundRgb r g b = selectGraphicRendition [38; 2; r; g; b]
let setBackgroundRgb r g b = selectGraphicRendition [48; 2; r; g; b]


