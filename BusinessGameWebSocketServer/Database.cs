using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessGameWebSocketServer.DatabaseObjects;
using SimpleJSON;
using System.IO;

namespace BusinessGameWebSocketServer
{
    class Database
    {

        private Dictionary<string, string> userTokens;

        public JSONObject cities;
        public JSONObject characters;

        public Database(bool dummy)
        {
            userTokens = new Dictionary<string, string>();
            LoadDatabase();
        }

        public JSONObject GetPropertyData(string city, string property)
        {
            return cities[city]["properties"][property].AsObject;
        }

        public JSONObject CreateCharacter(string username, string password)
        {
            bool success = characters[username] == null;
            JSONObject successObject = new JSONObject();

            if (success)
            {
                characters[username]["username"] = username;
                characters[username]["password"] = password;
            }

            successObject.Add("success", success);
            return successObject;
        }

        public JSONObject LogIn(string username, string password)
        {
            bool success = false;
            success = characters[username] != null
                && characters[username]["password"].Equals(password)
                && !userTokens.ContainsKey(username)
            ;

            JSONObject successObject = new JSONObject();
            successObject.Add("success", success);
            if (success)
            {
                
                string token = "";
                string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";

                Random rnd = new Random();
                for (int i = 0; i < 10; i++)
                {
                    token += glyphs[rnd.Next(glyphs.Length)];
                }

                successObject.Add("token", token);
                userTokens.Add(username, token);
            }
            
            return successObject;
        }

        public void SaveDatabase()
        {
            // Set a variable to the My Documents path.
            string mydocpath =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\database.bgd"))
            {
                outputFile.WriteLine(cities.ToString());
                outputFile.WriteLine(characters.ToString());
            }
        }

        public void LoadDatabase()
        {
            // Set a variable to the My Documents path.
            string mydocpath =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Write the string array to a new file named "WriteLines.txt".
            using (StreamReader inputFile = new StreamReader(mydocpath + @"\database.bgd"))
            {
                cities = JSON.Parse(inputFile.ReadLine().ToString()).AsObject;
                characters = JSON.Parse(inputFile.ReadLine().ToString()).AsObject;
            }
        }
    }

    /*
     * if (dummy)
            {
                JSONObject c = new JSONObject();
                JSONObject p = new JSONObject();

                p["owner"] = "Eivind";
                p["address"] = "Lervikvegen 98";
                p["sizeX"] = 30;
                p["sizeY"] = 10;
                p["bgData"] = new JSONArray();
                for (int i=0; i<(p["sizeX"] * p["sizeY"]); i++)
                {
                    p["bgData"].Add(0);
                }

                c["name"] = "Manger";
                c["properties"].Add(p["address"], p);

                cities.Add(c["name"], c);
            }
            */
}
