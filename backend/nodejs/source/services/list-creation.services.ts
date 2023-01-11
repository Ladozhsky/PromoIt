import {DbGetService, ITwitterUserIds, ICanpaignHashtag} from '../services/db.services';
import { ErrorService } from '../services/error.service';
import {  retweet } from '../entities';
import { TwitterService } from "./twitter.services";
import { resolve } from 'path';

const errorService: ErrorService = new ErrorService();
const dbGetService: DbGetService = new DbGetService(errorService);
const twitterService: TwitterService = new TwitterService();

interface IListCreation {
    createListOfRetweets(): Promise<retweet[]>;
}
  
export class ListCreation implements IListCreation {
    // Use created list of Ids and Hashtags to create list of retweets
    public  async createListOfRetweets () : Promise<retweet[]> {
        const userIds : ITwitterUserIds[] = await dbGetService.getTwitterUserIds();
        const hashtag : ICanpaignHashtag[] = await dbGetService.getCanpaignHashtag();
        const retweetArray : retweet[] = [];
            for (let i = 0; i < userIds.length; i++) {
                for (let j = 0; j < hashtag.length; j++) {
                    const tweets = await twitterService.searchTweetsByIdHashtag(userIds[i].twitter_user_id, hashtag[j].hashtag);
                    retweetArray.push(...tweets)
                
                }
            }
            return retweetArray;
        }

}

