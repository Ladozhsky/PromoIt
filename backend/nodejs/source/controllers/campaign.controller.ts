import { Request, Response, NextFunction } from 'express';
import { systemError, campaign } from '../entities';
import { RequestHelper } from '../helpers/request.helper';
import { ResponseHelper } from '../helpers/response.helper';
import { ErrorService } from '../services/error.service';
import { CampaignService } from '../services/campaign.services';
import { NON_EXISTENT_ID } from '../constants';

const errorService: ErrorService = new ErrorService();
const campaignService: CampaignService = new CampaignService(errorService);

const getCampaigns = async (req: Request, res: Response, next: NextFunction) => {
    campaignService.getCampaigns()
        .then((result: campaign[]) => {
            return res.status(200).json({
                campaings: result
            });
        })
        .catch((error: systemError) => {
            return ResponseHelper.handleError(res, error);
        });
};

const getCampaignsByUserId = async (req: Request, res: Response, next: NextFunction) => {
    const numericParamOrError: number | systemError = RequestHelper.ParseNumericInput(errorService, req.params.id)
    if (typeof numericParamOrError === "number") {
        if (numericParamOrError > 0) {
            campaignService.getCampaignsByUserId(numericParamOrError)
                .then((result: campaign[]) => {
                    return res.status(200).json(result);
                })
                .catch((error: systemError) => {
                    return ResponseHelper.handleError(res, error);
                });
        }
        else {
            // TODO: Error handling
        }
    }
    else {
        return ResponseHelper.handleError(res, numericParamOrError);
    }
};

const addCampaign = async (req: Request, res: Response, next: NextFunction) => {
    const body: campaign = req.body;

    campaignService.addCampaign({
        campaign_name: body.campaign_name,
        hashtag: body.hashtag,
        description: body.description,
        user_id: body.user_id,
        company_id: body.company_id
    })
        .then((result: campaign) => {
            return res.status(200).json(result);
        })
        .catch((error: systemError) => {
            return ResponseHelper.handleError(res, error);
        });
};

export default { getCampaigns, addCampaign, getCampaignsByUserId }