import { TwitterApiReadWrite } from "twitter-api-v2";

export class Queries {
    public static SelectIdentity: string = "SELECT SCOPE_IDENTITY() AS id;";

    public static AddCampaign: string = "INSERT campaign (campaign_name, hashtag, description, user_id, company_id) VALUES (?, ?, ?, ?, ?)";
    public static CampainHashtag: string = "SELECT hashtag, campaign_id FROM campaign";

    public static AddRetweet: string = "INSERT retweet (twitt_id, twitter_user_id, campaign_id, retweets, parsing_date, creation_date, update_date, create_by_user, update_by_user, status) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
    public static UpdateRetweet: string = "UPDATE retweet SET status = ? WHERE retweet_id = ?";
    public static LastNotProseedRetweet: string = "WITH cte AS (SELECT *, ROW_NUMBER() OVER (PARTITION BY campaign_id, twitter_user_id ORDER BY parsing_date DESC) AS rn FROM [promoit].[dbo].[retweet] WHERE status = 1) SELECT * FROM CTE WHERE rn = 1";
    public static MostRetweetedProsessedTweet: string = "WITH cte AS (SELECT *, ROW_NUMBER() OVER (PARTITION BY campaign_id, twitter_user_id ORDER BY retweets DESC) AS rn FROM [promoit].[dbo].[retweet] WHERE status = 2) SELECT * FROM CTE WHERE rn = 1";

    public static Transactions: string = "SELECT * FROM balance_transactions";
    public static TransactionsByUserId: string = "SELECT * FROM balance_transactions WHERE twitter_user_id = ?";
    public static AddTransaction: string = "INSERT balance_transactions (user_id, campaign_id, amount, reason, retweet_id, creation_date, update_date, create_by_user, update_by_user) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)";

    public static TwitterUserIds: string = "SELECT email FROM [promoit].[dbo].[user] WHERE user_id LIKE 'twitter|%'";
}

export const DB_CONNECTION_STRING: string = "Server=LAPTOP-V0M7V3Q0;Database=promoit;Trusted_Connection=True;";

export const client: TwitterApiReadWrite = new TwitterApiReadWrite({
  appKey: "OL3a0lj0AdyP4Yoswr9QFikM1",
  appSecret: "VCN23IQOU67cfQOOmlRiAAWYRgVyWHbEdwaLoQglsrHVctF6vz",
  accessToken: "1606972070222565382-dY5AVSpEC3q4XN5YOEGeKunRrovt4O",
  accessSecret: "5U92qoGomK3HB0W9oRBV5DPFGOKK4Zrlw6AFsrz0WlQLp",
});
