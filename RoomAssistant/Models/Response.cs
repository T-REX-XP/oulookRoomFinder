
namespace RoomAssistant.Models
{
    public interface IRoom
    {
        string Start { get; set; }

        string End { get; set; }

        string Organizer { get; set; }

        string Subject { get; set; }

        bool IsFree { get; set; }
    }

    public class Room : IRoom
    {
        public string Start { get; set; }
        public string End { get; set; }
        public string Organizer { get; set; }
        public string Subject { get; set; }
        public bool IsFree { get; set; }
    }
}