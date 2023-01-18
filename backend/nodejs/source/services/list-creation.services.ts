import {DbGetService} from './db.services';
import { ErrorService } from './error.service';

import { retweet, transaction, twitterUserIds, campaignIdHashtag } from '../entities';
import { TwitterService } from "./twitter.services";
import { Queries } from "../constants";
import { RetweetService } from './retweet.services';
import { error } from 'console';

const errorService: ErrorService = new ErrorService();
const dbGetService: DbGetService = new DbGetService(errorService);
const twitterService: TwitterService = new TwitterService(errorService);

const retweetService: RetweetService = new RetweetService(errorService);

interface localRetweet extends retweet {
    rn: number;
    retweet_id: number;
}

interface IListCreation {
    createListOfRetweets(): Promise<retweet[]>;
    createListOfTransaction () : Promise<transaction[]>
}
  
export class ListCreation implements IListCreation {
    
    private _errorService: ErrorService;

    constructor(private errorService: ErrorService) {
      this._errorService = errorService;
    }

    // Use created list of Ids and Hashtags to create list of retweets
    public async createListOfRetweets () : Promise<retweet[]> {
      
        const userIds : twitterUserIds[] = await dbGetService.getAllCollumnData(Queries.TwitterUserIds);
        const campaignsData : campaignIdHashtag[] = await dbGetService.getAllCollumnData(Queries.CampainHashtag);

        const retweetArray : retweet[] = [];

        try {
            for (let i = 0; i < userIds.length; i++) {
            for (let j = 0; j < campaignsData.length; j++) {
                const tweets = await twitterService.searchTweetsByIdHashtag(userIds[i].email, campaignsData[j]);
                retweetArray.push(...tweets)
                }
            }
        } catch (error) {
            console.log(error)
        }    
        return retweetArray;
    }

    public async createListOfTransaction () : Promise<transaction[]> {
       
        
        const lastRetweetAr : localRetweet[] = await dbGetService.getAllCollumnData(Queries.LastNotProseedRetweet);
        const top2RetweetAr : localRetweet[] = await dbGetService.getAllCollumnData(Queries.MostRetweetedProsessedTweet);
        const transactionArray : transaction[] = [];
        try {
        for (let i = 0; i < lastRetweetAr.length; i++) {
            let mostRetweetedTweet : localRetweet  = top2RetweetAr[top2RetweetAr.findIndex(item => item.twitter_user_id === lastRetweetAr[i].twitter_user_id && item.campaign_id === lastRetweetAr[i].campaign_id)];
            if(mostRetweetedTweet === undefined){
                transactionArray.push(this.parselocalTransaction(lastRetweetAr[i], lastRetweetAr[i].retweets + 1, "FTP")) // FTP - first tweet posting
            }  
            // if have new retweets
            else if (lastRetweetAr[i].retweets > mostRetweetedTweet.retweets){
                transactionArray.push(this.parselocalTransaction(lastRetweetAr[i], lastRetweetAr[i].retweets - mostRetweetedTweet.retweets, "RC")) // RC - retweet counting
            }
            else {
                retweetService.updateRetweetByScript(lastRetweetAr[i].retweet_id)
            }
        }     
        } catch (error) {
            console.log(error)
        }
        return transactionArray
    }

    private parselocalTransaction(local: localRetweet, amount: number, reason: string): transaction {
        return {
            user_id: local.twitter_user_id,
            campaign_id: local.campaign_id,   
            amount: amount,
            reason: reason,
            retweet_id: local.retweet_id,
            create_date: new Date(),
            update_date: new Date(),
            create_by_user: "server_script",  
            update_by_user: "server_script"  
        };
    }

}





