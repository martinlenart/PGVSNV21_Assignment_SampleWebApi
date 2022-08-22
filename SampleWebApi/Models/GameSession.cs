using System;
namespace SampleWebApi.Models
{
	public class GameSession
	{
        public Guid SessionId { get; set; }
        public bool IsRunning { get; set; } = false;
        public string GameType { get; set; }
        public DateTime? StartTime { get; set; } = null;
        public DateTime? EndTime { get; set; } = null;


        public bool StartGame(string gameType)
        { 
            if (!IsRunning)
            {
                SessionId = Guid.NewGuid();
                IsRunning = true;
                GameType = gameType;
                StartTime = DateTime.Now;
                return true;
            }

            return false;
        }

        public bool EndGame()
        {
            if (IsRunning)
            {
                IsRunning = false;
                EndTime = DateTime.Now;
                return true;
            }

            return false;
        }
    }
}

