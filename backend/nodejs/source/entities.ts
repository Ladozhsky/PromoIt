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