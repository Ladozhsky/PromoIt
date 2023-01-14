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
    campaign: string;    
    retweets: number;
    parsing_date: Date;
    creation_date: Date;
    update_date: Date;
    create_by_user: string;   
    update_by_user: string;   
    status: number;   
}

export interface twitterUserId {
    user_id: number;
    twitter_username: string;
    twitter_user_id: string;
}

export interface twitterUserId {
    user_id: number;
    twitter_username: string;
    twitter_user_id: string;
}