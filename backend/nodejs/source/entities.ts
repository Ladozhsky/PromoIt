import { AppError} from "./enums";

export interface systemError {
    key: AppError;
    code: number;
    message: string;
}

export interface campaign {
    campaign_id: number;
    campaign_name: string;
    hashtag: string;
    description: string;
    user_id: number;
    company_id: number;
}