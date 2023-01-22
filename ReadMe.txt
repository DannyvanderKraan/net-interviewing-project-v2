Task 1 [BugFix]:
Before fixing the bug I added unit tests which tests all the flows in the CalculateInsurance method of the HomeController and 
added an unit test which tests calculating insurance given a laptop that costs less than 500 euros should calculate 500 euros 
insurance. I wrote these tests via the provided specs in the documentation: "Functionality already implemented".
I added the test products by looking at the API's swagger UI and list the products and product types (to get some realistic data).
With the unit test in place I felt confident to fix the bug in the CalculateInsurance method of the HomeController. 
As I already saw a refactor task as the second task I fixed the bug as quickly as possible for now by disturbing the existing code 
as little as possible by adding another if statement basically. Who knows, maybe this code was already in production and needed a hot fix! ;)
p.s.: I'm from the "Art of Unit Testing" school so the "Given... Should..." feels like home to me and I also added Arrange, Act, Assert 
because I like that guidance while making unit tests.

Task 2 [Refactoring]
Because I've already added the unittests I needed to fix the bug with confidence, I was already sure I wouldn't break existing functionality 
if I would refactor the CalculateInsurance method of the HomeController which houses the implemented functionality.
It violates several SOLID principles to be honest and would be a nightmare for your collegea's to expand or maintain (or fix bugs in).

	Steps taken:
	- I got rid of as much magic strings and numbers as I could by replacing them with constants and other methods, see for instance 
	the constants in ControllerTestStartup (let us not make Uncle Bob write another book please)
	- Gave everything clearer names, for instance UnitTest1 is not a clear name while CalculateInsuranceTests is.
	I kept the InsuranceDto which is passed along in the HomeController the same, altough I don't think it's clear for and ProductDto would be 
	clearer. But i kept is the same because else I'd also have to version the API to stay backwards compatible, and this assesment case is big enough... ;)
	- Moved classes to their own file and under a clear folder so stuff is not hidden and more clearer to see, like 
	ControllerTestFixture under the Fixtures folder 
	- First thing I really refactored was the BusinessRules class. Especially the unnecessary call to product_types to get a collection
	while you can just retrieve type by id. 
	- I proceeded by abstracting the logic that was happening in the BusinessRules class to an 
	InsuranceCalculator service which has dependencies injected to a ProductRepository and ProductTypeRepository with their own Data Transfer Objects. 
	This has been done to abstract away the implementation details of where the data was coming from, because it shouldn't matter if it's 
	from a database or another API. 
	For example, it abstracts away that you need two seperate API calls; one to get a product; one to get product type.
	It also serves as an Anti Corruption Layer to make sure implementation details of the API in this case 
	don't bleed into the rest of your code and thus adhering to the Open/Closed principle. Dependency Injection was added for Inversion 
	of Control to be able to have the benefits this provides, such as better maintenance and easier unit-testing.
	- To continue about the magic strings, I moved the Laptops and Smartphones magic strings to IProductTypeRepository, so that they wouldn't 
	bleed out to the rest of the code. But I wouldn't provide the product types via an API like this, but I would make it an enum 
	in a shared NuGet package. Because you've got logic dependant on these product type names anyhow you might as wel make it explicit.
	- Refactored the Product API url const in HomeController to be injected where needed via de IOptions model
	- Renamed HomeController to InsuranceController and removed "api/insurance" from the routes and added a route attribute to the class 
	to improve the maintenance of the routes
	- Added XML tags to make my collegea's really happy with me (everything for a smile)

Task 3 [Feature 1]
For this feature it seemed fine to provide a list of product Ids to the InsuranceCalculator and call the existing method to caculate the insurance value 
per product id. I already decided to keep the provided approach in HomeController the same, so I've made OrderDto to match the style.
So my assumption is that the concerning product Ids are provided to this API via an order DTO.
I've added one unit test to test the working of this new method, because 100% coverage is not a goal in itself. 
Reading task 4 feature 2 I started to assume an order has a total insurance value, so I implemented this.

Task 4 [Feature 2]
I assumed because in the Product API digital cameras can be insured and it's not mentioned in the description of the feature that 
I don't have to check additionaly if the product type can be insured or not. Other than that it was pretty straight forward to implement after my 
refactor work.

Task 5 [Feature 3]
I assumed the surcharge is added even if the producttype can't be insured. I assumed if the Sales Price is below 0 it's not added. I had no time to ask anymore via Slack. 
For now it seems like SurchargeService is overkill. But when anything changes in the logic concerning adding and retrieving surcharges 
or it needs additional logic this will make sure it'll be done in 1 place, the service.
As the documentation explicitly warns: "Please be aware that this endpoint is going to be used simultaneously by multiple users." I opted for 
Azure Storage Table, because it is made by Microsoft to handle huge loads. It is also very easy with Azure Storage Table to implement the "optimistic concurrency" 
strategy and have the Azure Storage service check the etags, but in this case I assumed the "Last writer wins" strategy is good enough. I implemented 
this strategy by using UpsertEntity method which does a merge. 


Things to consider:
- Make the endpoints "v1" (version 1) for backwards compatability when version X endpoints are implemented for new features
- Add Swagger
- Seems this API is going to be called from a front end, so then it's better to use async await (for now KISS)
- The test AddSurcharge_GivenProductTypeIdAnd500Euros_ShouldAdd500EurosSurchargeToProvidedProductTypeId should be flanked by 
integrationtests to test if the data is correctly stored in the Storage Table.
- Depending on how many users simultaneously are going to use the endpoints you could also consider deploying the web API multiple times 
(horizontal scaling) and have a load balancer infront of them to balance the load and prevent Http connection exhaustion and nasty 
things like that. 
- As far as the logging concerned I implemented ILogger pattern and added Application Insight so you can monitor pro-actively on Azure. 
- as far as the "self healing" and "resilient" I'd like to note a couple of things:
  - I always try to make all components of a system stateless and idempotent in this regard; you can see this by using UpsertEntity for example
  - Furthermore when it's background processes I'm used to event-driven systems that throw the messages after a couple of retries back on a poison queue 
  (the system keeps running and you just need to monitor these poison queues); but as for API's concerned they are usually called from a front-end or another 
  app and you need some imediate feedback that something happened (a Bad Request for example)

P.S.: I've left launchSettings.json for now even though weatherforecast doesn't exist

aant:
- Check de "What we expect in your solution"
