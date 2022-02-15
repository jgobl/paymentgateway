# Running the solution

The idea was I would have Dockerfiles for the postgresql database, the payment gateway api and the simulator api and you could run everything with docker compose but I ran out of time to get that done. The database is part of docker compose so it can be started by running the following command from the main directory.

`docker-compose up`

It will create a payment table when it starts the database.

To run the Payment gateway api go the PaymentGateway.Api directory and run the following command

`dotnet run`

That will build and run the api on port `5000`. Swagger  documentation for the Api is available on http://localhost:5000/swagger/index.html.

To run the simulator api go the AcquiringBankSimulator.Api directory and run the following command

`dotnet run`

That will build and run the api on port `5001`. The simulator is set up as follows
- cardnumber 111111111111111 will return http status 200
- cardnumber 500111111111111 will return http status 500
- any other number will return http status 422 with an error in the response body

There is a postman collection `paymentgatewaycollection.postman_collection.json` that can be used to make requests against the payment gateway.

There are unit tests in `PaymentGateway.UnitTests` directory. Run the following command from the directory to run the tests

`dotnet test`

There are integration tests which currently cover the create payment scenario. Go to `PaymentGateway.Integrationtests` directory and run the
following command to run the tests.

`dotnet test`


# Assumptions

From what I understand from a PCI perspective it is best not to store credit card information if possible so I have just stored the masked card number to satisfy the requirements given.

# Areas for improvement

- Add some more unit test coverage...I ran out of time to cover everything
- Add integration tests to cover the get payment requirement. Tidy up the tests also.
- Add Dockerfiles for paymentgateway.api and the simulator api so you can run everything with docker compose. The Dockerfile for paymentgateway.api could run the unit tests also.
- Generate coverage report for unit tests
- Add a retry and circuit breaker policy to the acquiringbank client.
- For production services observability should be built in so that would be needed. There are off the shelf integrations such as AppDynamics, DataDog etc or you can push metrics to a StatsD server or use Prometheus to pull metrics.

# Cloud technologies you would use

- For the Api I would used some container orchestrator such as AWS ECS or AWS EKS as they work with Docker images, give you a lot out of the box and are tried and tested.
- For the basic requirement for the database we have a Postgresql Database hosted in AWS RDS would be sufficient.

# Bonus Points

- Added swagger api documentation
- Added unit and integration tests
- On the way to having docker compose to run everything