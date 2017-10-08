namespace MyFsharpApp
module AzureFunction =
    open Microsoft.Azure.WebJobs
    open Microsoft.Azure.WebJobs.Host

    let Run (myTimer: TimerInfo) (log: TraceWriter) =
        sprintf "My F# timer is running in a dotnet standard 2.0 Azure Function!!!"
        |> log.Info