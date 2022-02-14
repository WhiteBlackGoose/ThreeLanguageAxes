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

let closeToHw n = n

let declarative n = n

let langs = [
    { name = "C#";         typing = typesafe 0.7; runtime = closeToHw 0.7; paradigm = declarative 0.5 }
    { name = "C";          typing = typesafe 0.5; runtime = closeToHw 0.9; paradigm = declarative 0.1 }
    { name = "F#";         typing = typesafe 0.8; runtime = closeToHw 0.7; paradigm = declarative 0.8 }
    { name = "Python";     typing = typesafe 0.3; runtime = closeToHw 0.2; paradigm = declarative 0.4 }
    { name = "Asm";        typing = typesafe 0.0; runtime = closeToHw 1.0; paradigm = declarative 0.0 }
    { name = "F*";         typing = typesafe 1.0; runtime = closeToHw 0.1; paradigm = declarative 1.0 }
    { name = "Javascript"; typing = typesafe 0.2; runtime = closeToHw 0.3; paradigm = declarative 0.4 }
    { name = "Kotlin";     typing = typesafe 0.8; runtime = closeToHw 0.5; paradigm = declarative 0.6 }
    { name = "Java";       typing = typesafe 0.6; runtime = closeToHw 0.5; paradigm = declarative 0.4 }
    { name = "Haskell";    typing = typesafe 0.9; runtime = closeToHw 0.8; paradigm = declarative 0.9 }
    { name = "Fresh";      typing = typesafe 0.8; runtime = closeToHw 0.7; paradigm = declarative 0.7 }
]

let points, labels =
    langs
    |> List.map (fun { name = n; typing = t; runtime = r; paradigm = p } -> (t, r, p), n)
    |> List.unzip

let htmlChart =
    Chart.Point3D(
        points,
        MultiText = labels,
        TextPosition = StyleParam.TextPosition.BottomCenter
    )
    |> Chart.withXAxisStyle("Typing safety", Id=StyleParam.SubPlotId.Scene 1)
    |> Chart.withYAxisStyle("How close to HW it is", Id=StyleParam.SubPlotId.Scene 1)
    |> Chart.withZAxisStyle("Expressiveness/declarativeness")
    |> GenericChart.toEmbeddedHTML

let src = html [] [
    head [] [
        Text """
.main {
    width: 60%;
    margin-left: 20%;
}
.container {
    width: 100%;
}
        """
    ]
    body [] [
        div [_class "main"] [
            rawText htmlChart
        ]
    ]
]

File.WriteAllText("./index.html", src)
