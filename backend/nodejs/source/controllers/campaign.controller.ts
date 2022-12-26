import { Request, Response, NextFunction } from 'express';
import { systemError, campaign } from '../entities';
import { RequestHelper } from '../helpers/request.helper';
import { ResponseHelper } from '../helpers/response.helper';
import { ErrorService } from '../services/error.service';
import { CampaignService } from '../services/campaign.services';

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

export default { getCampaigns }