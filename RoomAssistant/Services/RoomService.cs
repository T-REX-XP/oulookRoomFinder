using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security;
using RoomAssistant.Models;
using Microsoft.Exchange.WebServices.Data;

namespace RoomAssistant.Services
{
    public class RoomService
    {
        private static ExchangeService _exchangeService;

        public RoomService()
        {
            IUserData userData = CreateUserData();
            _exchangeService = Service.ConnectToService(userData);
        }
        /// <summary>
        /// Find and then cancel a meeting.
        /// </summary>
        /// <param name="service">An ExchangeService object with credentials and the EWS URL.</param>
        public IDictionary<string, IList<IRoom>> FindRooms()
        {
            IDictionary<string, IList<IRoom>> result = new Dictionary<string, IList<IRoom>>();
            try
            {
                DateTime startDate = DateTime.Today;
                DateTime endDate = DateTime.Today.AddDays(1);
                CalendarView cv = new CalendarView(startDate, endDate);
                string roomsFromSettings = ConfigurationManager.AppSettings["roomList"];

                IList<string> rooms = roomsFromSettings.Split(',').ToList();
                foreach (var room in rooms)
                {
                    int i = 1;
                    while (i < 9)
                    {
                        try
                        {
                            string roomPerFloor = room.Replace("x", i.ToString());
                            IList<IRoom> roomList = FillRoom(roomPerFloor, cv);
                            result.Add(roomPerFloor, roomList);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error message: " + ex.Message);
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error message: " + ex.Message);
            }
            return result;
        }

        private IList<IRoom> FillRoom(string room, CalendarView cv)
        {
            IList<IRoom> roomList = FillCalendarDay();

            String mailboxToAccess = room;
            FolderId calendarFolderId = new FolderId(WellKnownFolderName.Calendar, mailboxToAccess);
            
            IList<Appointment> appointments = _exchangeService.FindAppointments(calendarFolderId, cv).ToList();

            foreach (var appointment in appointments)
            {
                IRoom result = new Room();
                result.Organizer = appointment.Organizer.Name;
                result.Subject = string.IsNullOrEmpty(appointment.Subject) ? "No subject" : appointment.Subject;
                result.Start = appointment.Start.ToShortTimeString();
                result.End = appointment.End.ToShortTimeString();
                result.IsFree = false;
                roomList.Add(result);
            }
            return roomList;
        }

        private IList<IRoom> FillCalendarDay()
        {
            IList<IRoom> result = new List<IRoom>();
            // TODO. Fill the list with the data received
            return result;
        }

        private IUserData CreateUserData()
        {
            IUserData userData = new UserData();
            userData.EmailAddress = ConfigurationManager.AppSettings["id"];
            string key = ConfigurationManager.AppSettings["key"];
            char[] keyArray = key.ToCharArray();
            userData.Password = new SecureString();
            foreach (char keyChar in keyArray)
            {
                userData.Password.AppendChar(keyChar);
            }
            userData.Version = ExchangeVersion.Exchange2013;
            return userData;
        }

    }
}