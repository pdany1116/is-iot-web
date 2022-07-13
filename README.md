# is-iot-web

This repository represents a ASP.NET web application that interactis with an IoT Irrigation System, mainly communicates with the sink module via MQTT and makes CRUD operations on a MongoDB database.

Users are allowed to:
1. [x] To authenticate with an existing account.
2. [x] To add/edit/delete users.
3. [x] To add roles.
4. [x] To manually control (turn on/off) the irrigation valves.
5. [x] To visualize real time state of the valves.
6. [x] To add irrigation plans.
7. [ ] To edit/delete irrigation plans.
8. [x] To visualize collector data in tables and charts.
9. [x] To visualize automated irrigation times in tables and charts.
10. [x] To visualize valve action logs in tables.
11. [x] To visualize users and roles in tables.
12. [ ] To configure irrigation mode (manual/automated).
13. [ ] To configure automated irrigation process.
14. [ ] To configure the sink module.

### System requirements
1. Windows 10.
2. Visual Studio 2022 Community with C#.
2. .NET 5 SDK.
3. ASP.NET and web development workload (can be installed from Visual Studio Installer).

### Clone repository
```
git clone https://github.com/pdany1116/is-iot-sink.git
```
or download as ZIP and extract.

### Configure application settings
#### Change default values
```
cp appsettings.example.json appsetings.json
```
`Note: Replace MongoDB, MQTT and AccuWeather settings with your values. All variables need to be defined!!!`

### Open solution with Visual Studio 2022.
### Clean and build solution.
### Restore nuget packages.
### Deploy with IIS Express.
