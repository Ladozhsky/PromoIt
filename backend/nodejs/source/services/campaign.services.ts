import * as _ from "underscore";
import { Queries } from "../constants";
import { systemError, campaign } from "../entities";
import { SqlHelper } from "../helpers/sql.helper";
import { ErrorService } from "./error.service";

interface localCampaign {
    campaign_id: number;
    campaign_name: string;
    hashtag: string;
    description: string;
    user_id: number;
    company_id: number;
}

interface ICampaignService {
    getCampaigns(): Promise<campaign[]>;
}

export class CampaignService implements ICampaignService {

    private _errorService: ErrorService;

    constructor(
        private errorService: ErrorService
    ) { 
        this._errorService = errorService;
    }


    public getCampaigns(): Promise<campaign[]> {
        return new Promise<campaign[]>((resolve, reject) => {
            const result: campaign[] = [];

            SqlHelper.executeQueryArrayResult<localCampaign>(this._errorService, Queries.Campaigns)
                .then((queryResult: localCampaign[]) => {
                    queryResult.forEach((campaign: localCampaign) => {
                        result.push(this.parseLocalCampaign(campaign));
                    });

                    resolve(result);
                })
                .catch((error: systemError) => {
                    reject(error);
                });
        });
    }

    private parseLocalCampaign(local: localCampaign): campaign {
        return {
            campaign_id: local.campaign_id,
            campaign_name: local.campaign_name,
            hashtag: local.hashtag,
            description: local.description,
            user_id: local.user_id,
            company_id: local.company_id
        };
    }
}