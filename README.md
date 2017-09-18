# test-flask-sample
This repo contains a TestFlask sample service solution (MovieRentalService) that is weaved with [TestFlask](http://https://github.com/FatihSahin/test-flask) fody addin. 

All examples, docs and wikis will be based on that sample service solution. It is a fictional movie rental service that exposes information about movies and their stock state.

## How to build MovieRental for TestFlask 

*   Properly build solution in Debug mode
*   After building in debug mode, enable TestFlask addin from FodyWeavers.xml, and try to build sub-projects first. Lastly, build WCF service itself.

## What happens behind the scenes?

*   Open up your bin folder. Delete if any MovieRentalService.pdb file exists to make sure that decompilers should decompile your assembly from scratch. Decompile your MovieRentalService.dll with a decompiler tool like (dotPeek, justDecompile, ILSpy or ildasm etc.).
You should see your [Playback] operations weaved similar to the following structure.

*   A TestFlask ready service works as shown below

*   You can use [SoapUI](https://www.soapui.org/) project inside the SoapUI folder to trigger some operations on the service to record or play them on test flask.

## Sample service configuration

*   Sample service project shows a proper TestFlask ready WCF service configuration. Here are the most important configurations

    * Properly configure your hosted TestFlask API environment
        * To start a TestFlask API host, see test-flask project wiki 
    * Enable AspNetCompatibility Mode
    * Register TestFlaskHttpModule
    * Enable Cors support in Global.asax