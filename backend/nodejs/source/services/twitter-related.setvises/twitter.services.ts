import { retweet } from '../../entities';
import { client, NON_EXISTENT_ID} from "../../constants"
import { TweetV2, TweetSearchRecentV2Paginator} from "twitter-api-v2";
import { DateHelper } from "../../helpers/date.helper";

export interface IUserHashtag {
  twitter_user_id: string;
  hashtag: string;
}

export interface ICanpaignHashtag {
  hashtag: string;
  campaign_name: string;
}

interface ITwitterService {
    searchTweetsByIdHashtag(userId : string , campaignsData : ICanpaignHashtag): Promise<retweet[]>;
  }

export class TwitterService implements ITwitterService {
    
    public async searchTweetsByIdHashtag (userId : string , campaignsData : ICanpaignHashtag) : Promise<retweet[]> {
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
              retweetArray.push(this.parselocalRetweet(tweet, campaignsData.campaign_name))

            }   
          }
          else retweetArray.push(this.parselocalRetweet(tweet, campaignsData.campaign_name))
        }

        return retweetArray
      }

    }

    
    private parselocalRetweet(local: TweetV2, lCampaign : string): retweet {
     
        return {
          twitt_id: local.id,
          twitter_user_id: local.author_id as string,
          campaign: lCampaign,
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