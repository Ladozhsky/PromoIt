import campaignController from "./controllers/campaign.controller";
import { AppError, CompanyType} from "./enums";

export interface systemError {
    key: AppError;
    code: number;
    message: string;
}

export interface campaign {
    campaign_name: string;
    hashtag: string;
    description: string;
    user_id: number;
    company_id: number;
}

export interface company {
    company_name: string;
    site: string;
    email: string;
    company_type: CompanyType;
}

export interface retweet {
    twitt_id: string;
    twitter_user_id: string ;
    retweets: number;
    campaign: string;
    parsing_date: Date;
}

export interface twitterUserId {
    user_id: number;
    twitter_username: string;
    twitter_user_id: string;
}