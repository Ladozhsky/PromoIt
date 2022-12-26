export class SqlParameters {
    public static Id: string = "id";
}

export class Queries {
    public static Campaigns: string = "SELECT * FROM campaign";
    public static SelectIdentity: string = "SELECT SCOPE_IDENTITY() AS id;";
    public static AddCampaign: string = "INSERT campaign (campaign_name, hashtag, description, user_id, company_id) VALUES (?, ?, ?, ?, ?)";
}

export const DB_CONNECTION_STRING: string = "server=.;Database=promoit;Trusted_Connection=Yes;Driver={ODBC Driver 17 for SQL Server}";
export const NON_EXISTENT_ID: number = -1;
