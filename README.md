# Lightweight Email Service Wrapper

The Email Service Wrapper is a backend API that provides a unified interface to send emails using various email service providers. It allows developers to integrate their applications with popular email services such as SMTP, SendGrid, Amazon SES, SparkPost, and Postmark. This project serves as a wrapper around these services, providing a simplified and consistent way to send emails regardless of the underlying provider.

## Features

- Send emails using SMTP, SendGrid, Amazon SES, SparkPost, or Postmark.
- Register and manage SMTP email credentials for individual users.
- Single API endpoint to send emails, supporting multiple email providers.
- Swagger documentation for easy API exploration and testing.

## Technologies Used

- ASP.NET 7 (C#)
- SendGrid
- Amazon SES
- SparkPost
- Postmark
- Swagger

## Getting Started

### Prerequisites

- .NET 7 SDK
- SendGrid API key (for SendGrid service)
- Amazon SES credentials (for Amazon SES service)
- SparkPost API key (for SparkPost service)
- Postmark server token (for Postmark service)

### Installation

1. Clone the repository:
git clone https://github.com/samikhwaireh/MailingService.git
2. Navigate to the project directory:
cd MailingService
3. Build the project:
dotnet build
4. Configure the email service providers:
   - SMTP: Update the SMTP server host and port in the `appsettings.json` file.
   - SendGrid: Set the SendGrid API key in the `appsettings.json` file.
   - Amazon SES: Set the AWS access key and secret key in the `appsettings.json` file.
   - SparkPost: Set the SparkPost API key in the `appsettings.json` file.
   - Postmark: Set the Postmark server token in the `appsettings.json` file.
5. Run the application:
dotnet run


The API will be available at `http://localhost:5000`.

6. Access the Swagger documentation:

   Open your web browser and navigate to `http://localhost:5000/swagger`.

### Usage

1. Register an SMTP email:

   - Send a `POST` request to `/api/email/register` with the email credentials (email, password, host, port) in the request body.
   - The API will return a unique secret key that can be used to send emails from the registered email.

2. Send an email:

   - Send a `POST` request to `/api/email/send` with the email details (sender, recipients, subject, body) in the request body.
   - Include the secret key in the `X-Secret-Key` header.
   - The API will route the email to the appropriate email service based on the secret key.

Refer to the Swagger documentation for detailed API specifications and request/response examples.

## Contributing

Contributions are welcome! If you'd like to contribute to this project, please follow these steps:

1. Fork the repository.
2. Create your branch: `git checkout -b feature/my-feature`.
3. Commit your changes: `git commit -m 'Add some feature'`.
4. Push to the branch: `git push origin feature/my-feature`.
5. Submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact

If you have any questions or suggestions, please feel free to reach out
