open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open WebSharper.AspNetCore
open PeopleApi

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)

    builder.Services.AddWebSharper()
        .AddAuthentication("WebSharper")
        .AddCookie("WebSharper", fun _ -> ())
    |> ignore

    let app = builder.Build()

    let corsAllowedOrigins =
        app.Configuration.GetSection("allowedOrigins").AsEnumerable()
        |> Seq.map (fun kv -> kv.Value)
        |> List.ofSeq

    if not (app.Environment.IsDevelopment()) then
        app.UseExceptionHandler("/Error").UseHsts() |> ignore

    app.UseHttpsRedirection()
       .UseAuthentication()
       .UseStaticFiles()
       .UseWebSharper(fun ws -> ws.Sitelet(App.Site.Main corsAllowedOrigins) |> ignore)
    |> ignore

    app.Run()

    0 // Exit code
