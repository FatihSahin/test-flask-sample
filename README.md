# test-flask-sample (MovieRental)

This repo contains a sample web app (MovieRental.Web) and a sample WCF service solution (MovieRental.Service)  that is weaved with [TestFlask](http://https://github.com/FatihSahin/test-flask) fody addin. 

TestFlask examples, docs and wikis will be based on that sample service solution as much as possible. It is a fictional movie rental service that exposes information about movies and their stock state. There is nothing special about the service operations. (AddMovie, DeleteMovie, RentMovie). To demonstrate TestFlask, service and web ui will be kept simple and dummy.

## How to build MovieRental for TestFlask 

* This sample solution uses TestFlask nuget packages, however I have not deployed them to nuget.org yet. In order to create TestFlask nuget packages, clone [test-flask](http://https://github.com/FatihSahin/test-flask) repo

* Build [test-flask](http://https://github.com/FatihSahin/test-flask) solution first, after that you need to prepare three nuget packages
    * You should create a folder in your machine to use it as nuget local repo. Configure your VS to use that local nuget repo as well.
    * Copy TestFlaskAddin.Fody package to your local nuget repo folder (This package will already be prepared inside NugetBuild folder when you properly build test-flask solution)
    * Open TestFlask.Aspects folder in command prompt and run "nuget pack". Copy TestFlask.Aspects.nupkg to your local nuget repo folder.
    * Open TestFlask.Assistant folder and again run "nuget pack". Copy TestFlask.Assistant package to your local nuget repo as well.

* After preparing packages, try to build MovieRental projects. An annoying dll locking occurs while weaving (hoping to solve that issue) and building solution, therefore try to build MovieRental projects in that order

    * MovieRental.Models
    * MovieRental.Business
    * MovieRental.Service
    * MovieRental.Web

## What happens behind the scenes?

*   If you hopefully managed to build MovieRental solution, there are some interesting things to observe. Open up your MovieRental.Service bin folder. Delete if any MovieRental.Service.pdb file exists to make sure that decompilers should decompile your assembly from scratch. Decompile your MovieRental.Service.dll with a decompiler tool like (dotPeek, justDecompile, ILSpy or ildasm etc.).
* You should see your methods that are marked with [Playback] attribute are now weaved similar to the following structure. Please observe that actual class files does not contain this kind of code. It is the real benefit and power of TestFlask that it can manipulate your any backend method and make it mockable with a simple [Playback] attribute.

*   A TestFlask ready service (before and after) diagram is shown below.

    ![alt text](Docs\withoutTestFlask.png "without TestFlask")
    ![alt text](Docs\withTestFlask.png "with TestFlask")

*   You can also use [SoapUI](https://www.soapui.org/) project inside the SoapUI folder to trigger some operations on the service to record or play them on TestFlask. 

## TestFlask.API Host and TestFlask Manager Web UI

* Properly configure your hosted TestFlask API environment
    * To start a TestFlask API host, see [test-flask](http://github.com/FatihSahin/test-flask) project 
* Instantiate your TestFlask Manager web app
    * To start a TestFlask Manager, see [test-flask-web](http://github.com/FatihSahin/test-flask-web) project
    * Define a project inside TestFlask Manager app with a proper project key.

    ![alt text](Docs\createProject.png "create TestFlask project")

## Sample service configuration

*   MovieRental.Service project shows a proper TestFlask ready WCF service configuration. Here are the most important configurations

    * Enable AspNetCompatibility Mode
    ```xml
    <system.serviceModel>
        <serviceHostingEnvironment aspNetCompatibilityEnabled="true" />
        ...
    </system.serviceModel>
    ```
    * Register TestFlaskHttpModule
    ```xml
    <system.webServer>
        <modules runAllManagedModulesForAllRequests="true">
            <add name="TestFlaskHttpModule" type="TestFlask.Aspects.Context.TestFlaskHttpModule, TestFlask.Aspects, PublicKeyToken=null, Version=1.0.0.0" />
        </modules>
        ...
    </system.webServer>
    ```
    * Enable Cors support in Global.asax (This is needed to support TestFlask Manager to be able to assert your scenarios)
    ```csharp
    protected void Application_BeginRequest(object sender, EventArgs e)
    {
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
        if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "*");

            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers",
                "Connection, Content-Type, SOAPAction, TestFlask-ScenarioNo, TestFlask-Mode, TestFlask-ProjectKey, TestFlask-StepNo, VsDebuggerCausalityData");
            HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
            HttpContext.Current.Response.End();
        }
    }
    ```

    * Finally change your testFlask config section with proper TestFlask.API host url and a project key
    ```xml
    <testFlask>
        <api url="http://localhost:12079" />
        <project key="MovieRental" />
    </testFlask>
    ```
## Sample web application configuration

*   MovieRental.Web shows a proper TestFlask ready MVC app configuration. Here are the most important configurations
    
    * Change your testFlaskAssistant config section with proper TestFlask.API host url, TestFlask Manager Url and your project key
    
    ```xml
    <testFlaskAssistant enabled="true">
        <api url="http://localhost:12079" />
        <manager url="http://localhost:4200" />
        <project key="MovieRental" />
    </testFlaskAssistant>
    ```

    * Configure your backend service binding with TestFlask assistant endpoint behavior

    ```xml
    <system.serviceModel>
        <extensions>
            <behaviorExtensions>
                <add name="testFlaskAssistantEndpointBehavior" type="TestFlask.Assistant.WcfExtensions.WcfEndpointBehaviorExtensionElement, TestFlask.Assistant, PublicKeyToken=null, Version=1.0.0.0" />
            </behaviorExtensions>
        </extensions>
        <behaviors>
            <endpointBehaviors>
                <behavior name="withTestFlaskAssistantEndpointBehavior">
                <testFlaskAssistantEndpointBehavior />
                </behavior>
            </endpointBehaviors>
        </behaviors>
        <bindings>
            <basicHttpBinding>
                <binding name="BasicHttpBinding_IMovieRentalService" />
            </basicHttpBinding>
        </bindings>
        <client>
            <endpoint address="http://localhost:31728/MovieRentalService.svc" behaviorConfiguration="withTestFlaskAssistantEndpointBehavior" binding="basicHttpBinding" bindingConfiguration="BasicHttpBinding_IMovieRentalService" contract="MovieRentalService.IMovieRentalService" name="BasicHttpBinding_IMovieRentalService" />
        </client>
    </system.serviceModel>
    ```

    ## Playing with your TestFlask ready web app and service

    * Open up the TestFlask assistant in your web page
        * Create a new scenario
        * Set record mode on
        * Trigger some operations (AddMovie, DeleteMovie, RentMovie)
            * You should be able to see auto generated steps inside assistant. Rename them if you like.
        * Set record mode off
        * Examine your scenario and steps and all invocation tree using TestFlask Manager

    * Here are some screenshots. (I promise I will remove them right after I prepare a YouTube video)

        * Using TestFlask Assistant inside your ASP.NET MVC App

            ![alt text](Docs\assistant_1.png)

        * Observing and editing scenario details and steps

            ![alt text](Docs\manager_1.png)

        * Diving deep into recorded step with call tree and see request, response objects

            ![alt text](Docs\manager_2.png)

        * Preparing an assertion for asserting steps.

            ![alt text](Docs\manager_3.png)

        * You can also see recorded raw requests which triggered that step.

             ![alt text](Docs\manager_4.png)

        * Using TestFlask Assistant, you can assert the whole scenario (run all step assertions)

            ![alt text](Docs\manager_5.png)