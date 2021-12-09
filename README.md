# Thynk.CovidCenter
This is a simplistic COVID-19 PCR test management system that helps manage and process requests for PCR test

## Table Of Contents
- Architecture Diagram
- Assumptions made
- Goals
- [Design Decisions and Dependencies](#design-decisions-and-dependencies)
  * [The Core Project](#the-core-project)
  * [The Data Project](#the-infrastructure-project)
  * [The API Project](#the-web-project)
  * [The Test Projects](#the-test-projects)
- Patterns and Architectures Used
  * CQRS
  * Clean Architecture
  
## Architecture Diagram
![Blank diagram - Azure (2019) framework (3)](https://user-images.githubusercontent.com/13546416/145377898-106e550d-8b06-4c16-b977-05327631a79b.png)

## Code Structure
#Src
  - Thynk.CovidCenter.Api
  - Thynk.CovidCenter.Data
  - Thynk.CovidCenter.Core
  - Thynk.CovidCenter.Repository
  
#test
  - Thynk.CovidCenter.IntegrationTests
  - Thynk.CovidCenter.UnitTests

## Goals
- An administrator can allocate spaces for tests at specific locations and days
- An individual can book a PCR or Rapid Test at a specific day and location
- An individual can cancel a booked test, thus making available a slot
- A lab admin can set outcomes of test
- A lab admin can spool report
- Implement caching improve system response time and reduce database pulls

## Assumptions
- Only Administrators can allocate spaces for tests, if lad administrators or individuals try to, code will return an error
- Only Administrators and Lab administrators can spool reports
- Only Lab administrators can set test results
- Only individuals can book test date

## Improvements
- Implement logging with Serilog
- Implement unit testing
- Implement authentication and authorization
- Implement end to end encryption
- Implement load balancing on serverless computer

Link To FrontEnd
- https://quirky-bohr-fa5fca.netlify.app/dashboard

If the following error occurs please resolve with this 

"Page loaded over HTTPS but requested an insecure XMLHttpRequest endpoint"

https://stackoverflow.com/a/59857675

Link To BackEnd
- http://thynkcovidcenter-env.eba-y3vj8n6i.us-east-2.elasticbeanstalk.com
