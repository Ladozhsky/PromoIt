import { Request, Response, NextFunction } from 'express';
import { systemError, retweet, transaction } from '../entities';
import { ResponseHelper } from '../helpers/response.helper';
import { ErrorService } from '../services/error.service';
import { RetweetService } from '../services/twitter-related.setvises/retweet.services';

const errorService: ErrorService = new ErrorService();
const retweetService: RetweetService = new RetweetService(errorService);

const getRetweets = async (req: Request, res: Response, next: NextFunction) => {
    retweetService.getRetweets()
        .then((result: retweet[]) => {
            return res.status(200).json({
                campaings: result
            });
        })
        .catch((error: systemError) => {
            return ResponseHelper.handleError(res, error);
        });
};

const addRetweet = async (req: Request, res: Response, next: NextFunction) => {
    const body: retweet = req.body;

    retweetService.addRetweet({
        twitt_id: body.twitt_id,
        twitter_user_id: body.twitter_user_id,
        campaign: body.campaign,
        retweets: body.retweets,
        parsing_date: body.parsing_date,
        creation_date: body.creation_date,
        update_date: body.update_date,
        create_by_user: body.create_by_user,
        update_by_user: body.update_by_user,
        status: body.status
        
    })
        .then((result: retweet) => {
            return res.status(200).json(result);
        })
        .catch((error: systemError) => {
            return ResponseHelper.handleError(res, error);
        });
};

export default { getRetweets, addRetweet}