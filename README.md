# Football Information System

Handles teams in multiple competitions and their games and presents ranking tables

Clone and start in visual studio

Send requests e.g. by Post to http://localhost:64179/

e.g Get http://localhost:64179/api/Rankings
Get http://localhost:64179/api/Teams

Add Teams by POST http://localhost:64179/api/Teams, typical body:
{
        "Name": "Loko",
        "City": {
        	"Name": "Sofia"
        },
        "Country": {
        	"Name": "Bulgaria"
        },
        "Competitions": [{ "Name": "BFF" }]
    }

Add Games by POST http://localhost:64179/api/Games
    typical body: 
{
    "Competition": {
        "Id": 1,
        "Name": "BFF"
    },
        "HomeTeam": {
            "Id": 4,
        	"Name": "Levski"
        },
        "AwayTeam": {
            "Id": 5,
        	"Name": "CSKA"
        },
        "HomeTeamGoals": 3,
        "AwayTeamGoals": 2,
        "Date": "2020-01-01T00:00:00",
        "GameFinished": false
    }

