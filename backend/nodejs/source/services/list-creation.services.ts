import {DbGetService, ITwitterUserIds} from '../services/db.services';
import { ErrorService } from '../services/error.service';
import { retweet, campaign } from '../entities';
import { TwitterService } from "./twitter.services";
import { Queries } from "../constants";
import {ICanpaignHashtag} from "./twitter.services"

const errorService: ErrorService = new ErrorService();
const dbGetService: DbGetService = new DbGetService(errorService);
const twitterService: TwitterService = new TwitterService();

interface IListCreation {
    createListOfRetweetsTw(): Promise<retweet[]>;
}
  
export class ListCreation implements IListCreation {
    // Use created list of Ids and Hashtags to create list of retweets
    public  async createListOfRetweetsTw () : Promise<retweet[]> {
        const userIds : ITwitterUserIds[] = await dbGetService.getAllCollumnData(Queries.TwitterUserIds);
        const campaignsData : ICanpaignHashtag[] = await dbGetService.getAllCollumnData(Queries.CampainHashtag);
        const retweetArray : retweet[] = [];
            for (let i = 0; i < userIds.length; i++) {
                for (let j = 0; j < campaignsData.length; j++) {
                    const tweets = await twitterService.searchTweetsByIdHashtag(userIds[i].twitter_user_id, campaignsData[j]);
                    retweetArray.push(...tweets)
                }
            }
        return retweetArray;
    }

}

