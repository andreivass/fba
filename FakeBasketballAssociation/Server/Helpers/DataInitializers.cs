using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using FakeBasketballAssociation.Server.Data;
using FakeBasketballAssociation.Shared.Entities;
using Microsoft.AspNetCore.Identity;

namespace FakeBasketballAssociation.Server.Helpers
{
    public class DataInitializers
    {
        public static void SeedData(ApplicationDbContext context)
        {
            var samplePlayers = new Player[]
            {
                new Player()
                {
                    NbaId = "203507",
                    PlayerVotes = 0
                },
                new Player()
                {
                    NbaId = "2544",
                    PlayerVotes = 0
                },
                new Player()
                {
                    NbaId = "201935",
                    PlayerVotes = 0
                },
                new Player()
                {
                    NbaId = "1627936",
                    PlayerVotes = 0
                },
                new Player()
                {
                    NbaId = "203076",
                    PlayerVotes = 0
                }
            };

            foreach (var player in samplePlayers)
            {
                if (!context.Players.Any(p => p.NbaId == player.NbaId))
                {
                    context.Players.Add(player);
                }
            }
            context.SaveChanges();

            // random guid for sample votes
            var sampleVotes = new Vote[]
            {
                new Vote
                {
                    ApplicationUserId = new Guid("ace8d4da-aa66-4000-a508-59ceda17573b"),
                    PlayerId = 1
                },
                new Vote
                {
                    ApplicationUserId = new Guid("ace8d4da-aa66-4000-a508-59ceda17573b"),
                    PlayerId = 1
                },
                new Vote
                {
                    ApplicationUserId = new Guid("ace8d4da-aa66-4000-a508-59ceda17573b"),
                    PlayerId = 1
                },
                new Vote
                {
                    ApplicationUserId = new Guid("ace8d4da-aa66-4000-a508-59ceda17573b"),
                    PlayerId = 1
                },
                new Vote
                {
                    ApplicationUserId = new Guid("ace8d4da-aa66-4000-a508-59ceda17573b"),
                    PlayerId = 2
                },
                new Vote
                {
                    ApplicationUserId = new Guid("ace8d4da-aa66-4000-a508-59ceda17573b"),
                    PlayerId = 2
                },
                new Vote
                {
                    ApplicationUserId = new Guid("ace8d4da-aa66-4000-a508-59ceda17573b"),
                    PlayerId = 2
                },
                new Vote
                {
                    ApplicationUserId = new Guid("ace8d4da-aa66-4000-a508-59ceda17573b"),
                    PlayerId = 3
                },
                new Vote
                {
                    ApplicationUserId = new Guid("ace8d4da-aa66-4000-a508-59ceda17573b"),
                    PlayerId = 4
                },
                new Vote
                {
                    ApplicationUserId = new Guid("ace8d4da-aa66-4000-a508-59ceda17573b"),
                    PlayerId = 4
                },
                new Vote
                {
                    ApplicationUserId = new Guid("ace8d4da-aa66-4000-a508-59ceda17573b"),
                    PlayerId = 2
                }
            };

            foreach (var vote in sampleVotes)
            {
                if (!context.Votes.Any(p => p.ApplicationUserId == new Guid("ace8d4da-aa66-4000-a508-59ceda17573b")))
                {
                    context.Votes.Add(vote);
                }
            }
            context.SaveChanges();
        }

    }
}
