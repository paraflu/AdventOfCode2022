module AnsiColor

type Colors =
    | Red = 1
    | Green = 2
    | Yellow = 3

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


#nowarn "9"
module DllImports =
    open System.Runtime.InteropServices
    open Microsoft.FSharp.NativeInterop

    let INVALID_HANDLE_VALUE = nativeint -1
    let STD_OUTPUT_HANDLE = -11
    let ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004

    [<DllImport("Kernel32")>]
    extern void* GetStdHandle(int nStdHandle)

    [<DllImport("Kernel32")>]
    extern bool GetConsoleMode(void* hConsoleHandle, int* lpMode)

    [<DllImport("Kernel32")>]
    extern bool SetConsoleMode(void* hConsoleHandle, int lpMode)

    let enableVTMode() =
        let handle = GetStdHandle(STD_OUTPUT_HANDLE)
        if handle <> INVALID_HANDLE_VALUE then
            let mode = NativePtr.stackalloc<int> 1
            if GetConsoleMode(handle, mode) then
                let value = NativePtr.read mode
                let value = value ||| ENABLE_VIRTUAL_TERMINAL_PROCESSING
                SetConsoleMode(handle, value)
            else
                printfn "no get"
                false
        else
            printfn "no handle"
            false


