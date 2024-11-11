using HospitalSystem.Domain.Common;

namespace HospitalSystem.Domain.Entities
{
    public class Photo : EntityBase
    {
        public string Url { get; set; }
        public bool IsMain { get; set; }

        public Photo(string url, bool isMain = false)
        {
            Url = url;
            IsMain = isMain;
        }
    }

}