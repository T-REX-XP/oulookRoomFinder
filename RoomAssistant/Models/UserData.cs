using System;
using System.Security;
using Microsoft.Exchange.WebServices.Data;

namespace RoomAssistant.Models
{
    public interface IUserData
    {
        ExchangeVersion Version { get; set; }
        string EmailAddress { get; set; }
        SecureString Password { get; set; }
        Uri AutodiscoverUrl { get; set; }
    }

    public class UserData : IUserData
    {
        public ExchangeVersion Version { get; set; }
        public string EmailAddress { get; set; }
        public SecureString Password { get; set; }
        public Uri AutodiscoverUrl { get; set; }
    }
}