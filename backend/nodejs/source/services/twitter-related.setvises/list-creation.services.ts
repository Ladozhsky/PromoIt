import {DbGetService} from '../db.services';
import { ErrorService } from '../error.service';
import { retweet, transaction } from '../../entities';
import { TwitterService } from "./twitter.services";
import { Queries } from "../../constants";
import {ICanpaignHashtag} from "./twitter.services"

const errorService: ErrorService = new ErrorService();
const dbGetService: DbGetService = new DbGetService(errorService);
const twitterService: TwitterService = new TwitterService();

export interface ITwitterUserIds {
    twitter_user_id: string;
  }

interface localRetweet extends retweet {
    rn: number;
    retweet_id: number;
}

interface IListCreation {
    createListOfRetweetsTw(): Promise<retweet[]>;
    createListOfTransactionDemo () : Promise<transaction[]>
}
  
export class ListCreation implements IListCreation {
    // Use created list of Ids and Hashtags to create list of retweets
    public async createListOfRetweetsTw () : Promise<retweet[]> {
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

    public async createListOfTransactionDemo () : Promise<transaction[]> {
        const lastRetweetAr : localRetweet[] = await dbGetService.getAllCollumnData(Queries.LastRetweet);
        const top2RetweetAr : localRetweet[] = await dbGetService.getAllCollumnData(Queries.Top2Retweet);
        const transactionArray : transaction[] = [];
        for (let i = 0; i < lastRetweetAr.length; i++) {
            let top2Retweet : localRetweet  = top2RetweetAr[top2RetweetAr.findIndex(item => item.twitter_user_id === lastRetweetAr[i].twitter_user_id && item.campaign === lastRetweetAr[i].campaign)];
            if(top2Retweet === undefined){
                transactionArray.push(this.parselocalTransaction(lastRetweetAr[i], lastRetweetAr[i].retweets + 1, "FTP")) // FTP - first tweet posting
            }  

            // if have neew retweets
            else if (lastRetweetAr[i].retweets - top2Retweet.retweets > 0){
                transactionArray.push(this.parselocalTransaction(lastRetweetAr[i], lastRetweetAr[i].retweets - top2Retweet.retweets, "RC")) // RC - retweet counting
            }
        }
        return transactionArray
    }

    private parselocalTransaction(local: localRetweet, amount: number, reason: string): transaction {
        return {
            user_id: local.twitter_user_id,
            campaign: local.campaign,   
            amount: amount,
            reason: reason,
            retweet_id: local.retweet_id,
            creation_date: new Date(),
            update_date: new Date(),
            create_by_user: "server_script",  
            update_by_user: "server_script"  
        };
    }

}



// async function name  () {
// const list : ListCreation = new ListCreation
// const newList = await list.createListOfTransactionDemo()
// return console.log(newList)}

// name ()






