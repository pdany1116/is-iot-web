# is-iot-web

This repository represents a ASP.NET web application that interactis with an IoT Irrigation System, mainly communicates with the sink module via MQTT and makes CRUD operations on a MongoDB database.

Users are allowed:
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

## Linux Setup
1. `git clone https://github.com/pdany1116/is-iot-sink.git`
1. `cp appsettings.example.json appsettings.json`
2. `docker compose up`

The app is running at: http://localhost:5000
