import { Request, Response, NextFunction } from 'express';
import { systemError, retweet, tweetParams } from '../entities';
import { ErrorService } from '../services/error.service';
import { RetweetService } from '../services/retweet.services';
import { TwitterService } from '../services/twitter.services';

const errorService: ErrorService = new ErrorService();
const retweetService: RetweetService = new RetweetService(errorService);
const tweeterService: TwitterService = new TwitterService(errorService);

const addRetweets = async (retweetListInput: Promise<retweet[]>) => {
  const retweetList : retweet[] = await retweetListInput
  for (let i = 0; i < retweetList.length; i++) {
      retweetService.addRetweet({
        twitt_id: retweetList[i].twitt_id,
        twitter_user_id: retweetList[i].twitter_user_id,
        campaign_id: retweetList[i].campaign_id,
        retweets: retweetList[i].retweets,
        parsing_date: new Date(),
        creation_date: new Date(),
        update_date: new Date(),
        create_by_user: retweetList[i].create_by_user,
        update_by_user: retweetList[i].update_by_user,
        status: retweetList[i].status,
      })
        .then(() => {
          return console.log(retweetList[i]);
        })
        .catch((error: systemError) => {
          return console.log(error);
        });
    }
  }
  
const postTweet = async (req: Request, res: Response, next: NextFunction) => {
  const body : tweetParams  = req.body;
  tweeterService.postTweet(body)
    .then(() => {
      return console.log(req);
    })
    .catch((error: systemError) => {
      return console.log(error);
    });
}
    
export default { addRetweets, postTweet }
