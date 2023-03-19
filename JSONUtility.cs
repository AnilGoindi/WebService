namespace WebService
{
    public static class JSONUtility
    {
        public static string UpdateJSONData(LicenseHandler licData, string jsonFile)
        {
            string json = File.ReadAllText(jsonFile);
            dynamic jsonItem = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            jsonItem["LicenseNumber"]/*[0]["Password"]*/ = licData.LicenseNumber;
            jsonItem["ClientName"]/*[0]["Password"]*/ = licData.ClientName;
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonItem, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(jsonFile, output);

            return "Licence has been updated";
        }


    }
}
