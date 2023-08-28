using Utilities.Models;
using Utilities;

Database db = new Database();

PingModel ping = db.GetLatestPing();

