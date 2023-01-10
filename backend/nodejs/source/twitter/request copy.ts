
import {TwitterUserIdService, ITwitterUserIds, ICanpaignHashtag} from '../services/twitterParsing.services';
import { ErrorService } from '../services/error.service';
import { systemError, retweet } from '../entities';
import { client} from "./twitterClient"
import { TweetV2 } from "twitter-api-v2";
import { SqlHelper } from "../helpers/sql.helper";
import { Queries} from "../constants"

const errorService: ErrorService = new ErrorService();
const retweetService: TwitterUserIdService = new TwitterUserIdService(errorService);

async function processTwitterData() {

      //get Ids and Hashtags for parsing
      const userIds : ITwitterUserIds[] = await retweetService.getTwitterUserIds();
      const hashtag : ICanpaignHashtag[] = await retweetService.getCanpaignHashtag();
      const retweetList : retweet[] = await consumeTweets2();

      // Check list of Ids and Hashtags
      // for (let i = 0; i < userIds.length; i++) {
      //   for (let j = 0; j < hashtag.length; j++) {
      //       console.log(`${hashtag[j].hashtag} from:${userIds[i].twitter_user_id}`);
      // }}

      // Search Tweets using Ids and Hashtags Metod and convertion to retweet type
      async function searchTweetsByIdHash(userId : string , hashtag : string) {
        let twitterQuery = `${hashtag} from:${userId}`;
        const jsTweets = await client.v2.search(
            twitterQuery, {
            'tweet.fields': 'public_metrics,author_id',
          })
        return new Promise <retweet[]>((resolve, reject) => {
        const retweetArray : retweet[] = [];

        for await (const tweet of jsTweets) {
          // console.log(tweet);
          retweetArray.push(await parselocalRetweet(tweet, "23"))
          // console.log(retweetArray)
        }
      }
  
      // Use created list of Ids and Hashtags to create list of retweets
      async function consumeTweets2 () {
        for (let i = 0; i < userIds.length; i++) {
        for (let j = 0; j < hashtag.length; j++) {
          await searchTweetsByIdHash(userIds[i].twitter_user_id, hashtag[j].hashtag);
        return 

        }
        }  
      }
 
      async function addRetweetsFromTwitter(){
      let retweetList = consumeTweets2()
      return new Promise<retweet>((resolve, reject) => {
        SqlHelper.createNew(
          errorService,
          Queries.AddRetweet,
          retwretweetListeet,
          retweet.twitt_id,
          retweet.twitter_user_id,
          retweet.retweets,
          retweet.campaign,
          retweet.parsing_date
        )
        .then((result: retweet) => {
          resolve(result);
          })
          .catch((error: systemError) => {
            reject(error);
          });
        });
}
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