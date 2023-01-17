import { retweet, campaignIdHashtag, purchase} from '../entities';
import { client} from "../constants"
import { TweetV2, TweetSearchRecentV2Paginator, UserV2Result} from "twitter-api-v2";
import { ErrorService } from "./error.service";


interface ITwitterService {
    searchTweetsByIdHashtag(userId : string , campaignsData : campaignIdHashtag): Promise<retweet[]>;
  }

export class TwitterService implements ITwitterService {
  private _errorService: ErrorService;

  constructor(private errorService: ErrorService) {
    this._errorService = errorService;
  }
    public async searchTweetsByIdHashtag (userId : string , campaignsData : campaignIdHashtag) : Promise<retweet[]> {
        const twitterQuery : string = `${campaignsData.hashtag} from:${userId}`;
        let retweetArray : retweet[] = [];

        const jsTweets : TweetSearchRecentV2Paginator = await client.v2.search(
            twitterQuery, 
            {
                'tweet.fields': 'public_metrics,author_id,referenced_tweets',
            })
        {
        for (const tweet of jsTweets) {
          if(tweet?.referenced_tweets != undefined) {
            let retweetedCheck : boolean = false
            for (const referencedTweet of tweet.referenced_tweets) {
              if(referencedTweet.type === "retweeted") {
                retweetedCheck = true
              }
            }
            if (retweetedCheck === false){
              retweetArray.push(this.parselocalRetweet(tweet, campaignsData.campaign_id))

            }   
          }
          else retweetArray.push(this.parselocalRetweet(tweet, campaignsData.campaign_id))
        }

        return retweetArray
      }

    }

    

    public async postTweet (purchase : purchase) : Promise<void> {
    try {
      const username : UserV2Result = await client.v2.user(`${purchase.twitter_user_id}`);
      const response = await client.v2.tweet(`Activist @${username.data.username} purchase ${purchase.product} provided by ${purchase.company} for charity campaign ${purchase.campaign}`);
      return console.log(response);
    } 
    catch (error) {
      return console.log(error);
    }
}

    private parselocalRetweet(local: TweetV2, campaignId : number): retweet {
     
        return {
          twitt_id: local.id,
          twitter_user_id: local.author_id as string,
          campaign_id: campaignId,
          retweets: local.public_metrics?.retweet_count as number,
          parsing_date: new Date(),
          creation_date: new Date(),
          update_date: new Date(),
          create_by_user: "server_script",  
          update_by_user: "server_script",  
          status: 1,   
        };
    }
}