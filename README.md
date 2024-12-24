# RabbitMQ Message Sender Application

This application is a simple WPF tool designed to connect to a RabbitMQ message broker and send messages to a specified queue. It provides an intuitive graphical interface for configuration and testing.

---

## Features
- Connects to RabbitMQ using user-defined settings:
  - Hostname
  - Username
  - Password
  - Virtual Host
- Validates input fields to ensure no configuration is missing.
- Tests the connection to RabbitMQ and displays feedback with status color:
  - **Green Text**: Successful connection.
  - **Red Text**: Connection failed.
- Sends messages to a designated queue.

---

## How It Works

1. **Configuration**:  
   The user provides RabbitMQ connection details.
2. **Connection Test**:  
   - The "Test" button verifies the connection to RabbitMQ.
   - Displays success or error status.
3. **Message Sending**:  
   - Once connected, messages can be sent to a specified queue.

---

## Technologies Used
- **RabbitMQ**: Message broker.
- **WPF**: Graphical user interface.
- **C#**: Application logic and RabbitMQ communication.

---

## Usage Steps
1. Open the application.
2. Enter RabbitMQ connection details:
   - Hostname
   - Username
   - Password
   - Virtual Host
3. Click the "Test" button to verify the connection.
4. If the connection is successful, input the message and send it to the queue.

---

## Dependencies
- [RabbitMQ.Client](https://www.nuget.org/packages/RabbitMQ.Client) NuGet package for .NET.
- A running instance of RabbitMQ broker.

---

## Future Enhancements
- Add a message viewer to see received messages in real-time.
- Support for configuring exchanges and routing keys.
- Implement a logging system to track sent messages and errors.

---

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
