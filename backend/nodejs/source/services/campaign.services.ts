import * as _ from "underscore";
import { Queries } from "../constants";
import { systemError, campaign } from "../entities";
import { SqlHelper } from "../helpers/sql.helper";
import { ErrorService } from "./error.service";

interface ICampaignService {
  addCampaign(campaign: campaign): Promise<campaign>
}

export class CampaignService implements ICampaignService {
  private _errorService: ErrorService;

  constructor(private errorService: ErrorService) {
    this._errorService = errorService;
  }

  public addCampaign(campaign: campaign): Promise<campaign> {
    return new Promise<campaign>((resolve, reject) => {
      SqlHelper.createNew(
        this._errorService,
        Queries.AddCampaign,
        campaign,
        campaign.campaign_name,
        campaign.hashtag,
        campaign.description,
        campaign.user_id,
        campaign.company_id
      )
        .then((result: campaign) => {
          resolve(result);
        })
        .catch((error: systemError) => {
          reject(error);
        });
    });
  }
}
