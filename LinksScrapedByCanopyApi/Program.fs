namespace LinksScrapedByCanopyApi

open System
open System.IO
open System.Data

open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.Hosting

open System.Threading.Tasks
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection

open Saturn
open Giraffe

open Helpers
open RestApiThothJson.ThothJson

module Program =

    [<EntryPoint>]
    let main args =

        let apiKey = "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"  

        let validateApiKey (next: HttpFunc) (ctx: HttpContext) =  //GIRAFFE
                     
            match ctx.Request.Headers.TryGetValue("X-API-KEY") with
            | true, key 
                when string key = apiKey 
                    -> 
                     next ctx  
            | _     ->
                     ctx.Response.StatusCode <- 401
                     ctx.Response.WriteAsync("Unauthorized: Invalid API Key") |> ignore
                     System.Threading.Tasks.Task.FromResult<HttpContext option>(None) // API key is missing or invalid

        let apiRouter = //SATURN

            router
                { 
                    pipe_through validateApiKey //...for every request
                    get "/" getHandler   //anebo /user atd.
                    put "/" putHandler //anebo /user atd.              
                }

        let app =  //SATURN

            application
                {
                    use_router apiRouter
                    url "http://natalie.somee.com/api"
                    memory_cache
                    use_static "static"
                    use_gzip
                }

        run app //SATURN

        0

(*
<Project Sdk="Microsoft.NET.Sdk.Web">

	<PropertyGroup>
		<TargetFramework>net8.0</TargetFramework>
		<AspNetCoreHostingModel>InProcess</AspNetCoreHostingModel>
		<!-- Hosting model for ASP.NET Core -->
		<OutputType>Exe</OutputType>
		<!-- Specify that it's an executable -->
	</PropertyGroup>

	<ItemGroup>
		<Compile Include="CEBuilders.fs" />
		<Compile Include="ErrorHandlers.fs" />
		<Compile Include="Helpers.fs" />
		<Compile Include="ThothCoders.fs" />
		<Compile Include="UsingThothJson.fs" />
		<Compile Include="Program.fs" />
	</ItemGroup>

	<ItemGroup>
		<PackageReference Include="Giraffe" Version="7.0.2" />
		<PackageReference Include="Saturn" Version="0.17.0" />
		<PackageReference Include="Thoth.Json.Net" Version="12.0.0" />
	</ItemGroup>

</Project>
*)
