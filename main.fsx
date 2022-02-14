#r "nuget: Plotly.NET, 2.0.0-preview.17"
#r "nuget: Giraffe.ViewEngine, 2.0.0-alpha-1"

open Plotly.NET
open Giraffe.ViewEngine

type Language = {
    name: string
    typing: float
    runtime: float
    paradigm: float
}

let typesafe n = n

let closeToHw n = 1.0 - n

let declarative n = n

let langs = [
    { name = "C#";         typing = typesafe 0.70; runtime = closeToHw 0.70; paradigm = declarative 0.50 }
    { name = "C";          typing = typesafe 0.50; runtime = closeToHw 0.90; paradigm = declarative 0.10 }
    { name = "C++";        typing = typesafe 0.55; runtime = closeToHw 0.90; paradigm = declarative 0.15 }
    { name = "F#";         typing = typesafe 0.80; runtime = closeToHw 0.70; paradigm = declarative 0.80 }
    { name = "Python";     typing = typesafe 0.30; runtime = closeToHw 0.20; paradigm = declarative 0.40 }
    { name = "Asm";        typing = typesafe 0.00; runtime = closeToHw 1.00; paradigm = declarative 0.05 }
    { name = "F*";         typing = typesafe 1.00; runtime = closeToHw 0.10; paradigm = declarative 1.00 }
    { name = "Javascript"; typing = typesafe 0.20; runtime = closeToHw 0.30; paradigm = declarative 0.40 }
    { name = "Kotlin";     typing = typesafe 0.80; runtime = closeToHw 0.50; paradigm = declarative 0.60 }
    { name = "Java";       typing = typesafe 0.60; runtime = closeToHw 0.50; paradigm = declarative 0.40 }
    { name = "Haskell";    typing = typesafe 0.90; runtime = closeToHw 0.80; paradigm = declarative 0.90 }
    { name = "Fresh";      typing = typesafe 0.75; runtime = closeToHw 0.70; paradigm = declarative 0.70 }
    { name = "Brainfuck";  typing = typesafe 0.00; runtime = closeToHw 0.95; paradigm = declarative 0.00 }
]

let src = html [] [
    head [] [
        style [] [
            Text """
.container {
    // width: 100%;
}
"""
        ]
    ]
    body [] [
        div [_class "main"] [
            let graphInfos = [
                (fun { typing = t; runtime = r } -> t, r), "Type safety", "Abstraction from machine code"
                (fun { typing = t; paradigm = p } -> t, p), "Type safety", "Expressiveness"
                (fun { runtime = r; paradigm = p } -> r, p), "Abstraction from machine code", "Expressiveness"
            ]
            for (f, xName, yName) in graphInfos do
                div [] [
                    let x, y = langs |> List.map f |> List.unzip
                    Chart.Point(x, y, MultiText=List.map (fun l -> l.name) langs, TextPosition = StyleParam.TextPosition.BottomCenter)
                    |> Chart.withXAxisStyle(xName)
                    |> Chart.withYAxisStyle(yName)
                    |> Chart.withSize(900., 600.)
                    |> GenericChart.toChartHTML
                    |> rawText
                ]
        ]
    ]
]

let srcText = RenderView.AsString.htmlNode src

System.IO.File.WriteAllText("./index.html", srcText)
