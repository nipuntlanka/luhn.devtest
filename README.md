
# **Luhn.DevTest**

## **Overview**
`Luhn.DevTest` is a production-grade ASP.NET Core Web API that validates credit card numbers using the **Luhn algorithm**. This project adheres to professional software development practices, including modular architecture, robust error handling, and comprehensive testing.

---

## **Features**
- **Luhn Algorithm Validation**: Validates credit card numbers for compliance with the Luhn checksum formula.
- **RESTful API**: A single endpoint with HTTP GET and POST support for card number validation.
- **Robust Error Handling**: Global exception handling for unexpected errors, input validation, and standardized error responses.
- **Modular Architecture**: A clean, maintainable solution structure with Core, Service, Repository, and API layers.
- **Swagger/OpenAPI Integration**: API documentation for easy testing and understanding of the endpoints.
- **Comprehensive Testing**: Unit tests for the Luhn validator and integration tests for the API endpoint.
- **Logging**: Integrated with Serilog for error and activity logging.

---

## **Project Structure**
```
Luhn.DevTest/
├── Luhn.DevTest.sln               # Solution file
├── Luhn.DevTest.Api               # API project
│   ├── Controllers                # REST API Controllers
│   ├── Middleware                 # Global Exception Handling
│   └── Program.cs                 # Main entry point
├── Luhn.DevTest.Core              # Core library
│   └── Utilities                  # Luhn Validation logic
├── Luhn.DevTest.Repository        # Data access layer
├── Luhn.DevTest.Service           # Business logic layer
├── Luhn.DevTest.Test              # Unit and Integration Tests
└── README.md                      # Documentation
```

---

## **Getting Started**

### **Prerequisites**
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Postman](https://www.postman.com/) or `curl` for testing the API.
- Optional: A code editor like [Visual Studio Code](https://code.visualstudio.com/)

### **Installation**

1. Clone the repository:
   ```bash
   git clone https://github.com/nipuntlanka/luhn.devtest.git
   cd Luhn.DevTest
   ```

2. Build the solution:
   ```bash
   dotnet build
   ```

3. Run the tests:
   ```bash
   dotnet test
   ```

4. Run the API:
   ```bash
   cd Luhn.DevTest.Api
   dotnet run
   ```

---

## **API Documentation**

### **Endpoint Overview**
#### Base URL
`http://localhost:7090/api/CreditCard`

#### **GET /validate**
Validate a credit card number using the Luhn algorithm.

- **Query Parameters**:
  - `cardNumber` (string, required): The credit card number to validate.
- **Response**:
  ```json
  {
    "cardNumber": "4539578763621486",
    "isValid": true
  }
  ```
- **Errors**:
  ```json
  {
    "statusCode": 400,
    "message": "Card number is required."
  }
  ```

#### Example `curl` Command:
```bash
curl -X GET "http://localhost:7090/api/CreditCard/validate?cardNumber=4539578763621486"
```

---

## **Implementation Details**

### **Luhn Algorithm**
The Luhn algorithm is a simple checksum formula used to validate a variety of identification numbers, such as credit card numbers. The algorithm ensures that accidental errors in the input (e.g., mistyped digits) are detected.

The algorithm is implemented in the `LuhnValidator` class within the `Luhn.DevTest.Core.Utils` namespace.

```csharp
public static bool ValidateCreditCard(string cardNumber)
{
    int sum = 0;
    bool alternate = false;

    for (int i = cardNumber.Length - 1; i >= 0; i--)
    {
        if (!char.IsDigit(cardNumber[i]))
            return false;

        int digit = cardNumber[i] - '0';

        if (alternate)
        {
            digit *= 2;
            if (digit > 9) digit -= 9;
        }

        sum += digit;
        alternate = !alternate;
    }

    return sum % 10 == 0;
}
```

### **Error Handling**
Robust error handling is implemented via:
1. **Global Exception Middleware**: Catches unhandled exceptions and returns a generic error response.
2. **Model Validation**: Ensures all inputs are valid and meaningful.
3. **Standardized Error Responses**: Provides consistent error structures for clients.

---

## **Testing**

### **Unit Tests**
The `LuhnValidatorTests` in the `Luhn.DevTest.Test` project validate the Luhn algorithm with various test cases, including edge cases and invalid inputs.

#### Example Unit Test
```csharp
[Theory]
[InlineData("4539578763621486", true)]
[InlineData("6011000990139424", true)]
[InlineData("1234567890123456", false)]
[InlineData("4111111111111111", true)]
public void ValidateCreditCard_ShouldValidateCorrectly(string cardNumber, bool expected)
{
    bool result = LuhnValidator.ValidateCreditCard(cardNumber);
    Assert.Equal(expected, result);
}
```

## **Logging**
- **Serilog** is used to log all unhandled exceptions and critical application events to a file for debugging purposes.
- Logs are located in the `logs` directory when running in a production environment.

---

## **Deployment**

### **Docker**
1. Use the`Dockerfile` in the `Luhn.DevTest.Api` project:
   ```dockerfile
   FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
   WORKDIR /app
   EXPOSE 80

   FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
   WORKDIR /
   COPY ["Luhn.DevTest.Api/Luhn.DevTest.Api.csproj", "Luhn.DevTest.Api/"]
   RUN dotnet restore "Luhn.DevTest.Api/Luhn.DevTest.Api.csproj"
   COPY . .
   WORKDIR "/Luhn.DevTest.Api"
   RUN dotnet build -c Release -o /app/build

   FROM build AS publish
   RUN dotnet publish -c Release -o /app/publish

   FROM base AS final
   WORKDIR /app
   COPY --from=publish /app/publish .
   ENTRYPOINT ["dotnet", "Luhn.DevTest.Api.dll"]
   ```

2. Build and run the Docker container:
   ```bash
   docker build -t luhn-devtest .
   docker run -p 7090:80 luhn-devtest
   ```

3. Access the API at `http://localhost:7090/api/CreditCard/validate`.

---

## **Design Principles**
- **Separation of Concerns**: Each project handles a distinct aspect of the application.
- **Test-Driven Development**: Unit tests validate all critical business logic.
- **Scalability**: Modular architecture makes it easy to extend functionality.
- **Error Transparency**: Clients receive helpful error messages without exposing sensitive details.

---

## **Future Enhancements**
- Add support for batch credit card validations.
- Integrate with a database for logging validation history.
- Add authentication and rate-limiting middleware.
- Deploy using CI/CD pipelines.

---

## **Contributing**
1. Fork the repository.
2. Create a feature branch.
3. Submit a pull request with detailed changes.

---

## **License**
If needed in future. 
