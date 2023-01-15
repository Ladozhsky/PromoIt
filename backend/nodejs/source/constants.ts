import { TwitterApiReadWrite } from "twitter-api-v2";

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
    public static AddRetweet: string = "INSERT retweet (twitt_id, twitter_user_id, campaign, retweets, parsing_date, creation_date, update_date, create_by_user, update_by_user, status) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

    public static Transactions: string = "SELECT * FROM balance_transactions";
    public static TransactionsByUserId: string = "SELECT * FROM balance_transactions WHERE twitter_user_id = ?";
    public static AddTransaction: string = "INSERT balance_transactions (user_id, campaign, amount, reason, retweet_id, creation_date, update_date, create_by_user, update_by_user) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";

    //requests for twitter parsing and ingection
    public static TwitterUserIds: string = "SELECT twitter_user_id FROM twitter_accounts";
    public static CampainHashtag: string = "SELECT hashtag, campaign_name FROM campaign";

    //requests for balance transaction

    public static LastRetweet: string = "WITH cte AS (SELECT *, ROW_NUMBER() OVER (PARTITION BY campaign, twitter_user_id ORDER BY parsing_date DESC) AS rn FROM [promoit].[dbo].[retweet] WHERE status = 1) SELECT * FROM CTE WHERE rn = 1";
    public static Top2Retweet: string = "WITH cte AS (SELECT *, ROW_NUMBER() OVER (PARTITION BY campaign, twitter_user_id ORDER BY retweets DESC) AS rn FROM [promoit].[dbo].[retweet] WHERE status = 2) SELECT * FROM CTE WHERE rn = 1";
}

export const DB_CONNECTION_STRING: string = "server=DESKTOP-MRQ963D\\MSSQLSERVER4;Database=promoit;Trusted_Connection=Yes;Driver={ODBC Driver 17 for SQL Server}";
export const NON_EXISTENT_ID: number = -1;

export const client:TwitterApiReadWrite = new TwitterApiReadWrite({
    appKey: "OL3a0lj0AdyP4Yoswr9QFikM1",
    appSecret: "VCN23IQOU67cfQOOmlRiAAWYRgVyWHbEdwaLoQglsrHVctF6vz",
    accessToken: "1606972070222565382-dY5AVSpEC3q4XN5YOEGeKunRrovt4O",
    accessSecret: "5U92qoGomK3HB0W9oRBV5DPFGOKK4Zrlw6AFsrz0WlQLp",
})
