using System.Collections.Generic;

namespace FUTAutoBuyer.Entities
{
    public class Persona
    {
        public long PersonaId { get; set; }

        public string PersonaName { get; set; }

        public IEnumerable<UserClub> UserClubList { get; set; }

        public bool ReturningUser { get; set; }
    }
}
