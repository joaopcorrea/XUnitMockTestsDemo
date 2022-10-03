using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MockingUnitTestsDemoApp.Tests.Services
{
    public class PlayerServiceTest
    {
        [Fact]
        public void GetForLeague_ExistingId_ReturnListOfPlayers()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void GetForLeague_ExistingIdButNoPlayersFound_ReturnEmptyListOfPlayers()
        {

        }

        [Fact]
        public void GetForLeague_NonExistingId_ReturnEmptyListOfPlayers()
        {

        }
    }
}
