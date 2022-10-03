using FluentAssertions;
using MockingUnitTestsDemoApp.Impl.Models;
using MockingUnitTestsDemoApp.Impl.Repositories.Interfaces;
using MockingUnitTestsDemoApp.Impl.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MockingUnitTestsDemoApp.Tests.Services
{
    public class PlayerServiceTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetForLeague_ExistingId_ReturnListOfPlayers(int leagueID)
        {
            // Arrange
            ArrangeMocks(out var mockplayerRepo, out var mockteamRepo, out var mockleagueRepo, leagueID);

            var playerService = new PlayerService(
                mockplayerRepo.Object, mockteamRepo.Object, mockleagueRepo.Object);

            // Act
            var players = playerService.GetForLeague(leagueID);

            // Assert
            players.Should().NotBeEmpty();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void GetForLeague_ExistingIdButNoPlayersFound_ReturnEmptyListOfPlayers(int leagueID)
        {
            // Arrange
            ArrangeMocks(out var mockplayerRepo, out var mockteamRepo, out var mockleagueRepo, leagueID);

            var playerService = new PlayerService(
                mockplayerRepo.Object, mockteamRepo.Object, mockleagueRepo.Object);

            // Act
            var players = playerService.GetForLeague(leagueID);

            // Assert
            players.Should().BeEmpty();
        }

        [Theory]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(14)]
        public void GetForLeague_NonExistingId_ReturnEmptyListOfPlayers(int leagueID)
        {
            // Arrange
            ArrangeMocks(out var mockplayerRepo, out var mockteamRepo, out var mockleagueRepo, leagueID);

            var playerService = new PlayerService(
                mockplayerRepo.Object, mockteamRepo.Object, mockleagueRepo.Object);

            // Act
            var players = playerService.GetForLeague(leagueID);

            // Assert
            players.Should().BeEmpty();
        }

        private void ArrangeMocks(out Mock<IPlayerRepository> mockplayerRepo,
            out Mock<ITeamRepository> mockteamRepo, out Mock<ILeagueRepository> mockleagueRepo, int leagueID)
        {
            mockplayerRepo = new Mock<IPlayerRepository>();
            mockteamRepo = new Mock<ITeamRepository>();
            mockleagueRepo = new Mock<ILeagueRepository>();

            mockplayerRepo
                .Setup(x => x.GetForTeam(It.IsAny<int>()))
                .Returns(GetPlayerList()
                    .Where(x => x.TeamID == GetTeamList()
                        .Where(x => x.LeagueID == leagueID).Select(x => x.ID).FirstOrDefault()
                    ).ToList()
                );

            mockteamRepo
                .Setup(x => x.GetForLeague(leagueID))
                .Returns(GetTeamList().Where(x => x.LeagueID == leagueID).ToList());

            mockleagueRepo
                .Setup(x => x.IsValid(leagueID))
                .Returns(leagueID < 10);
        }

        private static List<Team> GetTeamList()
        {
            var teams = new List<Team>()
            {
                new Team() { ID = 1, LeagueID = 1 },
                new Team() { ID = 2, LeagueID = 1 },
                new Team() { ID = 3, LeagueID = 1 },
                new Team() { ID = 4, LeagueID = 2 },
                new Team() { ID = 5, LeagueID = 2 },
            };

            return teams;
        }

        private static List<Player> GetPlayerList()
        {
            var players = new List<Player>()
            {
                new Player() { ID = 1, TeamID = 1 },
                new Player() { ID = 2, TeamID = 2 },
                new Player() { ID = 3, TeamID = 3 },
                new Player() { ID = 4, TeamID = 4 },
                new Player() { ID = 5, TeamID = 5 },
            };

            return players;
        }
    }
}
