using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Windows.Forms;
using FUTAutoBuyer.Entities;
using FUTAutoBuyer.Params;
using FUTAutoBuyer.Native;
using FUTAutoBuyer;

namespace FUTAutoBuyer.Data
{
    public class PlayerData
    {
        public async void UpdatePlayerData(FUTClient client, ListBox lb)
        {           
            //keep a list of players we have in the DB - probably populate this 1st, then add as we go
            List<Item> CurrentPlayers = GetAllPlayers();
            List<uint> playerIDsStored;
            playerIDsStored = CurrentPlayers.Select(x => x.FifaPlayerDataID).ToList();

            //new request
            var playerSearchParams = new PlayerSearch() { Page = 1, League = 13 };
            var playerSearchResponse = await client.SearchAsync(playerSearchParams);

            foreach (var auctionInfo in playerSearchResponse.AuctionInfo)
            {
                if (!playerIDsStored.Contains(Convert.ToUInt32(auctionInfo.ItemData.AssetId)))
                {
                    var playerDataSearchResponse = await client.GetItemAsync(auctionInfo);
                
                    if (playerDataSearchResponse != null)
                    {
                        AddPlayer(playerDataSearchResponse, Convert.ToInt32(auctionInfo.ItemData.AssetId));
                        playerIDsStored.Add(Convert.ToUInt32(auctionInfo.ItemData.AssetId));

                        lb.BeginUpdate();       
                        lb.Items.Add("Added player : " + playerDataSearchResponse.FirstName + " " + playerDataSearchResponse.LastName + " " + playerDataSearchResponse);
                        lb.EndUpdate();
                    }
                }
            }
        }

        public List<Item> GetAllPlayers()
        {
            ABDatabase abd = new ABDatabase();
            DataSet ds = new DataSet();
            List<Item> pi = new List<Item>();

            try
            {
                ds = abd.RunProcedure("PlayersGetAll", new List<System.Data.SqlClient.SqlParameter>());

                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        DateTime dateofbirth = Convert.ToDateTime(dr["dob"]);
                        DOB playerDOB = new DOB
                        {
                            Day = Convert.ToByte(dateofbirth.Day),
                            Month = Convert.ToByte(dateofbirth.Month),
                            Year = Convert.ToUInt16(dateofbirth.Year)
                        };

                        Item nextPi = new Item
                        {
                            FifaPlayerDataID = Convert.ToUInt32(dr["fifa_player_data_id"]),
                            FirstName = dr["first_name"].ToString(),
                            LastName = dr["last_name"] != null ? dr["last_name"].ToString() : "",
                            CommonName = dr["common_name"] != null ? dr["common_name"].ToString() : "",
                            Height = Convert.ToByte(dr["height"]),                            
                            DateOfBirth = playerDOB,
                            PreferredFoot = dr["preferred_foot"].ToString(),
                            ClubId = Convert.ToUInt32(dr["club_id"]),
                            LeagueId = Convert.ToUInt32(dr["league_id"]),
                            NationId = Convert.ToUInt32(dr["nation_id"]),
                            Rating = Convert.ToByte(dr["rating"]),
                            Attribute1 = Convert.ToByte(dr["attribute1"]),
                            Attribute2 = Convert.ToByte(dr["attribute2"]),
                            Attribute3 = Convert.ToByte(dr["attribute3"]),
                            Attribute4 = Convert.ToByte(dr["attribute4"]),
                            Attribute5 = Convert.ToByte(dr["attribute5"]),
                            Attribute6 = Convert.ToByte(dr["attribute6"]),
                            Rare = (PlayerRareType) Enum.Parse(typeof(PlayerRareType), dr["rare_type"].ToString()),
                            CardType = (CardType) Enum.Parse(typeof(CardType), dr["card_type"].ToString()),
                        };
                        pi.Add(nextPi);
                    }
                }
            }
            finally
            {
                ds.Dispose();
            }

            return pi;
        }

        public void AddPlayer(Item pi, int playerDataID)
        {
            ABDatabase abd = new ABDatabase();
            List<SqlParameter> par = new List<SqlParameter>();
            par.Add(new SqlParameter("@FifaPlayerDataID", SqlDbType.Int));
            par.Add(new SqlParameter("@FirstName", SqlDbType.NVarChar));
            par.Add(new SqlParameter("@LastName", SqlDbType.NVarChar));
            par.Add(new SqlParameter("@CommonName", SqlDbType.NVarChar));
            par.Add(new SqlParameter("@Height", SqlDbType.Int));
            par.Add(new SqlParameter("@DOB", SqlDbType.DateTime));
            par.Add(new SqlParameter("@PreferredFoot", SqlDbType.NVarChar));
            par.Add(new SqlParameter("@ClubID", SqlDbType.Int));
            par.Add(new SqlParameter("@LeagueID", SqlDbType.Int));
            par.Add(new SqlParameter("@NationID", SqlDbType.Int));
            par.Add(new SqlParameter("@Rating", SqlDbType.Int));
            par.Add(new SqlParameter("@Attribute1", SqlDbType.Int));
            par.Add(new SqlParameter("@Attribute2", SqlDbType.Int));
            par.Add(new SqlParameter("@Attribute3", SqlDbType.Int));
            par.Add(new SqlParameter("@Attribute4", SqlDbType.Int));
            par.Add(new SqlParameter("@Attribute5", SqlDbType.Int));
            par.Add(new SqlParameter("@Attribute6", SqlDbType.Int));
            par.Add(new SqlParameter("@RareType", SqlDbType.Int));
            par.Add(new SqlParameter("@CardType", SqlDbType.Int));

            par[0].Value = playerDataID;
            par[1].Value = pi.FirstName;
            par[2].Value = pi.LastName;
            par[3].Value = pi.CommonName == null ? "" : pi.CommonName;
            par[4].Value = Convert.ToInt32(pi.Height);
            int year = Convert.ToInt32(pi.DateOfBirth.Year);
            int month = Convert.ToInt32(pi.DateOfBirth.Month);
            int day = Convert.ToInt32(pi.DateOfBirth.Day);
            DateTime dob = new DateTime(year, month, day);
            par[5].Value = dob;
            par[6].Value = pi.PreferredFoot;
            par[7].Value = Convert.ToInt32(pi.ClubId);
            par[8].Value = Convert.ToInt32(pi.LeagueId);
            par[9].Value = Convert.ToInt32(pi.NationId);
            par[10].Value = Convert.ToInt32(pi.Rating);
            par[11].Value = Convert.ToInt32(pi.Attribute1);
            par[12].Value = Convert.ToInt32(pi.Attribute2);
            par[13].Value = Convert.ToInt32(pi.Attribute3);
            par[14].Value = Convert.ToInt32(pi.Attribute4);
            par[15].Value = Convert.ToInt32(pi.Attribute5);
            par[16].Value = Convert.ToInt32(pi.Attribute6);
            par[17].Value = Convert.ToInt32(pi.Rare);
            par[18].Value = Convert.ToInt32(pi.CardType);

            try
            {
                abd.RunProcedure("PlayerAdd", par);
            }
            finally
            {

            }
        }
    }
}
