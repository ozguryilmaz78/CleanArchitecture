using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Abstractions
{
    public class BaseEntity
    {
        TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");

        #region Constructure
        public BaseEntity(Guid id)
        {
            Id = id;
        }
        #endregion

        #region Method
        public void CreateBaseEntity(string recUser)
        {
            RecDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZone);
            RecUser = recUser;
        }
        public void UpdateBaseEntity(string updateUser)
        {
            RecDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZone);
            RecUser = updateUser;
        }
        public void DeleteBaseEntity(string deleteUser)
        {
            RecDate = TimeZoneInfo.ConvertTime(DateTime.Now, timeZone);
            RecUser = deleteUser;
            isDeleted = true;
        }
        #endregion

        public Guid Id { get; set; }
        public DateTime RecDate { get; set; }
        public string RecUser { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime DeleteDate { get; set; }
        public string DeleteUser { get; set; }
        public bool isDeleted { get; set; }
      
    }
}
