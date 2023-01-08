export class SqlParameters {
    public static Id: string = "id";
}

export class Queries {
    public static SelectIdentity: string = "SELECT SCOPE_IDENTITY() AS id;";
    public static Campaigns: string = "SELECT * FROM campaign";
    public static CampaignsByUserId: string = "SELECT * FROM campaign WHERE user_id = ?";
    public static AddCampaign: string = "INSERT campaign (campaign_name, hashtag, description, user_id, company_id) VALUES (?, ?, ?, ?, ?)";

    public static Companies: string = "SELECT * FROM company";
    public static AddCompany: string = "INSERT company (company_name, site, email, company_type) VALUES (?, ?, ?, ?)";

    public static Retweets: string = "SELECT * FROM retweet";
    public static RetweetsByUserId: string = "SELECT * FROM retweet WHERE user_id = ?";
    public static AddRetweet: string = "INSERT retweet (twitt_id, twitter_user_id, retweets, campaign, parsing_date) VALUES (?, ?, ?, ?, ?)";

    //requests for twitter parsing
    public static TwitterUserIds: string = "SELECT twitter_user_id FROM twitter_accounts";
    public static CampainHashtag: string = "SELECT hashtag FROM campaign";

}

export const DB_CONNECTION_STRING: string = "server=DESKTOP-MRQ963D\\MSSQLSERVER4;Database=promoit;Trusted_Connection=Yes;Driver={ODBC Driver 17 for SQL Server}";
export const NON_EXISTENT_ID: number = -1;
