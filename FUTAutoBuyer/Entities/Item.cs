using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FUTAutoBuyer.Entities
{
    public class Item
    {
        private byte _rating;

        /* Attributes vary by position? */

        public uint FifaPlayerDataID { get; set; }

        /* Pace */
        public byte Attribute1 { get; set; }

        /* Shooting */
        public byte Attribute2 { get; set; }

        /* Passing */
        public byte Attribute3 { get; set; }

        /* Dribbling */
        public byte Attribute4 { get; set; }

        /* Defending */
        public byte Attribute5 { get; set; }

        /* Heading */
        public byte Attribute6 { get; set; }

        public uint ClubId { get; set; }

        public string CommonName { get; set; }

        public DOB DateOfBirth { get; set; }

        public string FirstName { get; set; }

        public byte Height { get; set; }

        public string ItemType { get; set; }

        public string LastName { get; set; }

        public uint LeagueId { get; set; }

        public uint NationId { get; set; }

        public string PreferredFoot { get; set; }

        public PlayerRareType Rare { get; set; }

        public byte Rating
        {
            get { return _rating; }
            set
            {
                _rating = value;
                SetCardType();
            }
        }

        public CardType CardType { get; set; }

        private void SetCardType()
        {
            if (Rating < 65)
                CardType = CardType.Bronze;
            else if (Rating < 75)
                CardType = CardType.Silver;
        }
    }
}
