import { retweet } from '../entities';
import { client} from "../constants"
import { TweetV2, TweetSearchRecentV2Paginator} from "twitter-api-v2";

interface ITwitterService {
    searchTweetsByIdHashtag(userId : string , hashtag : string): Promise<retweet[]>;
  }

export class TwitterService implements ITwitterService {
    
    public async searchTweetsByIdHashtag (userId : string , hashtag : string) : Promise<retweet[]> {
        const twitterQuery : string = `${hashtag} from:${userId}`;
        let retweetArray : retweet[] = [];

        const jsTweets : TweetSearchRecentV2Paginator = await client.v2.search(
            twitterQuery, {
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
              retweetArray.push(this.parselocalRetweet(tweet, hashtag))

            }   
          }
          else retweetArray.push(this.parselocalRetweet(tweet, hashtag))
        }

        return retweetArray
      }

    }


    private parselocalRetweet(local: TweetV2, hashtag : string): retweet {
        return {
          twitt_id: local.id,
          twitter_user_id: local.author_id as string,
          retweets: local.public_metrics?.retweet_count as number,
          campaign: hashtag,
          parsing_date: new Date(2023.01),
        };
    }
}