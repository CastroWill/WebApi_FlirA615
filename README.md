# WebApi_FlirA615

## 📌 Project Description
**WebApi_FlirA615** is an API developed in **.NET Framework 4.8** that enables communication and control of a **FLIR A615** thermal camera. The API provides RESTful endpoints to connect, disconnect, capture images, and manage thermal camera settings, facilitating integration with external systems.

## ⚙️ Technologies Used
- **C#**
- **ASP.NET Web API**
- **OWIN**
- **Swashbuckle (Swagger)**
- **FLIR Atlas SDK**

## 📂 Project Structure
```
/WebApi_FlirA615
├── Controllers
│   └── CameraController.cs  # Main API controller
├── Services
│   └── CameraService.cs  # Camera control logic
├── Http
│   └── WebApiConfig.cs  # Web API and Swagger configuration
├── Startup.cs  # OWIN and Web API initialization
├── Program.cs  # Application entry point
└── README.md  # Project documentation
```

## 🚀 How to Run the Project
### 🔧 Prerequisites
Before running the API, make sure you have installed:
- **.NET Framework 4.8**
- **FLIR Atlas SDK**
- **Spinnarker SDK**
- **eBUS SDK**

### 🛠️ Installation
1. **Clone the repository**
   ```bash
   git clone https://github.com/CastroWill/WebApi_FlirA615.git
   cd WebApi_FlirA615
   ```
2. **Install dependencies**
   ```bash
   dotnet restore
   ```
3. **Build the project**
   ```bash
   dotnet build
   ```
4. **Run the application**
   ```bash
   dotnet run
   ```

## 📡 API Endpoints
### 🔗 **Base URL:** `http://localhost:5000/api/camera/`

| Method | Endpoint       | Description |
|--------|--------------|-------------|
| POST   | `/connect`   | Connects to the thermal camera (requires IP in the request body) |
| POST   | `/disconnect` | Disconnects the thermal camera |
| POST   | `/capture`    | Captures a thermal image and saves it to the specified directory |
| POST   | `/autoFocus`  | Adjusts the camera's autofocus |
| GET    | `/status`     | Returns the current status of the camera |

### **Example Request to Connect the Camera**
```bash
curl -X POST http://localhost:5000/api/camera/connect \
     -H "Content-Type: application/json" \
     -d "\"169.254.36.128\""
```

### **Example Request to Capture an Image**
```bash
curl -X POST http://localhost:5000/api/camera/capture \
     -H "Content-Type: application/json" \
     -d "\"C:\\images\\thermal.jpg\""
```

## 📝 Documentation with Swagger
The API integrates with **Swagger** for interactive documentation. After starting the application, access:
```
http://localhost:5000/swagger
```

## 🤝 Contribution
1. **Fork this repository**
2. **Create a branch:** `git checkout -b my-feature`
3. **Make your changes and commit:** `git commit -m 'Adding new functionality'`
4. **Push to the remote repository:** `git push origin my-feature`
5. **Create a Pull Request**

---

💡 **Questions or suggestions?** Feel free to reach out! 😊


