import { AppError } from "./enums";

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

export interface retweet {
  twitt_id: string;
  twitter_user_id: string;
  campaign_id: number;
  retweets: number;
  parsing_date: Date;
  creation_date: Date;
  update_date: Date;
  create_by_user: string;
  update_by_user: string;
  status: number;
}

export interface transaction {
  user_id: string;
  campaign_id: number;
  amount: number;
  reason: string;
  retweet_id: number;
  create_date: Date;
  update_date: Date;
  create_by_user: string;
  update_by_user: string;
}

export interface twitterUserId {
  user_id: number;
  twitter_username: string;
  twitter_user_id: string;
}
export interface twitterUserIds {
  email_twitter_id: string;
}

export interface campaignIdHashtag {
  hashtag: string;
  campaign_id: number;
}

export interface tweetParams {
  twitter_user_id: string;
  campaign: string;
  company: string;
  product: string;
}
