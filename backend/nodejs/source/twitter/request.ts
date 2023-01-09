
import {TwitterUserIdService, ITwitterUserIds, ICanpaignHashtag} from '../services/twitterParsing.services';
import { ErrorService } from '../services/error.service';
import { systemError, retweet } from '../entities';
import { client} from "./twitterClient"
import { TweetV2 } from "twitter-api-v2";

const errorService: ErrorService = new ErrorService();
const retweetService: TwitterUserIdService = new TwitterUserIdService(errorService);

async function processTwitterData() {

      //get Ids and Hashtags for parsing
      const userIds : ITwitterUserIds[] = await retweetService.getTwitterUserIds();
      const hashtag : ICanpaignHashtag[] = await retweetService.getCanpaignHashtag();
       
      //Check list of Ids and Hashtags
      for (let i = 0; i < userIds.length; i++) {
        for (let j = 0; j < hashtag.length; j++) {
            console.log(`${hashtag[j].hashtag} from:${userIds[i].twitter_user_id}`);
        }}
      //

      let retweetArray : retweet[] = []
      // Search Tweets using Ids and Hashtags Metod and convertion to retweet type
      async function searchTweetsByIdHash(userId : string , hashtag : string)  {
        let twitterQuery = `${hashtag} from:${userId}`;
        const jsTweets = await client.v2.search(
            twitterQuery, {
            'tweet.fields': 'public_metrics,author_id',
          })
      
        for await (const tweet of jsTweets) {
          console.log(tweet);
          retweetArray.push(await parselocalRetweet(tweet, "23"))
          console.log(retweetArray)
        
        }
      }
  
      // Use created list of Ids and Hashtags to create list of retweets
      async function consumeTweets2 () {
        for (let i = 0; i < userIds.length; i++) {
        for (let j = 0; j < hashtag.length; j++) {
          await searchTweetsByIdHash(userIds[i].twitter_user_id, hashtag[j].hashtag);

        }
        }  
      }    
      consumeTweets2()
}

  processTwitterData()

async function parselocalRetweet(local: TweetV2, hashtag : string): Promise<retweet> {
      return {
        twitt_id: local.id,
        twitter_user_id: local.author_id as string,
        retweets: local.public_metrics?.retweet_count as number,
        campaign: hashtag,
        parsing_date: new Date(2023.01),
      };
    }