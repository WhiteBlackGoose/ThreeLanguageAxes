#r "nuget: Plotly.NET, 2.0.0-preview.17"

open Plotly.NET

type Language = {
    name: string
    typing: float
    runtime: float
    paradigm: float
}

let langs = [
    { name = "C#"; typing = 0.7; runtime = 0.8; paradigm = 0.5 }
    
    { name = "C";  typing = 0.5; runtime = 0.9; paradigm = 0.1 }
    
    { name = "F#"; typing = 0.8; runtime = 0.9; paradigm = 0.8 }
    
    { name = "Python"; typing = 0.3; runtime = 0.2; paradigm = 0.4 }
    
    { name = "Asm"; typing = 0.0; runtime = 1.0; paradigm = 0.0 }
    
    { name = "F*"; typing = 1.0; runtime = 0.1; paradigm = 1.0 }
    
    { name = "Javascript"; typing = 0.2; runtime = 0.3; paradigm = 0.4 }
]

let points, labels =
    langs
    |> List.map (fun { name = n; typing = t; runtime = r; paradigm = p } -> (t, r, p), n)
    |> List.unzip

Chart.Point3D(
    points,
    MultiText = labels,
    TextPosition = StyleParam.TextPosition.BottomCenter
)
|> Chart.withXAxisStyle("Typing safety", Id=StyleParam.SubPlotId.Scene 1)
|> Chart.withYAxisStyle("How close to HW it is", Id=StyleParam.SubPlotId.Scene 1)
|> Chart.withZAxisStyle("Expressiveness/declarativeness")
|> Chart.withSize(1200., 900.)
|> GenericChart.toEmbeddedHTML
|> (fun c -> System.IO.File.WriteAllText("./index.html", c))
