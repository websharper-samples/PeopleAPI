namespace PeopleApi

open Microsoft.AspNetCore
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Http
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open WebSharper.AspNetCore

type Website(config: IConfiguration) =
    inherit SiteletService<App.Model.EndPoint>()

    let corsAllowedOrigins =
        config.GetSection("allowedOrigins").AsEnumerable()
        |> Seq.map (fun kv -> kv.Value)
        |> List.ofSeq

    override val Sitelet = App.Site.Main corsAllowedOrigins

type Startup() =

    member this.ConfigureServices(services: IServiceCollection) =
        services.AddSitelet<Website>()
        |> ignore

    member this.Configure(app: IApplicationBuilder, env: IHostingEnvironment) =
        if env.IsDevelopment() then app.UseDeveloperExceptionPage() |> ignore

        app.UseStaticFiles()
            .UseWebSharper()
            .Run(fun context ->
                context.Response.StatusCode <- 404
                context.Response.WriteAsync("Page not found"))

module Program =

    [<EntryPoint>]
    let main args =
        WebHost
            .CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .Build()
            .Run()
        0
