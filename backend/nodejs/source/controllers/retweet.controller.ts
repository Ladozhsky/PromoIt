import { Request, Response, NextFunction } from 'express';
import { systemError, retweet } from '../entities';
import { RequestHelper } from '../helpers/request.helper';
import { ResponseHelper } from '../helpers/response.helper';
import { ErrorService } from '../services/error.service';
import { RetweetService } from '../services/retweet.services';
import { NON_EXISTENT_ID } from '../constants';

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

const getRetweetsByUserId = async (req: Request, res: Response, next: NextFunction) => {
    const numericParamOrError: number | systemError = RequestHelper.ParseNumericInput(errorService, req.params.id)
    if (typeof numericParamOrError === "number") {
        if (numericParamOrError > 0) {
            retweetService.getRetweetsByUserId(numericParamOrError)
                .then((result: retweet[]) => {
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

const addRetweet = async (req: Request, res: Response, next: NextFunction) => {
    const body: retweet = req.body;

    retweetService.addRetweet({
        twitt_id: body.twitt_id,
        twitter_user_id: body.twitter_user_id,
        retweets: body.retweets,
        campaign: body.campaign,
        parsing_date: body.parsing_date
    })
        .then((result: retweet) => {
            return res.status(200).json(result);
        })
        .catch((error: systemError) => {
            return ResponseHelper.handleError(res, error);
        });
};

export default { getRetweets, addRetweet, getRetweetsByUserId }